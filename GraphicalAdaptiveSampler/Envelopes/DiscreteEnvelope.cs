using System;
using System.Collections.Generic;
using System.Linq;

using GraphicalAdaptiveSampler.Distributions;

namespace GraphicalAdaptiveSampler.Envelopes
{
	public class DiscreteEnvelope : Envelope
	{
        private IDistribution<double> distr;
        private Dictionary<Region, double> regionBoundMap;

        public DiscreteEnvelope (double domainInf, double domainSup,
            IList<double> initialPoints, IDistribution<double> distr)
            : base(domainInf, domainSup, initialPoints)
		{
            this.distr = distr;
            this.regionBoundMap = new Dictionary<Region, double>();
            this.InitializeRegionBounds();
		}

        protected override void UpdateRegionBound (Region region)
        {
            double regionMax = ComputeRegionMax(region);

            if (this.regionBoundMap.ContainsKey(region))
            {
                this.regionBoundMap[region] = regionMax;
            }
            else
            {
                this.regionBoundMap.Add(region, regionMax);    
            }
        }

        protected override double GetRegionProb(Region region)
        {
            return Math.Exp(this.regionBoundMap[region]);
        }

        public override double SampleContinuous()
        {
            Region sampledRegion = SampleDiscrete();
            double rand = Utils.SRandom.GetUniform();

            if (sampledRegion.LowerBound == double.NegativeInfinity || sampledRegion.UpperBound == double.PositiveInfinity)
            {
                throw new NotImplementedException("Cannot sample uniformly over infinite domains. Use a transformation.");
            }

            return sampledRegion.LowerBound + rand * (sampledRegion.UpperBound - sampledRegion.LowerBound);
        }

        private void InitializeRegionBounds()
        {
            foreach (Region region in this.regionList)
            {
                this.regionBoundMap.Add(region, ComputeRegionMax(region));
            }
        }

        private double ComputeRegionMax(Region region)
        {
            // If log concave exact values can be chosen.
            if (distr.IsLogConcave)
            {
                ILogConcaveDistribution<double> logConcDistr = (ILogConcaveDistribution<double>) distr;
                double max = logConcDistr.MaxPoint;

                if (max < region.LowerBound)
                {
                    return distr.GetLogProb(region.LowerBound);
                }
                else if (max > region.UpperBound)
                {
                    return distr.GetLogProb(region.UpperBound);
                }
                else
                {
                    return distr.GetLogProb(max);
                }
            }

            throw new NotImplementedException();
        }
	}
}

