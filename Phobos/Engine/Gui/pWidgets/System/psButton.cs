using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phobos.Engine.Content;

namespace Phobos.Engine.Gui.pWidgets.System {
    class psButton : ApButton {

        protected static Texture2D spriteButton;
        public psTextLabel label;

        public psButton( ApWidget parent, int x, int y, string text )
            : base( parent, x, y, 128, 35 ) {
            label =
                new psTextLabel( this, x, y, 128, 35, text);

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
            spriteButton = ContentHelper.Load<Texture2D>( @"gui\system\psButton" );
        }

        public override void Draw( GameTime gameTime ) {
            if( isActivated ) {
                if( IsActionKeyPressed && isMouseOver) {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 128, 35 ), new Rectangle( 0, 70, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                } else if( isMouseOver ) {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 128, 35 ), new Rectangle( 0, 35, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                } else {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 128, 35 ), new Rectangle( 0, 0, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );

                }
            } else {
                GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 128, 35 ), new Rectangle( 0, 0, 128, 35 ), Color.Gray, 0f, Vector2.Zero, SpriteEffects.None, 0f );
            }

            foreach( ApWidget child in Children ) {
                child.Draw( gameTime );
            }
        }
    }
}
