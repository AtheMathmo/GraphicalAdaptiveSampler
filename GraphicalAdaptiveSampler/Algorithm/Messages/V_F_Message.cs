using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph;
using GraphicalAdaptiveSampler.Algorithm;

namespace GraphicalAdaptiveSampler.Algorithm.Messages {

	// Message from Variable to Factor
	// Given by product of all messages going into variable.
	class V_F_Message : Message {

        private VariableNode variable;
        private FactorNode factor;

        public V_F_Message(VariableNode variable, FactorNode factor)
        {
            this.factor = factor;
            this.variable = variable;
        }

        public override void ComputeMessage()
        {
            // Find the neighbours of the variable
            // Get their incoming messages to the variable
            // Product of the incoming messages
            // Return the new function which is the product of the other functions

            FactorNode[] neighbours = (FactorNode[]) variable.Neighbours;

            List<F_V_Message> incomingMessages = new List<F_V_Message>();

            foreach (FactorNode neighbour in neighbours)
            {
                if (neighbour == factor)
                {
                    continue;
                }

                incomingMessages.Add((F_V_Message) neighbour.OutMessages[factor]);
            }

            //TODO: use incoming messages to complete message computation.

        }

	}

}