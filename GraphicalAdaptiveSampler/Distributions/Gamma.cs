using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Utils;

namespace GraphicalAdaptiveSampler.Distributions
{
    class Gamma : IDistribution<double>, ILogConcaveDistribution<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Distributions.Gamma"/> class, with given shape and scale.
        /// </summary>
        /// <param name="shape">Shape.</param>
        /// <param name="scale">Scale.</param>
        public Gamma(double shape, double scale)
        {
            if (shape <= 0)
            {
                throw new ArgumentException("Shape must be positive.");
            }

            if (scale <= 0)
            {
                throw new ArgumentException("Scale must be positive.");
            }

            this.Shape = shape;
            this.Scale = scale;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Distributions.Gamma"/> class, with shape 1 and scale 1.
        /// </summary>
        public Gamma()
            : this(1.0, 1.0)
        {
            
        }

        /// <summary>
        /// Computes the log of the pdf for the Gamma distribution.
        /// </summary>
        /// <param name="x">Point at which to evaluate.</param>
        /// <returns>The log of the pdf.</returns>
        public double GetLogProb (double x)
        {
            if (x <= 0)
            {
                throw new ArgumentException("x must be positive.");
            }

            return (this.Shape - 1) * Math.Log(x) - (x / this.Scale) - (this.Shape * Math.Log(this.Scale)) - AMaths.LnGamma(this.Shape);
        }

        /// <summary>
        /// Computes the derivative of the log of the pdf for the Gamma distribution.
        /// </summary>
        /// <param name="x">Point at which to evaluate.</param>
        /// <returns>The log of the pdf.</returns>
        public double GetLogProbDeriv (double x)
        {
            if (x <= 0)
            {
                throw new ArgumentException("x must be positive.");
            }

            return (this.Shape - 1) / x - 1 / this.Scale;
        }

        /// <summary>
        /// Gets the max point of the pdf.
        /// </summary>
        /// <value>The max point.</value>
        public double MaxPoint {
            get {
                if (this.Shape < 1)
                {
                    throw new InvalidOperationException("This gamma distribution is not log-concave." +
                        "Check log-concavity before calling this method.");
                }
                return (this.Shape - 1) * this.Scale;
            }
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

        /// <summary>
        /// Gets a value indicating whether this instance is log concave.
        /// </summary>
        /// <value>true</value>
        /// <c>false</c>
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
