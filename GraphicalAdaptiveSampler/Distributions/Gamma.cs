using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Distributions
{
    class Gamma : IDistribution<double>
    {
        public Gamma(double shape, double scale)
        {
            this.Shape = shape;
            this.Scale = scale;
        }
        public Gamma()
            : this(1.0, 1.0)
        {
            
        }

        public double GetLogProb (double x)
        {
            throw new NotImplementedException ();
        }

        public double GetLogProbDeriv (double x)
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// Gets the shape of the gamma distribution.
        /// </summary>
        /// <value>The shape.</value>
        public double Shape { get; private set; }

        /// <summary>
        /// Gets the scale of the gamma distribution.
        /// </summary>
        /// <value>The scale.</value>
        public double Scale { get; private set; }

        public bool IsLogConcave {
            get {
                if (this.Shape < 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }
}
