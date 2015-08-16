using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Utils
{
    /// <summary>
    /// Contains efficient advanced maths functions.
    /// </summary>
    public static class AMaths
    {
        public const double LN_2_PI = 1.83787706640934;

        /// <summary>
        /// Computes Log-Gamma of x using Stirlings approximation.
        /// </summary>
        /// <returns>The gamma.</returns>
        /// <param name="x">The x coordinate.</param>
        public static double LnGamma(double x)
        {
            // n = 1, 2, 3, 4, 5, 6
            // B2n = 1/6, -1/30, 1/42, -1/30, 5/66, -691/2730
            // divided by 2n(2n-1)

            // cn = 1/12, -1/360, 1/1260, -1/1680, 1/1188, -691/360360
            double c1 = 1.0/12;
            double c2 = -1.0/360;
            double c3 = 1.0/1260;
            double c4 = -1.0 / 1680;
            double c5 = 1 / 1188;
            double c6 = -691 / 360360;

            // Mirror property to get more accurate estimation of small values.
            if (x < 2)
            {
                return LnGamma(x + 2) - Math.Log(x + 1) - Math.Log(x);
            }

            double result = (x - 0.5) * Math.Log(x) - x + 0.5 * LN_2_PI;

            result += c1 / x + c2 / Math.Pow(x, 3) + c3 / Math.Pow(x, 5);
            result += c4 / Math.Pow(x, 7) + c5 / Math.Pow(x, 9) + c6 / Math.Pow(x, 11);
            return result;
        }
    }
}
