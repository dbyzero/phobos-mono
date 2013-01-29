using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Phobos.GUI.Widgets
{
    abstract class AButton : AWidget
    {
        #region Propriétés publiques

        public ALabel Label;

        #endregion

        #region Constructeurs

        public AButton(AWidget parent, Point position, Point size, ALabel label)
            : base(parent)
        {
            this.Location = new Rectangle(position.X, position.Y,size.X,size.Y);

        }

        public AButton(AWidget parent, int x, int y, int width, int height)
            : base(parent)
        {
            this.Location = new Rectangle(x, y, width, height);

            
        }

        public AButton(AWidget parent, Rectangle location, ALabel label)
            : base(parent)
        {
            this.Location = location;
        }

        #endregion

        

        #region Accessors / Mutators

        /// <summary>
        /// Acces au text du bouton.
        /// </summary>
        public string Text
        {
            get
            {
                return Label.Text;
            }
            set
            {
                Label.Text = value;
            }
        }

        #endregion
    }
}
