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

        public Envelope()
        {

        }

        public abstract double Sample();

        public abstract IEnumerable<double> GetLogProbs();

        public class Region
        {
            private double lowerBound;
            private double upperBound;

            private Tuple<double, double> linearLogBound;


            public Region(double lowerBound, double upperBound)
            {
                this.lowerBound = lowerBound;
                this.upperBound = upperBound;
            }

            public double LowerBound
            {
                get { return this.lowerBound; }
            }

            public double UpperBound
            {
                get { return this.upperBound; }
            }
        }
    }
}
