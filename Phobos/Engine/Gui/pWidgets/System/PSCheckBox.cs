using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phobos.Engine.Services;
using Microsoft.Xna.Framework.Content;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSCheckBox : APCheckBox {

        #region Fields & Properties
        protected static Texture2D spriteButton;
        #endregion
        #region Constructors and Indexers
        public PSCheckBox(int x, int y)
            : base( x, y, 18, 18 ) {

        }

        static PSCheckBox() {
            spriteButton = ServicesManager.GetService<ContentManager>().Load<Texture2D>( @"gui\system\psCheckboxes" );
        }
        #endregion
        #region Methods
        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            Rectangle _source = new Rectangle(0,0,18,18);

            if( IsEnabled ) {
                if( IsMouseover ){
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

            GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( AbsoluteLocation.X, AbsoluteLocation.Y, 18, 18 ), _source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
        }
        #endregion
        #endregion
    }
}
