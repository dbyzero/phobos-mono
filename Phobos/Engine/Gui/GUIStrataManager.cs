using System;
using System.Collections.Generic;


namespace Phobos.Engine.Gui {
    class GUIStrataManager : ALayerManager {

        public GUIStrataManager( GUIStratas level )
            : base() {
            depthIncrement = 0.000001f; //Pas dépasser le 6e digit pour éviter les erreurs d'arrondis et irrationnels binaires.

            switch( level ) {
                case GUIStratas.BACKGROUND:
                    lastUsedDepth = 0f;
                    maxLayerDepth = 0.1f - this.depthIncrement;
                    minLayerDepth = 0f;
                    break;
                case GUIStratas.CONTROLS:
                    lastUsedDepth = 0.1f;
                    maxLayerDepth = 0.2f - this.depthIncrement;
                    minLayerDepth = 0.1f;
                    break;
                case GUIStratas.DEFAULT:
                    lastUsedDepth = 0.3f;
                    maxLayerDepth = 0.4f - this.depthIncrement;
                    minLayerDepth = 0.3f;
                    break;
                case GUIStratas.IMPORTANT:
                    lastUsedDepth = 0.4f;
                    maxLayerDepth = 0.5f - this.depthIncrement;
                    minLayerDepth = 0.4f;
                    break;
                case GUIStratas.DIALOG:
                    lastUsedDepth = 0.5f;
                    maxLayerDepth = 0.6f - this.depthIncrement;
                    minLayerDepth = 0.5f;
                    break;
                case GUIStratas.FULLSCREEN:
                    lastUsedDepth = 0.6f;
                    maxLayerDepth = 0.7f - this.depthIncrement;
                    minLayerDepth = 0.6f;
                    break;
                case GUIStratas.FULLSCREEN_DIALOG:
                    lastUsedDepth = 0.7f;
                    maxLayerDepth = 0.8f - this.depthIncrement;
                    minLayerDepth = 0.7f;
                    break;
                case GUIStratas.SYSTEM:
                    lastUsedDepth = 0.8f;
                    maxLayerDepth = 0.9f - this.depthIncrement;
                    minLayerDepth = 0.8f;
                    break;
                case GUIStratas.SYSTEM_DIALOG:
                    lastUsedDepth = 0.9f;
                    maxLayerDepth = 1f - this.depthIncrement;
                    minLayerDepth = 0.9f;
                    break;
            }

        }

        public GUILayer BuildLayer( GUIStratas level, string layerName = "" ) {
            if( lastUsedDepth + depthIncrement > maxLayerDepth ) 
                throw new Exception( "Le layer '" + System.Enum.GetName( level.GetType(), level ) + "' ne peux plus contenir d'élements" );
            lastUsedDepth += depthIncrement;
            GUILayer l = new GUILayer( lastUsedDepth, layerName );
            layerList.Add( lastUsedDepth, l );

            return l;
        }

        public SortedList<float, GUILayer> StrataLayers {
            get {
                SortedList<float, GUILayer> temp = new SortedList<float, GUILayer>();

                foreach( KeyValuePair<float, GUILayer> d in ALayerManager.layerList ) {
                    if( d.Key >= this.minLayerDepth && d.Key <= this.maxLayerDepth ) {
                        temp.Add( d.Key, d.Value );
                    }
                }

                return temp;
            }
        }
    }
}
