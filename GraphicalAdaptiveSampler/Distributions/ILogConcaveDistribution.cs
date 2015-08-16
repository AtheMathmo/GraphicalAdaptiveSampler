using System;

namespace GraphicalAdaptiveSampler.Distributions
{
    public interface ILogConcaveDistribution<DomainT> : IDistribution<DomainT>
    {
        /// <summary>
        /// Gets the max point for the pdf.
        /// </summary>
        /// <value>The max point.</value>
        DomainT MaxPoint { get; }
    }
}

