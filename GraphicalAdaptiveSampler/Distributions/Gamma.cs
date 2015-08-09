using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Utils;

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
            return (this.Shape - 1) * Math.Log(x) - (x / this.Scale) - (this.Shape * Math.Log(this.Scale)) - AMaths.LnGamma(this.Shape);
        }

        public double GetLogProbDeriv (double x)
        {
            return (this.Shape - 1) / x - 1 / this.Scale;
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
