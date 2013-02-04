using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Phobos.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Content;
using Phobos.Engine.Inputs.MouseInput;

namespace Phobos.Engine.Gui.pWidgets {
    class pButton : ApButton {

        #region Propreties & Fields


        #region Public

        public string text {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }
        public ApLabel label;
        #endregion

        #region Protected

        protected static Texture2D spriteButton;
        protected static Texture2D spriteButton_down;
        protected static Texture2D spriteButton_hover;
        protected static Texture2D spriteButton_disabled;

        #endregion

        #region Private

        #endregion

        #endregion

        #region Constructeurs
        public pButton( ApWidget parent, int x, int y, string text )
            : base( parent, x, y, 128, 32 ) {

            label =
                new pTextLabel( this, location.X, location.Y, 128, 32, text, Color.White );

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

        /// <summary>
        /// Le constucteur statique contient le chargement des ressources.
        /// </summary>
        static pButton() {
            
        }

        #endregion

        #region Methods

        #endregion

        #region Implémentation de IDrawable

        public override void Draw( GameTime gameTime ) {
            if( isActivated ) {
                if( IsActionKeyPressed ) {
                    GameEngine.spriteBatch.Draw( spriteButton_down, this.location, null, Color.White );
                } else if( isMouseOver ) {
                    GameEngine.spriteBatch.Draw( spriteButton_hover, this.location, null, Color.White );
                } else {
                    GameEngine.spriteBatch.Draw( spriteButton, this.location, null, Color.White );
                }
            } else {
                GameEngine.spriteBatch.Draw( spriteButton_disabled, this.location, null, Color.Gray );
            }

            foreach( ApWidget child in Children ) {
                child.Draw( gameTime );
            }
        }
        #endregion


    }
}
