using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine;
using Phobos.Engine.Content;
using Microsoft.Xna.Framework.Content;
using Phobos.Engine.Gui.PWidgets.Events;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSTextLabel : APLabel {

        #region Fields and propreties

        #region Protected
        protected static SpriteFont spriteFont;
        protected Rectangle textLocation;
        #endregion

        #endregion

        #region Constructors and Indexers
        public PSTextLabel( APWidget parent, int x, int y, int width, int height, string text)
            : base( parent, x, y, width, height, text ) {
            Text = text.Trim();

            Vector2 textSize = spriteFont.MeasureString( Text );

            while( textSize.X > width ) {
                Text = Text.Substring( 0, text.Count() - 2 );
                textSize = spriteFont.MeasureString( Text );
            }

            textLocation.X = x + (int) ( ( width - textSize.X ) / 2 );
            textLocation.Y = y + (int) ( ( height - textSize.Y ) / 2 );

            #region Basic Events
            TextChange += delegate( object sender, StringChangeEvent e ) {
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

        static PSTextLabel() {
            spriteFont = ContentHelper.Load<SpriteFont>( @"fonts\Corbel14Normal" );
        }
        #endregion

        #region Methods
        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            if( Parent.Activated ) {
                GameEngine.spriteBatch.DrawString( spriteFont, Text, new Vector2( textLocation.X, textLocation.Y ), Color.White );
            } else {
                GameEngine.spriteBatch.DrawString( spriteFont, Text, new Vector2( textLocation.X, textLocation.Y ), Color.Gray );
            }

        }
        #endregion
        #endregion

    }
}
