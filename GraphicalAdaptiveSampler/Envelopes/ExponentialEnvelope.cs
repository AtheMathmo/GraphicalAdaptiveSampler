using System;
using System.Linq;
using System.Collections.Generic;

using GraphicalAdaptiveSampler.Distributions;

namespace GraphicalAdaptiveSampler.Envelopes
{
    /// <summary>
    /// Linear bound for the exponential envelope.
    /// </summary>
    internal class LinearBound
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Envelopes.LinearBound"/> class.
        /// </summary>
        /// <param name="intercept">Intercept.</param>
        /// <param name="gradient">Gradient.</param>
        public LinearBound(double intercept, double gradient)
        {
            this.Intercept = intercept;
            this.Gradient = gradient;
        }

        /// <summary>
        /// Gets or sets the intercept.
        /// </summary>
        /// <value>The intercept.</value>
        public double Intercept { get; set; }

        /// <summary>
        /// Gets or sets the gradient.
        /// </summary>
        /// <value>The gradient.</value>
        public double Gradient {get; set; }
    }

    /// <summary>
    /// Exponential envelope.
    /// Formed from the procedure described here:
    /// http://www.angelfire.com/falcon/isinotes/statcomp/mcmc/gibbs5.html
    /// </summary>
    class ExponentialEnvelope : Envelope<LinearBound>
    {
        private List<double> cutPoints = new List<double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalAdaptiveSampler.Envelopes.ExponentialEnvelope"/> class.
        /// </summary>
        /// <param name="domainInf">Domain inf.</param>
        /// <param name="domainSup">Domain sup.</param>
        /// <param name="distr">Distr.</param>
        /// <param name="initialPoints">Initial points.</param>
        public ExponentialEnvelope(double domainInf, double domainSup,
            IDistribution<double> distr, IList<double> initialPoints)
            : base(domainInf, domainSup, distr, initialPoints)
        {
            if (distr.IsLogConcave)
            {
                this.distr = distr;
            }
            else
            {
                throw new ArgumentException("Distribution for exponential envelope must be log-concave", "distr");    
            }

        }

        /// <summary>
        /// Initializes the exponential envelope. Creates the regions and associated bounds.
        /// </summary>
        /// <param name="initialCuts">Initial cuts.</param>
        /// <param name="initialPoints">Initial points.</param>
        protected override void InitializeEnvelope(IList<double> initialPoints)
        {
            List<double> newCutPoints = new List<double>();

            int pointCount = initialPoints.Count;
            List<double> logProbs = new List<double>();
            List<double> grads = new List<double>();
            List<double> intercepts = new List<double>();

            // Compute the log prob of each initial point.
            foreach (double point in initialPoints.OrderBy(p => p))
            {
                newCutPoints.Add(point);
                logProbs.Add(this.distr.GetLogProb(point));
            }

            // First pass computes the grads and intercepts for the lines between each point.
            using (IEnumerator<double> points = initialPoints.OrderBy(p => p).GetEnumerator())
            {
                int index = 0;
                points.MoveNext();
                double lastPoint = points.Current;
                double currentLogProb = logProbs[index];

                while (points.MoveNext())
                {
                    double nextLogProb = logProbs[++index];

                    double grad = (nextLogProb - currentLogProb) / (points.Current - lastPoint);
                    double intercept = currentLogProb - lastPoint * grad;
                    grads.Add(grad);
                    intercepts.Add(intercept);

                    lastPoint = points.Current;
                    currentLogProb = logProbs[index];
                }

            }

            // Second pass creates the regions and upperbounds for the envelope.
            using (IEnumerator<double> points = initialPoints.OrderBy(p => p).GetEnumerator())
            {
                int index = 0;
                points.MoveNext();
                double lastPoint = points.Current;

                // Region before the initial points
                Region firstRegion = new Region(DomainInf, lastPoint);
                this.regionList.Add(firstRegion);
                this.regionBoundMap.Add(firstRegion, new LinearBound(intercepts[index], grads[index]));
                index++;
                

                // Iterate over all but last (and first)
                points.MoveNext();
                for (var value = points.Current; points.MoveNext(); value = points.Current)
                {

                    // Take gradient after point for region before point
                    Region backRegion = new Region(lastPoint, value);
                    this.regionList.Add(backRegion);
                    this.regionBoundMap.Add(backRegion, new LinearBound(intercepts[index], grads[index]));

                    // Find intersect
                    if (index < pointCount - 2)
                    {
                        double intercept = (intercepts[index + 1] - intercepts[index - 1]) /
                                       (grads[index - 1] - grads[index + 1]);

                        // Take gradient before point for region after point
                        Region forwardRegion = new Region(value, intercept);
                        this.regionList.Add(forwardRegion);
                        this.regionBoundMap.Add(forwardRegion, new LinearBound(intercepts[index - 1], grads[index - 1]));

                        lastPoint = intercept;
                    }

                    index++;
                }

                // Add region after initial points
                Region lastRegion = new Region(points.Current, DomainSup);
                this.regionList.Add(lastRegion);
                this.regionBoundMap.Add(lastRegion, new LinearBound(intercepts.Last(), grads.Last()));
            }
            this.cutPoints = newCutPoints;                
        }

        /// <summary>
        /// Computes the envelope value of a given sample, under a specified bound.
        /// The sample should be within the region with the specified bound,
        /// but this method does not check for this.
        /// </summary>
        /// <returns>The prob from bound.</returns>
        /// <param name="bound">Bound.</param>
        /// <param name="sample">Sample.</param>
        protected override double LogProbFromBound (LinearBound bound, double sample)
        {
            return bound.Gradient * sample + bound.Intercept; 
        }

        /// <summary>
        /// Updates the region bound.
        /// </summary>
        /// <param name="region">Region.</param>
        protected override void UpdateRegionBound(Region region)
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// Gets the total probability for a region.
        /// </summary>
        /// <returns>The region probability.</returns>
        /// <param name="region">Region.</param>
        protected override double GetRegionProb(Region region)
    	{
            double intercept = this.regionBoundMap[region].Intercept;
            double grad = this.regionBoundMap[region].Gradient;

            return (Math.Exp(intercept + grad * region.UpperBound) - Math.Exp(intercept + grad * region.LowerBound)) / grad;
    	}

        /// <summary>
        /// Samples from the continuous distribution of this envelope, using a given region.
        /// </summary>
        /// <returns>The continuous sample.</returns>
        /// <param name="sampledRegion">Sampled region.</param>
        public override double SampleContinuous(Region sampledRegion)
        {
            // Here we compute the inverse of the exponential CDF (after normalizing).
            double rand = Utils.SRandom.GetUniform();

            double grad = this.regionBoundMap[sampledRegion].Gradient;

            if (sampledRegion.LowerBound == double.NegativeInfinity)
            {
                return (Math.Log(rand) / grad) + sampledRegion.UpperBound;
            }
            else if (sampledRegion.UpperBound == double.PositiveInfinity)
            {
                return (Math.Log(1 - rand) / grad) + sampledRegion.LowerBound;
            }
            else
            {
                double exponent = rand * (Math.Exp(grad * sampledRegion.UpperBound) - Math.Exp(grad * sampledRegion.LowerBound));
                exponent += Math.Exp(grad * sampledRegion.LowerBound);
                return Math.Log(exponent) / grad; 
            }
        }

        public override void AddCutPoint(double cutPoint)
        {
            // Currently very slow, only experimental.
            this.cutPoints.Add(cutPoint);
            InitializeEnvelope(this.cutPoints);
        }

        /// <summary>
        /// Computes the region bound.
        /// </summary>
        /// <returns>The region bound.</returns>
        /// <param name="region">Region.</param>
        private LinearBound ComputeRegionBound(Region region)
        {
            throw new NotImplementedException();
        }     
    }
}
