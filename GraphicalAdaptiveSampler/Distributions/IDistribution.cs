using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Distributions
{
    public interface IDistribution<DomainT>
    {
        /// <summary>
        /// Computes the log of the pdf.
        /// </summary>
        /// <param name="x">Point at which to evaluate.</param>
        /// <returns>The log of the pdf.</returns>
        double GetLogProb(DomainT x);

        /// <summary>
        /// Computes the derivative of the log of the pdf.
        /// </summary>
        /// <param name="x">Point at which to evaluate.</param>
        /// <returns>The log of the pdf.</returns>
        double GetLogProbDeriv(DomainT x);
    }
}
