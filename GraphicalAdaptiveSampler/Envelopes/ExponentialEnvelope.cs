using System;
using System.Linq;
using System.Collections.Generic;

using GraphicalAdaptiveSampler.Distributions;

namespace GraphicalAdaptiveSampler.Envelopes
{
    class LinearBound
    {
        public LinearBound(double intercept, double gradient)
        {
            this.Intercept = intercept;
            this.Gradient = gradient;
        }

        public double Intercept { get; set; }

        public double Gradient {get; set; }
    }

    class ExponentialEnvelope : Envelope<LinearBound>
    {

        public ExponentialEnvelope(double domainInf, double domainSup,
            IDistribution<double> distr, IList<double> initialPoints)
            : base(domainInf, domainSup, distr, initialPoints)
        {
            if (distr.IsLogConcave)
            {
                this.distr = distr;
            }
            else
            {
                throw new ArgumentException("Distribution for exponential envelope must be log-concave", "distr");    
            }

        }

        protected override void InitializeEnvelope(IList<double> initialPoints)
        {
            int pointCount = initialPoints.Count;
            List<double> logProbs = new List<double>();
            List<double> grads = new List<double>();
            List<double> intercepts = new List<double>();

            List<double> upperGrads = new List<double>();
            List<double> upperIntercepts = new List<double>();

            foreach (double point in initialPoints.OrderBy(p => p))
            {
                logProbs.Add(this.distr.GetLogProb(point));
            }


            using (IEnumerator<double> points = initialPoints.OrderBy(p => p).GetEnumerator())
            {
                int index = 0;
                points.MoveNext();
                double lastPoint = points.Current;
                double currentLogProb = logProbs[index];

                while (points.MoveNext())
                {
                    // This is wrong, bounds are constructed from intersections as shown here:
                    // http://www.angelfire.com/falcon/isinotes/statcomp/mcmc/gibbs5.html
                    double nextLogProb = logProbs[++index];

                    double grad = (nextLogProb - currentLogProb) / (points.Current - lastPoint);
                    double intercept = currentLogProb - lastPoint * grad;
                    grads.Add(grad);
                    intercepts.Add(intercept);

                    lastPoint = points.Current;
                    currentLogProb = logProbs[index];
                }

                index = 0;
                points.Reset();
                points.MoveNext();
                lastPoint = points.Current;

                {
                    Region region = new Region(DomainInf, lastPoint);
                    this.regionList.Add(region);
                    this.regionBoundMap.Add(region, new LinearBound(intercepts[index], grads[index]));
                    index++;
                }

                // Iterate over all but last (and first)
                points.MoveNext();
                for (var value = points.Current; points.MoveNext(); value = points.Current)
                {
                    // TODO add in the rest of this part from python code.

                    // Take gradient after point for region before point
                    Region backRegion = new Region(lastPoint, value);
                    this.regionList.Add(backRegion);
                    this.regionBoundMap.Add(backRegion, new LinearBound(intercepts[index], grads[index]));

                    // Find intersect
                    double intercept = (intercepts[index + 1] - intercepts[index - 1]) /
                                       (grads[index - 1] - grads[index + 1]);

                    // Take gradient before point for region after point
                    Region forwardRegion = new Region(value, intercept);
                    this.regionList.Add(forwardRegion);
                    this.regionBoundMap.Add(forwardRegion, new LinearBound(intercepts[index - 1], grads[index - 1]));
                }
            }
                
        }

        protected override void UpdateRegionBound(Region region)
        {
            throw new NotImplementedException ();
        }

        protected override double GetRegionProb(Region region)
    	{
            double intercept = this.regionBoundMap[region].Intercept;
            double grad = this.regionBoundMap[region].Gradient;

            return (Math.Exp(intercept + grad * region.UpperBound) - Math.Exp(intercept + grad * region.LowerBound)) / grad;
    	}

        public override double SampleContinuous ()
        {
            // Here we compute the inverse of the exponential CDF (after normalizing).
            Region sampledRegion = SampleDiscrete();
            double rand = Utils.SRandom.GetUniform();

            double grad = this.regionBoundMap[sampledRegion].Gradient;

            if (sampledRegion.LowerBound == double.NegativeInfinity)
            {
                return (Math.Log(rand) / grad) + sampledRegion.UpperBound;
            }
            else if (sampledRegion.UpperBound == double.PositiveInfinity)
            {
                return (Math.Log(1 - rand) / grad) + sampledRegion.LowerBound;
            }
            else
            {
                double exponent = rand * (Math.Exp(grad * sampledRegion.UpperBound) - Math.Exp(grad * sampledRegion.LowerBound));
                exponent += Math.Exp(grad * sampledRegion.LowerBound);
                return Math.Log(exponent) / grad; 
            }
        }

        private LinearBound ComputeRegionBound(Region region)
        {
            throw new NotImplementedException();
        }     
    }
}
