using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Algorithm {

	class Function<T, TResult> {
		private Func<T, TResult> func;

		// This class will contain a delegate which is used by the messages.
		// We will provide methods to take products and sums of these delegates.
		// THIS IS CURRENTLY NOT IMPLEMENTED

		// This is the framework for products of messages.
		// We will update this to use lists (for types)
		public void ProductFunc(Func<T2, TResult> newFunc)
		{
			// Basic idea.
			Func<T, T2, TResult> replacFunc = delegate(T orig, T2 newVar) {
				return func(orig)*newFunc(newVar);
			};
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