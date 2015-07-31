using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler
{
    class Program
    {
        static void Main(string[] args)
        {
            // The idea is that I will abuse log concavity to envelope the distribution with exponential bounds.
            // Then to compute the discretization I will use the integral of the exponential to weight each region:
            // exp(r_j)-exp(l_j)/sum(exp(r_i)-exp(l_i)
            // Ensuring that for infinite regions we have a negative gradient.
        }
    }
}
