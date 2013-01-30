using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phobos
{
    abstract class ALayerFactory
    {
        protected float minLayerDepth; //0 minimum, correspond au plus proche
        protected float maxLayerDepth;
        protected static SortedList<float, Layer> layerList;
        protected float lastUsedDepth;
        protected float depthIncrement;

        protected ALayerFactory()
        {
            ALayerFactory.layerList = new SortedList<float, Layer>();
        }

        public Layer BuildLayer(string layerName = "")
        {
            lastUsedDepth += depthIncrement;
            Layer l = new Layer(lastUsedDepth, layerName);
            layerList.Add(lastUsedDepth, l);
            return l;
        }

        
    }
}
