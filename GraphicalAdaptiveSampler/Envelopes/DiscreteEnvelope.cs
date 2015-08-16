using System;
using System.Collections.Generic;
using System.Linq;

using GraphicalAdaptiveSampler.Distributions;

namespace GraphicalAdaptiveSampler.Envelopes
{
    /// <summary>
    /// Discrete envelope.
    /// Envelope consists of discrete bounds.
    /// </summary>
    public class DiscreteEnvelope : Envelope<double>
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Envelopes.DiscreteEnvelope"/> class.
        /// </summary>
        /// <param name="domainInf">Domain inf.</param>
        /// <param name="domainSup">Domain sup.</param>
        /// <param name="distr">Distr.</param>
        /// <param name="initialPoints">Initial points.</param>
        public DiscreteEnvelope (double domainInf, double domainSup,
            IDistribution<double> distr, IList<double> initialPoints)
            : base(domainInf, domainSup, distr, initialPoints)
		{
            
		}

        /// <summary>
        /// Initializes the envelope. Creates the regions and associated bounds.
        /// </summary>
        /// <param name="initialCuts">Initial cuts.</param>
        /// <param name="initialPoints">Initial points.</param>
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

        /// <summary>
        /// Computes the envelope value of a given sample, under a specified bound.
        /// The sample should be within the region with the specified bound,
        /// but this method does not check for this.
        /// </summary>
        /// <returns>The prob from bound.</returns>
        /// <param name="bound">Bound.</param>
        /// <param name="sample">Sample.</param>
        protected override double LogProbFromBound(double bound, double sample)
        {
            return bound;
        }

        /// <summary>
        /// Updates the region bound.
        /// </summary>
        /// <param name="region">Region.</param>
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

        /// <summary>
        /// Gets the total probability for a region.
        /// </summary>
        /// <returns>The region probability.</returns>
        /// <param name="region">Region.</param>
        protected override double GetRegionProb(Region region)
        {
            return Math.Exp(this.regionBoundMap[region]) * (region.UpperBound - region.LowerBound);
        }

        /// <summary>
        /// Samples from the continuous distribution of this envelope, using a given region.
        /// </summary>
        /// <returns>The continuous sample.</returns>
        /// <param name="sampledRegion">Sampled region.</param>
        public override double SampleContinuous(Region sampledRegion)
        {
            double rand = Utils.SRandom.GetUniform();

            if (sampledRegion.LowerBound == double.NegativeInfinity || sampledRegion.UpperBound == double.PositiveInfinity)
            {
                throw new NotSupportedException("Cannot sample uniformly over infinite domains. Use a transformation.");
            }

            return sampledRegion.LowerBound + rand * (sampledRegion.UpperBound - sampledRegion.LowerBound);
        }

        /// <summary>
        /// Computes the max of the log pdf in the specified region.
        /// </summary>
        /// <returns>The region max.</returns>
        /// <param name="region">Region.</param>
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

