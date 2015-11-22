using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph.Algorithm.Messages;

namespace GraphicalAdaptiveSampler.Graph {

	// This node represents a factor in the factor graph
	abstract class Node {
		
        public Dictionary<IMessage, Node> OutMessages
        {
            get;
        }

		public Node[] Neighbours
		{
			get;
			set;
		}

        public int NeighbourCount
        {
            get
            {
                return this.Neighbours.Length;
            }
        }


    }
}
