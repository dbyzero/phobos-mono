using System;
using System.Collections.Generic;


namespace Phobos.Engine.Gui {
    class GuiLayerFactory : ALayerFactory {

        public GuiLayerFactory(pUILevel level)
            : base() {
            depthIncrement = 0.001f; //Pas dépasser le 3e digit pour éviter les erreurs d'arrondis et irrationnels binaires.
            maxLayerDepth = 5f - this.depthIncrement;
            switch( level ) {
                case pUILevel.BACKGROUND:
                    maxLayerDepth = 5f - this.depthIncrement;
                    minLayerDepth = 0f;
                    lastUsedDepth = 0f;
                    break;
                case pUILevel.CONTROLS:
                case pUILevel.DIALOG:
                case pUILevel.FOCUS:
                case pUILevel.SYSTEM:
                case pUILevel.SYSTEM_DIALOG:
                    maxLayerDepth = 1f - this.depthIncrement;
                    minLayerDepth = 0f;
                    lastUsedDepth = maxLayerDepth;
                    break;
            }
            
        }

        public SortedList<float, Layer> GuiLayers {
            get {
                SortedList<float, Layer> temp = new SortedList<float, Layer>();

                foreach (KeyValuePair<float, Layer> d in ALayerFactory.layerList) {
                    if (d.Key >= this.minLayerDepth && d.Key <= this.maxLayerDepth) {
                        temp.Add(d.Key, d.Value);
                    }
                }

                return temp;
            }
        }
    }
}
