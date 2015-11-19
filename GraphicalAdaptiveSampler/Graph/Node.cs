using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	class Node {

		public IEnumerable<Node> Parents
		{
			get;
			set;
		}

		public IEnumerable<Node> Children
		{
			get;
			set;
		}
	}
	
}