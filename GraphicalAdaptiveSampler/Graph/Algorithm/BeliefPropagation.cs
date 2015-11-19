using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph.Algorithm {

	class BeliefPropagation {

		// Steps:

		// 1. Get the root node
		// 2. Map outwards from the root node to build the tree.
		// 3. Schedule the messages:
		//		- Start from the furthest leaf on each branch and push messages towards the root until each branch reaches root.
		//		- Start from the root and push messages out towards the leafs until all leafs are reached.

		// 4. Get marginals by taking product of messages

	}

}