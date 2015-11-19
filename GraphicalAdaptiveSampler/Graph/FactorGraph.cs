using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	class FactorGraph {
		// Adjacency list representing bipartite graph
		private Dictionary<Node, List<Node>> adjList;

		private int rootNodeIndex;

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
