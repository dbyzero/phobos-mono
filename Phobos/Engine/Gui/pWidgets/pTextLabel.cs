using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine;
using Phobos.Engine.Content;
using Microsoft.Xna.Framework.Content;

namespace Phobos.Engine.Gui.pWidgets {
    class pTextLabel : ApLabel {

        #region Fields and propreties

        #region Public
        public Color color;
        protected static SpriteFont spriteFont;
        public TextAlign textAlign;
        public enum TextAlign { RIGHT, LEFT, CENTER }
        #endregion

        #region Private
        private Rectangle textLocation;
        #endregion

        #endregion

        #region Constructeurs
        public pTextLabel( ApWidget parent, int x, int y, int width, int height, string text, Color color, TextAlign align = TextAlign.CENTER )
            : base( parent, x, y, width, height, text ) {
            textAlign = align;
            
            textLocation = location;
            Vector2 textSize = spriteFont.MeasureString( text );

            while( textSize.X > width ) {
                text = text.Substring( 0, text.Count() - 2 );
                textSize = spriteFont.MeasureString( text );
            }

            if( this.textAlign == TextAlign.RIGHT ) {
                textLocation.X = y + 128 - (int) ( textSize.X );
            } else if( this.textAlign == TextAlign.CENTER ) {
                textLocation.X = x + 64 - (int) ( textSize.X / 2 );
            } else {
                textLocation.X = x;
            }
            textLocation.Y = y + 16 - (int) ( textSize.Y / 2 );

            #region Basic Events
            TextChanged += delegate( object sender, TextChangedEvent e ) {
                String txt = e.newText;
                Vector2 size = spriteFont.MeasureString( txt );

                while( size.X > width ) {
                    txt = txt.Substring( 0, txt.Count() - 2 );
                    size = spriteFont.MeasureString( txt );
                }
                if( this.textAlign == TextAlign.RIGHT ) {
                    textLocation.X = location.X + 128 - (int) ( size.X );
                } else if( this.textAlign == TextAlign.CENTER ) {
                    textLocation.X = location.X + 64 - (int) ( size.X / 2 );
                } else {
                    textLocation.X = location.X;
                }
                textLocation.Y = location.Y + 16 - (int) ( size.Y / 2 );
            };
            #endregion
            this.color = color;
        }

        static pTextLabel() {
            spriteFont = ContentHelper.Load<SpriteFont>( @"fonts\Corbel14Bold" );
        }
        #endregion

        #region Méthodes surchargée
        public override void Draw( GameTime gameTime ) {
            if( !this.Parent.isActivated ) {
                GameEngine.spriteBatch.DrawString( spriteFont, Text, new Vector2( textLocation.X, textLocation.Y ), Color.Gray);
            } else {
                GameEngine.spriteBatch.DrawString( spriteFont, Text, new Vector2( textLocation.X, textLocation.Y ), Color.White);
            }
        }

        #endregion

    }
}
