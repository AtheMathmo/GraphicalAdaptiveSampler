using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	class VariableNode {
		// This node represents a variable and it's associated factor. This makes up the nodes of the factor graph.
		// Each Variable Node will consist of a Factor and a Variable.

		private Variable variable;
		private Factor factor;

		public VariableNode(Variable variable, Factor factor) {
			this.variable = variable;
			this.factor = factor;
		}
	}

}
