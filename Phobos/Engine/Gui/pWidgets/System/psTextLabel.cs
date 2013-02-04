using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine;
using Phobos.Engine.Content;
using Microsoft.Xna.Framework.Content;

namespace Phobos.Engine.Gui.pWidgets.System {
    class psTextLabel : ApLabel {

        #region Fields and propreties

        #region Protected
        protected static SpriteFont spriteFont;
        protected static Color color;
        protected Rectangle textLocation;
        #endregion

        #region Private
        
        #endregion

        #endregion

        #region Constructeurs
        public psTextLabel( ApWidget parent, int x, int y, int width, int height, string text)
            : base( parent, x, y, width, height, text ) {
            text = text.Trim();
            if( parent.isActivated ) {
                color = Color.White;
            } else {
                color = Color.Gray;
            }
            Vector2 textSize = spriteFont.MeasureString( text );

            while( textSize.X > width ) {
                text = text.Substring( 0, text.Count() - 2 );
                textSize = spriteFont.MeasureString( text );
            }

            textLocation.X = x + (int) ( ( width - textSize.X ) / 2 );
            textLocation.Y = y + (int) ( ( height - textSize.Y ) / 2 );

            #region Basic Events
            TextChanged += delegate( object sender, TextChangedEvent e ) {
                String txt = e.newText.Trim();
                Vector2 size = spriteFont.MeasureString( txt );

                while( size.X > width ) {
                    txt = txt.Substring( 0, txt.Count() - 2 );
                    size = spriteFont.MeasureString( txt );
                }

                textLocation.X = location.X + (int) ( ( location.Width - size.X ) / 2 );
                textLocation.Y = location.Y + (int) ( ( location.Height - size.Y ) / 2 );
            };
            #endregion
        }

        static psTextLabel() {
            spriteFont = ContentHelper.Load<SpriteFont>( @"fonts\Corbel14Normal" );
        }
        #endregion

        #region Méthodes surchargée
        public override void Draw( GameTime gameTime ) {
            GameEngine.spriteBatch.DrawString( spriteFont, Text, new Vector2( textLocation.X, textLocation.Y ), color );
        }

        #endregion

    }
}
