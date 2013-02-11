using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets {
    abstract class APTextField : APLabel {
        #region Fields and propreties
        #region Events and delegates
        #endregion
        #region Public
        #endregion
        #region Protected
        protected bool hasFocus = false;
        #endregion
        #endregion
        #region Constructors / Indexers
        public APTextField( int x, int y, int width, int height, string text)
            : base( x, y, width, height, text ) {

        }
        #endregion

        #region Methods
        #region Accessors and mutators
        #endregion
        #region Events handling
        #endregion
        #region IUpdateable
        public override void Update( Microsoft.Xna.Framework.GameTime gameTime ) {
            base.Update( gameTime );
            
        }
        #endregion
        #endregion
    }
}
