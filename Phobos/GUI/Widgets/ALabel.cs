using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.GUI.Widgets
{
    abstract class ALabel : AWidget
    {

        #region Propriétés publiques

        public string Text { get; set; }
        public SpriteFont Font;

        #endregion

        #region Constructeurs
        public ALabel(AWidget parent, Point position, string text)
            : base(parent)
        {
            Text = text;
        }

        #endregion
    }
}
