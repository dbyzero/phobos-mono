using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine;
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
            this.color = color;
        }
        #endregion

        #region Méthodes surchargée
        public override void Draw(GameTime gameTime) {
            GameEngine.spriteBatch.DrawString(GameEngine.fonts[font], text, new Vector2(this.location.X, this.location.Y), Color.White);
        }
        #endregion
        
    }
}
