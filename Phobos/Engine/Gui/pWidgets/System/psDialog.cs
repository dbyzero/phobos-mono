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
    class psDialog : ApDialog {

        protected static Texture2D dialogSprite_focus;

        public psDialog( ApWidget parent, Rectangle location )
            : base( parent, location ) {

        }

        static psDialog() {

        }

        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime ) {
            if( HasFocus ) {
                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X, location.Y, 16, 16 ), new Rectangle( 0, 47, 16, 16 ), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0f );
                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X + 16, location.Y, location.Width - 32, 16 ), new Rectangle( 16, 47, 1, 16 ), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0f );
                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X + location.Width - 16, location.Y, 16, 16 ), new Rectangle( 47, 47, 16, 16 ), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0f );

                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X, location.Y+16, 16, location.Height - 32 ), new Rectangle( 0, 34, 16, 16 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X + 16, location.Y+16, location.Width - 32, location.Height - 32 ), new Rectangle( 32, 34, 1, 1 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X + location.Width - 16, location.Y+16, 16, location.Height - 32 ), new Rectangle( 47, 34, 16, 16 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );

                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X, location.Y+location.Height - 16, 16, 16 ), new Rectangle( 0, 47, 16, 16 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X + 16, location.Y+location.Height - 16, location.Width - 32, 16 ), new Rectangle( 16, 47, 1, 16 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                GameEngine.spriteBatch.Draw( dialogSprite_focus, new Rectangle( location.X + location.Width - 16, location.Y+location.Height - 16, 16, 16 ), new Rectangle( 47, 47, 16, 16 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
            }
        }
    }
}
