using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	class FactorGraph {
		// This will consist of a List of Variable Nodes and Edges.

		private List<VariableNode> variableNodes;
		private List<Edge> edges;

		private VariableNode rootNode;

		public bool IsValid() {
			// Should check that we have a bipartite graph.
			throw new NotImplementedException("Currently no validity check in place.");
		}

		public bool IsTree() {
			// Check that we have a tree and can return exact marginals.
			throw new NotImplementedException("Currently no tree check in place.");
		}
	}

}
