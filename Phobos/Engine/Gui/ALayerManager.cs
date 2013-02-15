using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phobos
{
    abstract class ALayerManager
    {
        protected float minLayerDepth; //0 minimum, correspond au plus proche
        protected float maxLayerDepth; //1f maximum

        public static SortedDictionary<float, GUILayer> layerList;
        protected float lastUsedDepth;
        protected float depthIncrement;

        static ALayerManager()
        {
            ALayerManager.layerList = new SortedDictionary<float, GUILayer>();
        }

        public GUILayer BuildLayer(string layerName = "")
        {
            lastUsedDepth += depthIncrement;
            GUILayer l = new GUILayer(lastUsedDepth, layerName);
            layerList.Add(lastUsedDepth, l);
            return l;
        }


        
    }
}
