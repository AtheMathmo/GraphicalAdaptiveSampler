using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Distributions
{
    class Gaussian : IDistribution<double>
    {
        /// <summary>
        /// Computes the log of the pdf for the Gaussian distribution.
        /// </summary>
        /// <param name="x">Point at which to evaluate.</param>
        /// <returns>The log of the pdf.</returns>
        public double GetLogProb(double x)
        {
            return Math.Log(this.Precision) - Math.Log(2 * Math.PI) - this.Precision * ((x - this.Mean) * (x - this.Mean)) / 2;
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
        public double Mean
        {
            get { return this.Mean; }
            set { this.Mean = value; }
        }

        /// <summary>
        /// The precision of the Gaussian distribution.
        /// </summary>
        public double Precision
        {
            get { return this.Precision; }
            set { this.Precision = value; }
        }
    }
}
