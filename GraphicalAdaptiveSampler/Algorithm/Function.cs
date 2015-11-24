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
		/// <returns>The product of all messages.</returns>
		public Func<double, double> ProductFunc(List<Message> messages)
		{
			// Construct the product of the message functions
			Func<double, double> productFunc = delegate(double x)
			{
				double productValue = 1.0;
				foreach(Message message in messages)
				{
					productValue *= message.MessageFunc(x);
				}
				return productValue;
			};

			return productFunc;

		}

		public void Summarize()
		{
			//Func<T, TResult> = delegate(T orig) {
			//	Loop through each value for each variable
			//  Sum them up and return the result as a function of orig.
			//};
		}


	}

}