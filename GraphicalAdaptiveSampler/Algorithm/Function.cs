using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Algorithm.Messages;

namespace GraphicalAdaptiveSampler.Algorithm {

	public class FunctionConstructor {

		/// <summary>
		/// Computes the product of messages coming into a variable
		/// </summary>
		/// <param name="messages">The messages to be producted.</param>
		/// <returns>The product function of all messages.</returns>
		public Func<double, double> ProductFunc(List<Message> messages)
		{
			// Construct the product of the message functions
            Func<double, double> productFunc = x => 1.0;

            foreach (Message message in messages)
            {
                Func<double, double> currentFunc = productFunc;
                productFunc = x => currentFunc(x) * message.MessageFunc(x);
            }

            return productFunc;

		}

		public Func<double, double> Summarize(List<Message> messages)
		{
            Func<double, double> summaryFunc = x => 0.0;
			
            // Get all the variables being summed over (from the message variable nodes)

            // loop over variable values
            // [var1, var2, var3]
            // var1 at min, var2 at min, go through var3
            // var1 at min, var2+1, go through var3
            // repeat until all combinations are found
            {

                // loop over messages
                {
                    // take product of messages at this given configuration
                }

                // multiply by factor at this given configuration (as a function of x)

                // add to summaryFunc

            }


		}


	}

}