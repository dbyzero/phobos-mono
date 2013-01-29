using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phobos.GUI
{
    class GUILayerFactory : ALayerFactory
    {

        private static GUILayerFactory singleton;

        private GUILayerFactory() : base()
        {
            this.depthIncrement = 0.001f; //Pas dépasser le 3e digit pour éviter les erreurs d'arrondis et irrationnels binaires.
            this.maxLayerDepth = 1f - this.depthIncrement;
            this.minLayerDepth = 0f;
            this.lastUsedDepth = 0f;
        }

        public static GUILayerFactory Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new GUILayerFactory();
                }
                return singleton;
            }
        }

        public SortedList<float, Layer> GUILayers
        {
            get
            {
                SortedList<float, Layer> temp = new SortedList<float,Layer>();

                foreach (KeyValuePair<float, Layer> d in ALayerFactory.layerList)
                {
                    if (d.Key >= this.minLayerDepth && d.Key <= this.maxLayerDepth)
                    {
                        temp.Add(d.Key, d.Value);
                    }
                }

                return temp;
            }
        }
    }
}
