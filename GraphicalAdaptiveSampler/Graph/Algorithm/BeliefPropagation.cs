using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph;

namespace GraphicalAdaptiveSampler.Graph.Algorithm {

	class BPAlgorithm {

		// Steps:

		// 1. Get the root node
		// 2. Map outwards from the root node to build the tree.
		// 3. Schedule the messages:
		//		- Start from the furthest leaf on each branch and push messages towards the root until each branch reaches root.
		//		- Start from the root and push messages out towards the leafs until all leafs are reached.

		// 4. Get marginals by taking product of messages

		private FactorGraph factorGraph;

		// For scheduling messages towards root
		private Stack<Node> inSchedule;

		// For scheduling messages out from root
		private Stack<Node> outSchedule;



		public BPAlgorithm(FactorGraph factorGraph)
		{
			this.factorGraph = factorGraph;
		}

		public void Run()
		{
			// Schedules messages and computes them
		}

		private void MapFactorGraph()
		{
			VariableNode rootNode = factorGraph.RootNode;
			// Idea: Go out from the root pushing to a stack.
			// Then pop off the top of the stack and compute messages (whilst pushing to a new stack to reverse the order for the way back).
		}

	}

}