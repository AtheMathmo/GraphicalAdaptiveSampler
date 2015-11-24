using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Algorithm.Messages;

namespace GraphicalAdaptiveSampler.Graph {

	// This node represents a factor in the factor graph
	public abstract class Node {
		
        public Dictionary<Node, Message> OutMessages
        {
            get;
            set;
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
