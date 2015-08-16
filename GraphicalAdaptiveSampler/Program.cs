using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Distributions;
using GraphicalAdaptiveSampler.Envelopes;
using GraphicalAdaptiveSampler.Utils;

namespace GraphicalAdaptiveSampler
{
    class Program
    {
        static void Main(string[] args)
        {
            // The idea is that I will abuse log concavity to envelope the distribution with exponential bounds.
            // Then to compute the discretization I will use the integral of the exponential to weight each region:
            // exp(r_j)-exp(l_j)/sum(exp(r_i)-exp(l_i)
            // Ensuring that for infinite regions we have a negative gradient.

            //TestEnvelope();
            TestGammaFunc(5);
            //ARSTest();
        }

        private static void TestEnvelope()
        {
            Gaussian gaussian = new Gaussian(1, 1);
            ExponentialEnvelope env = new ExponentialEnvelope(double.NegativeInfinity, double.PositiveInfinity, gaussian, new double[] { -2, -1, 0, 1, 2 });

            for (int i = 0; i < 10000; i++)
            {
                double test = env.SampleContinuous();
                Console.WriteLine(test);
            }
            Console.WriteLine("All Worked!");
        }

        private static void TestGammaFunc(double x)
        {
            Console.WriteLine(AMaths.LnGamma(x));
        }

        private static void ARSTest(int sampleCount = 100000)
        {
            List<double> acceptedSamples = new List<double>();

            double shape = 2;
            double scale = 2;
            Gamma gaussian = new Gamma(shape, scale);
            DiscreteEnvelope envelope = new DiscreteEnvelope(0, 1000, gaussian, new double[] { 1, 5 });

            int rejectedCount = 0;
            while  (acceptedSamples.Count < sampleCount)
            {
                var sampleRegion = envelope.SampleDiscrete();
                double sample = envelope.SampleContinuous(sampleRegion);

                double ratio = Math.Exp(gaussian.GetLogProb(sample) - envelope.GetLogProb(sampleRegion, sample));
                double u = Utils.SRandom.GetUniform();

                if (u < ratio)
                {
                    Console.WriteLine("Sample accepted {0}/{1} : {2}", acceptedSamples.Count + 1, sampleCount, sample);
                    acceptedSamples.Add(sample);
                }
                else
                {
                    Console.WriteLine("Rejected, adding cut at {0}", sample);
                    rejectedCount++;
                    envelope.AddCutPoint(sample);
                }
            }
            double mean = acceptedSamples.Sum() / acceptedSamples.Count;
            double variance = acceptedSamples.Select(s => (s - mean) * (s - mean)).Sum() / (sampleCount - 1);

            Console.WriteLine("Total Rejected = {0}", rejectedCount);
            Console.WriteLine("Sample Mean = {0}, Sample Variance = {1}", mean, variance);
            Console.WriteLine("True Mean = {0},     True Variance = {1}", shape * scale, shape * scale * scale);
        }
    }
}
