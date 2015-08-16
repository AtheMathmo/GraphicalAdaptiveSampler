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

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Envelopes.Envelope`1"/> class.
        /// </summary>
        /// <param name="domainInf">Domain inf.</param>
        /// <param name="domainSup">Domain sup.</param>
        /// <param name="distr">Distr.</param>
        /// <param name="initialPoints">Initial points.</param>
        public Envelope(double domainInf, double domainSup, 
            IDistribution<double> distr, IList<double> initialPoints)
        {
            // Set envelope domain bounds
            this.DomainInf = domainInf;
            this.DomainSup = domainSup;

            this.distr = distr;
			this.regionList = new List<Region> ();
            this.regionBoundMap = new Dictionary<Region, BoundT>();

            foreach (double point in initialPoints)
            {
                if (point < domainInf || point > domainSup)
                {
                    throw new ArgumentOutOfRangeException("initialPoints","Initial points are not within the domain range.");
                }
            }

            InitializeEnvelope(initialPoints);
        }

        /// <summary>
        /// Initializes the envelope. Creates the regions and associated bounds.
        /// </summary>
        /// <param name="initialCuts">Initial cuts.</param>
        protected abstract void InitializeEnvelope(IList<double> initialCuts);

        /// <summary>
        /// Samples over the discrete distribution to return a region.
        /// </summary>
        /// <returns>A region.</returns>
        public Region SampleDiscrete()
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
        /// Samples from the continuous distribution of this envelope, using a given region.
        /// </summary>
        /// <returns>The continuous sample.</returns>
        public abstract double SampleContinuous(Region sampledRegion);

        /// <summary>
        /// Samples from the continuous distribution of this envelope.
        /// </summary>
        /// <returns>The continuous.</returns>
        public double SampleContinuous()
        {
            Region sampledRegion = SampleDiscrete();
            return SampleContinuous(sampledRegion);
        }

        /// <summary>
        /// Gets the log prob of a specific sample, within a given region.
        /// The region is specified to save an o(n) search for the location of the sample.
        /// </summary>
        /// <returns>The log prob.</returns>
        /// <param name="sampleRegion">Sample region.</param>
        /// <param name="sample">Sample.</param>
        public double GetLogProb(Region sampleRegion, double sample)
        {
            if (sample < sampleRegion.LowerBound || sample > sampleRegion.UpperBound)
            {
                throw new ArgumentException("Sample is not within specified region.");
            }

            BoundT bound = this.regionBoundMap[sampleRegion];

            return LogProbFromBound(bound, sample);
        }

        /// <summary>
        /// Computes the envelope value of a given sample, under a specified bound.
        /// The sample should be within the region with the specified bound,
        /// but this method does not check for this.
        /// </summary>
        /// <returns>The prob from bound.</returns>
        /// <param name="bound">Bound.</param>
        /// <param name="sample">Sample.</param>
        protected abstract double LogProbFromBound(BoundT bound, double sample);

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

            /// <summary>
            /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Envelopes.Envelope`1+Region"/> class.
            /// </summary>
            /// <param name="lowerBound">Lower bound.</param>
            /// <param name="upperBound">Upper bound.</param>
            public Region(double lowerBound, double upperBound)
            {
                this.lowerBound = lowerBound;
                this.upperBound = upperBound;
            }

            /// <summary>
            /// Checks if the point is within the region, returns true if it is.
            /// </summary>
            /// <returns><c>true</c>, if point was containsed, <c>false</c> otherwise.</returns>
            /// <param name="point">Point.</param>
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

            /// <summary>
            /// Gets or sets the region lower bound.
            /// </summary>
            /// <value>The lower bound.</value>
            public double LowerBound
            {
                get { return this.lowerBound; }
				set { this.lowerBound = value; }
            }

            /// <summary>
            /// Gets or sets the region upper bound.
            /// </summary>
            /// <value>The upper bound.</value>
            public double UpperBound
            {
                get { return this.upperBound; }
				set { this.upperBound = value; }
            }
        }
    }
}
