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
using Phobos.Engine.Services;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSTextLabel : APLabel {

        #region Fields and propreties
        #region Fields

        protected static SpriteFont spriteFont;
        protected Rectangle textLocation;

        #endregion
        #endregion

        #region Constructors and Indexers
        public PSTextLabel( int x, int y, int width, int height, string _text)
            : base( x, y, width, height, _text ) {
            _text = _text.Trim();

            Vector2 textSize = spriteFont.MeasureString( _text );

            while( textSize.X > width ) {
                Text = Text.Substring( 0, _text.Count() - 2 );
                textSize = spriteFont.MeasureString( Text );
            }

            textLocation.X = x + (int) ( ( width - textSize.X ) / 2 );
            textLocation.Y = y + (int) ( ( height - textSize.Y ) / 2 );

            #region Basic Events
            TextChanged += delegate( APLabel sender , EventArgs e) {

                Vector2 size = spriteFont.MeasureString( text );

                while( size.X > width ) {
                    text = text.Substring( 0, text.Count() - 2 );
                    size = spriteFont.MeasureString( text );
                }

                textLocation.X = Location.X + (int) ( ( Location.Width - size.X ) / 2 );
                textLocation.Y = Location.Y + (int) ( ( Location.Height - size.Y ) / 2 );
            };
            #endregion
        }

        static PSTextLabel() {
            spriteFont = ServicesManager.GetService<ContentManager>().Load<SpriteFont>( @"fonts\Corbel14Normal" );
        }
        #endregion

        #region Methods
        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            if( parent.IsEnabled ) {
                GameEngine.spriteBatch.DrawString( spriteFont, Text, new Vector2( AbsoluteLocation.X + textLocation.X, AbsoluteLocation.Y + textLocation.Y ), Color.White );
            } else {
                GameEngine.spriteBatch.DrawString( spriteFont, Text, new Vector2( AbsoluteLocation.X + textLocation.X, AbsoluteLocation.Y + textLocation.Y ), Color.Gray );
            }

        }
        #endregion
        #endregion

    }
}
