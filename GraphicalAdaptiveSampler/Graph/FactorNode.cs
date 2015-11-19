using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Graph {

	// This node represents a factor in the factor graph
	abstract class FactorNode {
		
		public VariableNode ChildVariable
		{
			get;
			set;
		}

		public VariableNode[] ParentVariables
		{
			get;
			set;
		}

		// Still figuring this out...
		abstract double Density();
	}

}
