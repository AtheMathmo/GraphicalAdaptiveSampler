using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Distributions;

namespace GraphicalAdaptiveSampler.Envelopes
{
    public abstract class Envelope<BoundT>
    {
        protected IDistribution<double> distr;
        protected List<Region> regionList;
        protected Dictionary<Region, BoundT> regionBoundMap;

        public Envelope(double domainInf, double domainSup, 
            IDistribution<double> distr, IList<double> initialPoints)
        {
            // Set envelope domain bounds
            this.DomainInf = domainInf;
            this.DomainSup = domainSup;

            this.distr = distr;
			this.regionList = new List<Region> ();
            this.regionBoundMap = new Dictionary<Region, BoundT>();

            InitializeEnvelope(initialPoints);
        }

        protected abstract void InitializeEnvelope(IList<double> initialCuts);

        /// <summary>
        /// Samples over the discrete distribution to return a region.
        /// </summary>
        /// <returns>A region.</returns>
        protected Region SampleDiscrete()
        {
            List<double> probs = new List<double>();

            foreach (Region region in this.regionList)
            {
                probs.Add(this.GetRegionProb(region));
            }

            double sumOfProbs = probs.Sum();
            double cumsum = 0;
            double rand = Utils.SRandom.GetUniform();

            foreach (Region region in this.regionList)
            {
                double prob = this.GetRegionProb(region);
                cumsum += prob/sumOfProbs;
                if (rand < cumsum)
                {
                    return region;
                }
            }
            throw new InvalidOperationException("No region was sampled, i.e. range(rand) > cumsum. This should never happen.");
        }

        /// <summary>
        /// Gets the total probability for a region.
        /// </summary>
        /// <returns>The region probability.</returns>
        /// <param name="region">Region.</param>
        protected abstract double GetRegionProb(Region region);

        /// <summary>
        /// Samples continuously from the envelope.
        /// </summary>
        /// <returns>The continuous sample.</returns>
        public abstract double SampleContinuous();

        /// <summary>
        /// Updates the region bound.
        /// </summary>
        /// <param name="region">Region.</param>
        protected abstract void UpdateRegionBound(Region region);

        /// <summary>
        /// Adds a cut point to the envelope.
        /// </summary>
        /// <param name="point">Cut point.</param>
        public void AddCutPoint(double cutPoint)
		{
            if (cutPoint < this.DomainInf || cutPoint > this.DomainSup)
            {
                throw new ArgumentException("New cut point must be within envelope domain", "point");
            }

            // Check for the region containing the point
			foreach (Region region in this.regionList)
			{
                if (region.ContainsPoint(cutPoint))
                {
                    // Add new region to the right of the intersection
                    Region newRegion = new Region(cutPoint, region.UpperBound);
                    this.regionList.Add(newRegion);
                    region.UpperBound = cutPoint;

                    // Update the bounds on the modified regions
                    UpdateRegionBound(region);
                    UpdateRegionBound(newRegion);

                    // We break here under the assumption that regions are distinct.
                    break;
                }
			}
		}

        /// <summary>
        /// Gets the domain inf.
        /// </summary>
        /// <value>The domain inf.</value>
        public double DomainInf { get; private set; }

        /// <summary>
        /// Gets the domain sup.
        /// </summary>
        /// <value>The domain sup.</value>
        public double DomainSup { get; private set; }

        /// <summary>
        /// Region representing discrete subset of envelope domain.
        /// 
        /// </summary>
        public class Region
        {
            private double lowerBound;
            private double upperBound;

            public Region(double lowerBound, double upperBound)
            {
                this.lowerBound = lowerBound;
                this.upperBound = upperBound;
            }

			public bool ContainsPoint(double point)
			{
                if (point >= lowerBound && point <= upperBound)
                {
                    return true;
                }
                else
                {
                    return false;
                }
			}

            public double LowerBound
            {
                get { return this.lowerBound; }
				set { this.lowerBound = value; }
            }

            public double UpperBound
            {
                get { return this.upperBound; }
				set { this.upperBound = value; }
            }
        }
    }
}
