using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.pWidgets {
    class pWindow : ApWidget {

        #region Fields & Propreties

        #endregion

        #region Constructors / Indexers
        public pWindow( ApWidget parent, int x, int y, int width, int height )
            : base( parent, x, y, width, height ) {

        }

        static pWindow() {

        }
        #endregion

        #region Methods

        #region Overriding
        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime ) {
            throw new NotImplementedException();
        }

        public override void Update( Microsoft.Xna.Framework.GameTime gameTime ) {
            base.Update( gameTime );
        }
        #endregion
        #endregion
    }
}
