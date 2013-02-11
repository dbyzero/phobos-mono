using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Gui.PWidgets.System;

namespace Phobos.Engine.Gui.PWidgets {
    class APRadioGroup : APContainer{

        #region Fields & properties

        #endregion
        #region Constructors & Indexers
        public APRadioGroup( int x, int y, int width, int height )
            : base( x, y, width, height ) {
            
        }
        #endregion
        #region Methods

        public void SetSelectedRadioButton( APRadioButton radio ) {
            foreach( PSRadioButton button in children ) {
                if( button == radio ) {
                    button.Selected = true;
                } else if( ( button as PSRadioButton ).Selected ) {
                    button.Selected = false;
                }
            }
        }
        #region IDrawable

        public override void Draw( GameTime gameTime ) {
            foreach( APWidget child in children ) {
                child.Draw( gameTime );
            }
        }

        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            base.Update( gameTime );

            foreach( APWidget child in children ) {
                child.Update( gameTime );
            }
        }

        #endregion
        #endregion

    }
}
