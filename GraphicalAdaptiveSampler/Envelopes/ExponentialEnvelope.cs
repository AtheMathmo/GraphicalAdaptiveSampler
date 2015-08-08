using System;
using System.Collections.Generic;

using GraphicalAdaptiveSampler.Distributions;

namespace GraphicalAdaptiveSampler.Envelopes
{
    class ExponentialEnvelope : Envelope
    {
        public ExponentialEnvelope(double domainInf, double domainSup,
            IList<double> initialPoints, IDistribution<double> distr)
            : base(domainInf, domainSup, initialPoints)
        {

        }

        protected override void UpdateRegionBound (Region region)
        {
            throw new NotImplementedException ();
        }

		public override int SampleDiscrete ()
    	{
    		throw new NotImplementedException ();
    	}


            
          
    }
}
