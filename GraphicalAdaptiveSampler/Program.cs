using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Distributions;
using GraphicalAdaptiveSampler.Envelopes;

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

            TestEnvelope();
        }

        private static void TestEnvelope()
        {
            Gaussian gaussian = new Gaussian(0, 1e-3);
            DiscreteEnvelope env = new DiscreteEnvelope(-5, 5, new double[] { -2, 0, 2 }, gaussian);

            env.AddCutPoint(4);

            for (int i = 0; i < 10000; i++)
            {
                double test = env.SampleContinuous();
                Console.WriteLine(test);
            }
            Console.WriteLine("All Worked!");
        }
    }
}
