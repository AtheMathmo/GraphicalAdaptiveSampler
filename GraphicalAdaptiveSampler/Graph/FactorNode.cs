using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	abstract class FactorNode : Node {
		// Represents a factor within the factor graph.

		abstract double Density();
	}

}
