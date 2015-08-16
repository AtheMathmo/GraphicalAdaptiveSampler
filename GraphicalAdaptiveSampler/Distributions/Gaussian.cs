using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Utils;

namespace GraphicalAdaptiveSampler.Distributions
{
    class Gaussian : ILogConcaveDistribution<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Distributions.Gaussian"/> class, with given mean and precision.
        /// </summary>
        /// <param name="mean">Mean.</param>
        /// <param name="precision">Precision.</param>
        public Gaussian(double mean, double precision)
        {
            this.Mean = mean;
            this.Precision = precision;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Distributions.Gaussian"/> class, with mean 0 and precision 1.
        /// </summary>
        public Gaussian() : this(0, 1)
        {
            
        }

        /// <summary>
        /// Computes the log of the pdf for the Gaussian distribution.
        /// </summary>
        /// <param name="x">Point at which to evaluate.</param>
        /// <returns>The log of the pdf.</returns>
        public double GetLogProb(double x)
        {
            return Math.Log(this.Precision) - AMaths.LN_2_PI - this.Precision * ((x - this.Mean) * (x - this.Mean)) / 2;
        }

        /// <summary>
        /// Computes the derivative of the log of the pdf for the Gaussian distribution.
        /// </summary>
        /// <param name="x">Point at which to evaluate.</param>
        /// <returns>The log of the pdf.</returns>
        public double GetLogProbDeriv(double x)
        {
            return -this.Precision * (x - this.Mean);
        }

        /// <summary>
        /// The mean of the Gaussian distribution.
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// The precision of the Gaussian distribution.
        /// </summary>
        public double Precision { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is log concave.
        /// </summary>
        /// <value>true</value>
        /// <c>false</c>
        public bool IsLogConcave {
            get {
                return true;
            }
        }

        /// <summary>
        /// Gets the max point of the pdf.
        /// </summary>
        /// <value>The max point.</value>
        public double MaxPoint {
            get {
                return this.Mean;
            }
        }

    }
}
