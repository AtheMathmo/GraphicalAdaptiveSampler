using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph;
using GraphicalAdaptiveSampler.Graph.Algorithm;

namespace GraphicalAdaptiveSampler.Graph.Algorithm.Messages {

	// Message from factor to variable
	// Given by summation (excluding variable) of product of messages into factor and factor.
	class F_V_Message : IMessage {

		private VariableNode variable;
		private FactorNode factor;

		public void ComputeMessage()
		{
			// Find the neighbours of the factor
			// Get their incoming messages to the factor
			// Product of the incoming messages with the factor
			// Sum over this product (all variables except V)
			// Return this new function
		}
		
		
	}

}