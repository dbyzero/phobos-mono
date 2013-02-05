using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Gui.PWidgets.System;

namespace Phobos.Engine.Gui.PWidgets {
    class APRadioGroup : APWidget{

        #region Fields & propreties

        #endregion

        #region Constructors & Indexers
        public APRadioGroup( APWidget parent, int x, int y, int width, int height )
            : base( parent, x, y, width, height ) {
            Children = new GameComponentCollection();
        }
        #endregion

        #region Methods
        public void AddRadioButton( APRadioButton radio ) {
            Children.Add( radio );
        }

        public void SetSelectedRadioButton( APRadioButton radio ) {
            foreach( PSRadioButton button in Children ) {
                if( button == radio ) {
                    button.Selected = true;
                } else if( ( button as PSRadioButton ).Selected ) {
                    button.Selected = false;
                }
            }
        }

        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            base.Draw( gameTime );

            foreach( APWidget child in Children ) {
                child.Draw( gameTime );
            }
        }
        #endregion

        #region IUpdateable
        public override void Update( GameTime gameTime ) {
            base.Update( gameTime );

            foreach( APWidget child in Children ) {
                child.Update( gameTime );
            }
        }
        #endregion
        #endregion

    }
}
