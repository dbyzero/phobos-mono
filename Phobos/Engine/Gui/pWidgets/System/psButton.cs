using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phobos.Engine.Content;
using Phobos.Engine.Services;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSButton : APButton {

        #region Fields & Properties
        #region Fields

        protected static Texture2D spriteButton;
        protected PSTextLabel label;

        #endregion
        #region Properties

        public string ButtonText {
            get {
                return label.Text;
            }
            set {
                label.Text = value;
            }
        }

        #endregion
        #endregion
        #region Constructors & Indexer
        public PSButton( int x, int y, string text )
            : base( x, y, 128, 35 ) {
            label =
                new PSTextLabel( 0, 0, 128, 35, text );
            label.Parent = this;
            if(parent != null)
            Console.WriteLine( "PSButton::" + this.ButtonText + "[" + x + ";" + y + "]@[" + parent.AbsoluteLocation.X + ";" + parent.AbsoluteLocation.Y + "]" );
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
            if( IsVisible ) {
                if( IsEnabled ) {
                    if( IsActionKeyPressed && IsMouseover ) {
                        GameEngine.spriteBatch.Draw( spriteButton, AbsoluteLocation, new Rectangle( 0, 70, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                    } else if( IsMouseover ) {
                        GameEngine.spriteBatch.Draw( spriteButton, AbsoluteLocation, new Rectangle( 0, 35, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                    } else {
                        GameEngine.spriteBatch.Draw( spriteButton, AbsoluteLocation, new Rectangle( 0, 0, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );

                    }
                } else {
                    GameEngine.spriteBatch.Draw( spriteButton, AbsoluteLocation, new Rectangle( 0, 105, 128, 35 ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                }
    
                label.Draw( gameTime );
            }
            
        }
        #endregion
        #endregion
    }
}
