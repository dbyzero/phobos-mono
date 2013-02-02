using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phobos.Engine.Content;

namespace Phobos.Engine.Gui.pWidgets.System {
    class psButton : ApButton {

        protected static Texture2D spriteButton;
        private psTextLabel label;

        public psButton( ApWidget parent, int x, int y, int width, string text )
            : base( parent, x, y, width, 32 ) {
            label =
                new psTextLabel( this, x, y, width, 32, text, Color.Black );

            Children = new GameComponentCollection();
            Children.Add( label );

            #region Bases events
            this.MouseHoverChanged += delegate( object sender, MouseHoverEvent e ) {
                if( isActivated ) {
                    if( e.hover ) {
                        isMouseOver = true;
                    } else {
                        isMouseOver = false;
                    }
                }
            };
            #endregion
        }

        static psButton() {
            spriteButton = ContentHelper.Load<Texture2D>( @"gui\system\button" );
        }

        public override void Draw( GameTime gameTime ) {
            if( isActivated ) {
                if( IsActionKeyPressed && isMouseOver) {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 32, 32 ), new Rectangle( 32, 0, 32, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X + 32, location.Y, location.Width-64, 32 ), new Rectangle( 48, 0, 1, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X + location.Width - 32, location.Y, 32, 32 ), new Rectangle( 32, 0, 32, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f );
                } else if( isMouseOver ) {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 32, 32 ), new Rectangle( 64, 0, 32, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X + 32, location.Y, location.Width - 64, 32 ), new Rectangle( 80, 0, 1, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );                    
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X + location.Width - 32, location.Y, 32, 32 ), new Rectangle( 64, 0, 32, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f );

                } else {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 32, 32 ), new Rectangle( 0, 0, 32, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X + 32, location.Y, location.Width - 64, 32 ), new Rectangle( 16, 0, 1, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );                    
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X + location.Width - 32, location.Y, 32, 32 ), new Rectangle( 0, 0, 32, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f );

                }
            } else {
                GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 32, 32 ), new Rectangle( 96, 0, 32, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X + 32, location.Y, location.Width - 64, 32 ), new Rectangle( 112, 0, 1, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );                    
                GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X + location.Width - 32, location.Y, 32, 32 ), new Rectangle( 96, 0, 32, 32 ), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f );
            }

            foreach( ApWidget child in Children ) {
                child.Draw( gameTime );
            }
        }
    }
}
