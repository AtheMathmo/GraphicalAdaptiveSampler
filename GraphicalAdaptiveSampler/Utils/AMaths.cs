using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Utils
{
    public static class AMaths
    {
        public const double LN_2_PI = 1.83787706640934;

        public static double LnGamma(double x)
        {
            double c1 = 1.0/12;
            double c2 = 1.0/360;
            double c3 = 1.0/1260;

            double result = x * Math.Log(x) - x - 0.5 * Math.Log(x) + 0.5 * LN_2_PI;
            result += c1 / x - c2 / Math.Pow(x, 3) + c3 / Math.Pow(x, 5);
            return result;
        }
    }
}
