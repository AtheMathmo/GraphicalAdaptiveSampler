using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph;
using GraphicalAdaptiveSampler.Graph.Algorithm;

namespace GraphicalAdaptiveSampler.Graph.Algorithm.Messages {

	// Message from Variable to Factor
	// Given by product of all messages going into variable.
	class V_F_Message : IMessage {

		private VariableNode variable;
		private FactorNode factor;

		public void ComputeMessage()
		{
			// Find the neighbours of the variable
			// Get their incoming messages to the variable
			// Product of the incoming messages
			// Return the new function which is the product of the other functions
		}

	}

}