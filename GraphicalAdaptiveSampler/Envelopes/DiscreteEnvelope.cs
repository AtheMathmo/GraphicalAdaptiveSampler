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
		}

        protected override void UpdateRegionBound (Region region)
        {
            double regionMax = ComputeRegionMax(region);

            Console.WriteLine(regionMax);
            if (this.regionBoundMap.ContainsKey(region))
            {
                this.regionBoundMap[region] = regionMax;
            }
            else
            {
                this.regionBoundMap.Add(region, regionMax);    
            }
        }

		public override int SampleDiscrete ()
		{
            List<double> probs = new List<double>();

            foreach (Region region in this.regionList)
            {
                probs.Add(Math.Exp(this.regionBoundMap[region]));
            }

            double sumOfProbs = probs.Sum();
            double cumsum = 0;
            double rand = 0;
            throw new NotImplementedException();

            for (int i = 0; i < probs.Count(); i++)
            {
                double prob = probs[i];
                cumsum += probs[i]/sumOfProbs;
                if (rand < cumsum)
                {
                    return i;
                }
            }

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
            // distr is used in this method.
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

