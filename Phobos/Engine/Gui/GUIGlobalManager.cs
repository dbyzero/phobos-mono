using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Phobos.Engine.GameStates;
using Phobos.Engine.Gui.PWidgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Phobos.Engine.Gui {
    static class GUIGlobalManager {
        static Dictionary<GameStateList, Dictionary<GUIStratas, GUIStrataManager>> globalStrataManagers;

        static GUIGlobalManager() {
            globalStrataManagers = new Dictionary<GameStateList, Dictionary<GUIStratas, GUIStrataManager>>();
        }

        public static List<GUIStrataManager> GetGUIStrataManager( GUIStratas strata ) {
            List<GUIStrataManager> temp = new List<GUIStrataManager>();
            foreach( int state in Enum.GetValues( typeof( GameStateList ) ) ) {
                if( GameStateManager.GetGameState( (GameStateList) state ) != null
                    && GameStateManager.GetGameState( (GameStateList) state ).Status == GameStateStatus.Active ) {

                    if( globalStrataManagers[ (GameStateList) state ] != null
                        && globalStrataManagers[ (GameStateList) state ][ strata ] != null ) {
                        temp.Add( globalStrataManagers[ (GameStateList) state ][ strata ] );
                    }

                }
            }
            return temp;
        }


        public static void UpdateMouseover() {
            Point mouseLoc = new Point( Mouse.GetState().X, Mouse.GetState().Y );
            foreach( KeyValuePair<float, GUILayer> layer in ALayerManager.layerList.Reverse()) {
                foreach( APWidget widget in layer.Value.RegistredWidgets ) {
                    if(widget.AbsoluteMouseoverArea.Contains(mouseLoc)){

                        widget.IsMouseover = true;
                        return;
                    }
                }
            }
        }
    }
}
