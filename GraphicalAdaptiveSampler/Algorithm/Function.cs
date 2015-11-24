using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Algorithm.Messages;

namespace GraphicalAdaptiveSampler.Algorithm {

	public class FunctionConstructor {

		// This is the framework for products of messages.
		// We will update this to use lists (for types)
		public void ProductFunc(List<IMessage> messages)
		{
			// Basic idea.
			//Func<T, T, double> replacFunc = delegate(T orig, T newVar) {
			//	return func(orig)*newFunc(newVar);
			//};

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