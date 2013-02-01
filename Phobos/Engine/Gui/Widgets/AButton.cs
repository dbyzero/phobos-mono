using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Phobos.Gui.Widgets
{
    abstract class AButton : AWidget
    {
        #region Propriétés publiques

        public new Rectangle location;

        #endregion

        #region Constructeurs

        public AButton(AWidget parent, int x, int y, int width, int height)
            : base(parent, x, y)
        {
            this.location = new Rectangle(x, y, width, height);
        }

        #endregion

        #region Méthodes surchargée
        
        #endregion
    }
}
