using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	abstract class FactorNode : Node {
		// Represents a factor within the factor graph.

		public Variable Child
		{
			get;
			private set;
		}

		public Variable[] Parents
		{
			get;
			private set;
		}

		abstract double Density;
	}

}
