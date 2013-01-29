using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.GUI.Widgets
{
    class SimpleTextLabel : ALabel
    {

        #region Constructeurs
        public SimpleTextLabel(AWidget parent, Point position, string text)
            : base(parent, position, text)
        {

        }
        #endregion

        #region Implémentation de IDrawable



        #endregion

        public override void Draw(GameTime gameTime)
        {
            //TODO : SimpleTextLabel::Draw();
        }
    }
}
