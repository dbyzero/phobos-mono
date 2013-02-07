using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phobos.Engine.Content;
using Phobos.Engine.Services;
using Microsoft.Xna.Framework.Content;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSButton : APButton {

        #region Fields & Properties
        protected static Texture2D spriteButton;
        protected PSTextLabel label;

        public string ButtonText {
            get {
                return label.Text;
            }
            set {
                label.Text = value;
            }
        }

        #endregion

        #region Constructors & Indexer
        public PSButton( APWidget parent, int x, int y, string text )
            : base( parent, x, y, 128, 35 ) {
            label =
                new PSTextLabel( this, x, y, 128, 35, text);

            Children = new GameComponentCollection();
            Children.Add( label );

            
        }
        
        /// <summary>
        /// Contient tous les chargements de textures.
        /// </summary>
        static PSButton() {
            spriteButton = ServicesManager.GetService<ContentManager>().Load<Texture2D>( @"gui\system\psButton" );
        }
        #endregion

        #region Methods

        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            if( Activated ) {
                if( IsActionKeyPressed && Mouseover) {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 128, 35 ), new Rectangle( 0, 70, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                } else if( Mouseover ) {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 128, 35 ), new Rectangle( 0, 35, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                } else {
                    GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 128, 35 ), new Rectangle( 0, 0, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );

                }
            } else {
                GameEngine.spriteBatch.Draw( spriteButton, new Rectangle( location.X, location.Y, 128, 35 ), new Rectangle( 0, 105, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
            }

            foreach( APWidget child in Children ) {
                child.Draw( gameTime );
            }
        }
        #endregion
        #endregion
    }
}
