using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	class Edge {
		// This will connect Variable Nodes to each other, and describe which is the parent and child.

		private VariableNode parentNode;
		private VariableNode childNode;

		public Edge(VariableNode parent, VariableNode child) {
			this.parentNode = parent;
			this.childNode = child;
		}
	}

}
