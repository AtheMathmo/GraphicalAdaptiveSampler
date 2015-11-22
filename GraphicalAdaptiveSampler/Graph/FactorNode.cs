using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph.Algorithm.Messages;

namespace GraphicalAdaptiveSampler.Graph {

	// This node represents a factor in the factor graph
	public class FactorNode : Node {
		
		// Will contain a Factor object which knows the parents and children (and the density).

        private F_V_Message[] outMessages;

        public void InitializeMessages()
        {
            this.outMessages = new F_V_Message[this.NeighbourCount];
        }
	}

}
