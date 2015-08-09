using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Envelopes
{
    public abstract class Envelope
    {
        protected List<Region> regionList;

        public Envelope(double domainInf, double domainSup, IList<double> initialPoints)
        {
            // Set envelope domain bounds
            this.DomainInf = domainInf;
            this.DomainSup = domainSup;

			this.regionList = new List<Region> ();

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
        }

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

        protected abstract double GetRegionProb(Region region);

        public abstract double SampleContinuous();

        protected abstract void UpdateRegionBound(Region region);

        public void AddCutPoint(double point)
		{
            if (point < this.DomainInf || point > this.DomainSup)
            {
                throw new ArgumentException("New cut point must be within envelope domain", "point");
            }

            // Check for the region containing the point
			foreach (Region region in this.regionList)
			{
                if (region.ContainsPoint(point))
                {
                    // Add new region to the right of the intersection
                    Region newRegion = new Region(point, region.UpperBound);
                    this.regionList.Add(newRegion);
                    region.UpperBound = point;

                    // Update the bounds on the modified regions
                    UpdateRegionBound(region);
                    UpdateRegionBound(newRegion);

                    // We break here under the assumption that regions are distinct.
                    break;
                }
			}
		}

        public double DomainInf { get; private set; }

        public double DomainSup { get; private set; }

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
