using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph;
using GraphicalAdaptiveSampler.Graph.Algorithm.Messages;

namespace GraphicalAdaptiveSampler.Graph.Algorithm {

	class BPAlgorithm {

		// Steps:

		// 1. Get the root node
		// 2. Map outwards from the root node to build the tree.
		// 3. Schedule the messages:
		//		- Start from the furthest leaf on each branch and push messages towards the root until each branch reaches root.
		//		- Start from the root and push messages out towards the leafs until all leafs are reached.
        // 4. Compute messages
		// 5. Get marginals by taking product of messages

		private FactorGraph factorGraph;

		// For scheduling messages towards root
		private Stack<IMessage> inSchedule;

		// For scheduling messages out from root
        private Queue<IMessage> outSchedule;



		public BPAlgorithm(FactorGraph factorGraph)
		{
			this.factorGraph = factorGraph;
		}

		public void Run()
		{
			// Schedule messages
			MapFactorGraph();
		}

		private void MapFactorGraph()
		{
			Queue<Node> searchQueue = new Queue<Node>();

            searchQueue.Enqueue(this.factorGraph.RootNode);

            while (searchQueue.Count > 0)
			{
                Node v = searchQueue.Dequeue();

				foreach (Node u in v.Neighbours)
				{
					// Queue messages from v to u
                    this.outSchedule.Enqueue(v.OutMessages[u]);

                    // Stack messages from u to v
                    this.inSchedule.Push(u.OutMessages[v]);

                    searchQueue.Enqueue(u);
				}
			}
		}

	}

}