using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphicalAdaptiveSampler.Graph;

namespace GraphicalAdaptiveSampler.Graph.Algorithm.Messages {

	// Interface for message
	public interface IMessage {

		// Rather than void this should return a function.
		void ComputeMessage();

	}

}