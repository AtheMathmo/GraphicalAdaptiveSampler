using System;

namespace GraphicalAdaptiveSampler.Utils
{
    public static class SRandom
    {
        private static Random rand = new Random();



        public static double GetUniform()
        {
            return rand.NextDouble();
        }

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

