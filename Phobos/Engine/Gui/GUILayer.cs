using Phobos.Engine.Gui.PWidgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phobos {
    class GUILayer {
        protected float layerDepth;
        protected string layerName;
        protected List<APWidget> registered;

        public List<APWidget> RegistredWidgets {
            get {
                return registered;
            }
        }

        public GUILayer( float _layerDepth, string _layerName = "" ) {
            layerDepth = _layerDepth;
            layerName = _layerName;
            registered = new List<APWidget>();
        }

        public void Register( APWidget widget ) {
            
            registered.Add( widget );
        }

    }
}
