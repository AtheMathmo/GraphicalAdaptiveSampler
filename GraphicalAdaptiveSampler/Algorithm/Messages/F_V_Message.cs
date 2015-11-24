using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph;
using GraphicalAdaptiveSampler.Algorithm;

namespace GraphicalAdaptiveSampler.Algorithm.Messages {

	// Message from factor to variable
	// Given by summation (excluding variable) of product of messages into factor and factor.
	class F_V_Message : IMessage {

		private VariableNode variable;
		private FactorNode factor;

        public F_V_Message(FactorNode factor, VariableNode variable)
        {
            this.factor = factor;
            this.variable = variable;
        }

		public void ComputeMessage()
		{
			// Find the neighbours of the factor
			// Get their incoming messages to the factor
			// Product of the incoming messages with the factor
			// Sum over this product (all variables except V)
			// Return this new function

            VariableNode[] neighbours = (VariableNode[]) factor.Neighbours;

            List<V_F_Message> incomingMessages = new List<V_F_Message>();

            foreach (VariableNode neighbour in neighbours)
            {
                if (neighbour == variable)
                {
                    continue;
                }

                incomingMessages.Add((V_F_Message) neighbour.OutMessages[factor]);
            }

            //TODO: use incoming messages to complete message computation.

        }
		
		
	}

}