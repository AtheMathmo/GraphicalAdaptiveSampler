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

            this.regionList.Add(new Region(double.NegativeInfinity, -1));
            this.regionList.Add(new Region(-1, 0));
            this.regionList.Add(new Region(0, 2));
            this.regionList.Add(new Region(2, double.PositiveInfinity));
        }

        public abstract int SampleDiscrete();

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

        protected class Region
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
