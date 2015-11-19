using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	class FactorGraph {
		
		private List<VariableNode> variables;
		private List<FactorNode> factors;

		public VariableNode RootNode
		{
			get;
			set;
		}

		public bool IsValid() {
			bool valid = true;
			// Should check that we have a bipartite graph.
			throw new NotImplementedException("Currently no validity check in place.");

			foreach (Node node in adjList.Keys)
			{
				// Check type of node is different to each type in list
			}
		}

		public bool IsTree() {
			// Check that we have a tree and can return exact marginals.
			throw new NotImplementedException("Currently no tree check in place.");
		}
	}

}
