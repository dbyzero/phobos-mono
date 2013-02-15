using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets {
    abstract class APDialog : APContainer {
        #region Propreties & Fields

        #endregion
        #region Constructors & Indexer

        public APDialog( int x, int y, int width, int height, GUILayer layer )
            : base( x, y, width, height ) {
                this.layer = layer;
        }
        #endregion
        #region Methods
        public override void Add( APWidget item ) {
            base.Add( item );
            if( layer != null ) {
                layer.Register( item );
            }
        }

        #endregion
    }
}
