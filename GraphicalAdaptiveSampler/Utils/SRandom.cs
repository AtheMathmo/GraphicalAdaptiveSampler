using System;

namespace GraphicalAdaptiveSampler.Utils
{
    /// <summary>
    /// Internal random number generator (NOT THREAD SAFE).
    /// </summary>
    public static class SRandom
    {
        private static Random rand = new Random();

        /// <summary>
        /// Gets a sample from the Uniform[0,1] distribution.
        /// </summary>
        /// <returns>Uniform[0,1] sample.</returns>
        public static double GetUniform()
        {
            return rand.NextDouble();
        }

        /// <summary>
        /// Gets or sets the seed for the random generator.
        /// </summary>
        /// <value>The seed.</value>
        public static int Seed
        {
            get
            {
                return Seed;
            }

            set 
            {
                Seed = value;
                rand = new Random(Seed);
            }
        }
    }
}

