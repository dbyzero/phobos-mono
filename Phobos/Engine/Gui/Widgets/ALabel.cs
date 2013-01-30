using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Phobos.Engine;

//TODO: text and font changed Event management
namespace Phobos.Gui.Widgets
{
    abstract class ALabel : AWidget
    {

        #region Propriétés publiques

        public string text;
        public string font;

        #endregion

        #region Constructeurs

        public ALabel(AWidget parent, int x, int y, string text, string font)
            : base(parent, x, y) {
            this.text = text;
            this.font = font;
        }

        #endregion

        #region Méthodes surchargée


        #endregion

    }
}
