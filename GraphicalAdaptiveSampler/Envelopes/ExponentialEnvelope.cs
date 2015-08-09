using System;
using System.Collections.Generic;

using GraphicalAdaptiveSampler.Distributions;

namespace GraphicalAdaptiveSampler.Envelopes
{
    class ExponentialEnvelope : Envelope
    {
        private IDistribution<double> distr;

        public ExponentialEnvelope(double domainInf, double domainSup,
            IList<double> initialPoints, IDistribution<double> distr)
            : base(domainInf, domainSup, initialPoints)
        {
            this.distr = distr;
        }

        protected override void UpdateRegionBound (Region region)
        {
            throw new NotImplementedException ();
        }

        protected override double GetRegionProb(Region region)
    	{
    		throw new NotImplementedException ();
    	}

        public override double SampleContinuous ()
        {
            // Here we compute the inverse of the exponential CDF (after normalizing).
            Region sampledRegion = SampleDiscrete();
            double rand = Utils.SRandom.GetUniform();

            double grad = 1;

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
     
    }
}
