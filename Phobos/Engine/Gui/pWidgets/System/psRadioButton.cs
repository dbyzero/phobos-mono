using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Content;
using Phobos.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSRadioButton : APRadioButton {
        #region Fields & Propreties
        protected static Texture2D spriteButton;
        #endregion

        #region Constructors and Indexers
        public PSRadioButton( APWidget parent, int x, int y )
            : base( parent, x, y, 18, 18 ) { }

        static PSRadioButton() {
            spriteButton = ServicesManager.GetService<ContentManager>().Load<Texture2D>( @"gui\system\psRadioButton" );
        }
        #endregion

        #region
        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            base.Draw( gameTime );
            Rectangle _source = new Rectangle( 0, 0, 18, 18 );

            if( IsEnabled ) {
                if( IsMouseover ) {
                    _source.Y = 19;
                    if( IsActionKeyPressed ) {
                        _source.X = 19;
                    } else {
                        if( Selected ) {
                            _source.X = 38;
                        }
                    }

                } else {
                    if( Selected ) {
                        _source.X = 38;
                    }
                }
            } else {
                _source.Y = 38;
                if( Selected ) {
                    _source.X = 38;
                }
            }

            GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 18, 18 ), _source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
        }
        #endregion


        #endregion

    }
}
