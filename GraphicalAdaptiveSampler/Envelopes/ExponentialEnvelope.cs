using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalAdaptiveSampler.Envelopes
{
    class ExponentialEnvelope : Envelope
    {
        public ExponentialEnvelope()
        {

        }


        public override void Sample()
        {
            List<double> probWeights = new List<double>();

            foreach (Region region in this.regionList)
            {

            }
            
        }
    }
}
