﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	class FactorGraph {
		
		private List<VariableNode> variables;
		private List<FactorNode> factors;

		// 2 messages per edge (if a tree, this is 2(n-1) where n is node count.)
		private int totalSize;
		private int messageCount;

		public VariableNode RootNode
		{
			get;
			set;
		}

		public bool IsValid() {
			bool valid = true;
			// Should check that we have a bipartite graph.
			throw new NotImplementedException("Currently no validity check in place.");
		}

		public bool IsTree() {
			// Check that we have a tree and can return exact marginals.
			throw new NotImplementedException("Currently no tree check in place.");

			//Do a breadth search and check we never hit the same node twice
		}
	}

}
