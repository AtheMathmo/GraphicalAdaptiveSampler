using System;

namespace GraphicalAdaptiveSampler.Distributions
{
    public interface ILogConcaveDistribution<DomainT> : IDistribution<DomainT>
    {
        DomainT MaxPoint { get; }
    }
}

