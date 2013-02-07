using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phobos.Engine.Services;
using Microsoft.Xna.Framework.Content;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSCheckBox : APCheckBox {

        #region Fields & Propreties
        protected static Texture2D spriteButton;
        #endregion

        #region Constructors and Indexers
        public PSCheckBox(APWidget parent, int x, int y)
            : base( parent, x, y, 18, 18 ) {

        }

        static PSCheckBox() {
            spriteButton = ServicesManager.GetService<ContentManager>().Load<Texture2D>( @"gui\system\psCheckboxes" );
        }
        #endregion

        #region
        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            Rectangle _source = new Rectangle(0,0,18,18);
            base.Draw( gameTime );
            if( Activated ) {
                if( Mouseover ){
                    _source.Y = 19;
                    if( IsActionKeyPressed ) {
                        _source.X = 19;
                    } else {
                        if( Checked ) {
                            _source.X = 38;
                        }
                    }

                }else{
                    if( Checked ) {
                        _source.X = 38;
                    }
                }
            } else {
                _source.Y = 38;
                if( Checked ) {
                    _source.X = 38;
                }
            }

            GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 18, 18 ), _source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
        }
        #endregion
        #endregion
    }
}
