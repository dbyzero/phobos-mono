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

namespace Phobos.Gui.Widgets
{
    class SimpleTextLabel : ALabel
    {

        #region Propriétés publiques

        public Color color;

        #endregion

        #region Constructeurs
        public SimpleTextLabel(AWidget parent, int x, int y, string text, string font, Color color)
            : base(parent, x, y, text, font)
        {
            ContentHelper.Load<SpriteFont>( font );
            this.color = color;
            this.font = font;
        }
        #endregion

        #region Méthodes surchargée
        public override void Draw(GameTime gameTime) {
            if( this.parent is SimpleButton && ( this.parent as SimpleButton ).status == SimpleButton.ButtonState.DISABLED ) {
                GameEngine.spriteBatch.DrawString( ContentHelper.Get<SpriteFont>( font ), text, new Vector2( this.location.X, this.location.Y ), Color.Gray );
            } else {
                GameEngine.spriteBatch.DrawString( ContentHelper.Get<SpriteFont>( font ), text, new Vector2( this.location.X, this.location.Y ), Color.White );
            }
        }

        #endregion
        
    }
}
