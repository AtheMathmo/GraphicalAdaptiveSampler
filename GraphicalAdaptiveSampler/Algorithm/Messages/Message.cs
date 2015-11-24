using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph;

namespace GraphicalAdaptiveSampler.Algorithm.Messages {

	// Interface for message
	public abstract class Message {

		protected VariableNode variable;
		protected FactorNode factor;

		public Func<double, double>	MessageFunc
		{
			get;
			set;
		}

		// Rather than void this should return a function.
		public abstract void ComputeMessage();

	}

}