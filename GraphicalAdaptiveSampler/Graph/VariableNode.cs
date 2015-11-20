using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	// This node represents a random variable in the factor graph
	abstract class VariableNode : Node {
		
		public FactorNode ParentFactor
		{
			get;
			set;
		}

		public FactorNode[] ChildFactors
		{
			get;
			set;
		}
	}

}
