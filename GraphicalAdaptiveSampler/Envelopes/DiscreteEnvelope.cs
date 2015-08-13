using System;
using System.Collections.Generic;
using System.Linq;

using GraphicalAdaptiveSampler.Distributions;

namespace GraphicalAdaptiveSampler.Envelopes
{
    public class DiscreteEnvelope : Envelope<double>
	{
        public DiscreteEnvelope (double domainInf, double domainSup,
            IDistribution<double> distr, IList<double> initialPoints)
            : base(domainInf, domainSup, distr, initialPoints)
		{
            
		}

        protected override void InitializeEnvelope(IList<double> initialPoints)
        {
            using (IEnumerator<double> points = initialPoints.OrderBy(p => p).GetEnumerator())
            {
                points.MoveNext();
                double currentPoint = points.Current;

                this.regionList.Add(new Region(this.DomainInf, currentPoint));

                while (points.MoveNext())
                {
                    this.regionList.Add(new Region(currentPoint, points.Current));
                    currentPoint = points.Current;
                }

                this.regionList.Add(new Region(currentPoint, this.DomainSup));
            }
            foreach (Region region in this.regionList)
            {
                this.regionBoundMap.Add(region, ComputeRegionMax(region));
            }
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
                throw new NotSupportedException("Cannot sample uniformly over infinite domains. Use a transformation.");
            }

            return sampledRegion.LowerBound + rand * (sampledRegion.UpperBound - sampledRegion.LowerBound);
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

