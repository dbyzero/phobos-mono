using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.GUI.Widgets
{
    abstract class AWidget : IDrawable
    {
        public static SpriteFont DefaultFont;
        
        public Rectangle Location;
        public Layer Layer;
        public bool Enabled;
        public AWidget Parent;
        public IList<AWidget> Childrend;
        public bool Visible;
        

        public AWidget(AWidget parent)
        {
            this.Parent = parent;
            this.Enabled = true;
            this.Visible = true;
        }

        #region Transformations
        /// <summary>
        /// Applique une translation au Widget ainsi qu'à tous ses enfants.
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Translate(int dx, int dy)
        {
            this.Location.X += dx;
            this.Location.Y += dy;
            foreach (AWidget child in Childrend)
            {
                child.Translate(dx, dy);
            }
        }

        /// <summary>
        /// Applique une translation au Widget ainsi qu'à tous ses enfants.
        /// </summary>
        /// <param name="translation">Un point définisant la translation</param>
        public void Translate(Point translation)
        {
            this.Location.X += translation.X;
            this.Location.Y += translation.Y;
            foreach (AWidget child in Childrend)
            {
                child.Translate(translation);
            }
        }

        #endregion

        #region Accesseur / Mutateurs


        public int X
        {
            get
            {
                return Location.X;
            }
            set
            {
                Location.X = value;
            }
        }

        public int Y
        {
            get
            {
                return Location.Y;
            }
            set
            {
                Location.Y = value;
            }
        }

        public int Width
        {
            get
            {
                return Location.Width;
            }
            set
            {
                Location.Width = value;
            }
        }

        public int Height
        {
            get
            {
                return Location.Height;
            }
            set
            {
                Location.Height = value;
            }
        }
        #endregion


        #region Implémentation de IDrawable
        public abstract void Draw(GameTime gameTime);

        public int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;

        bool IDrawable.Visible
        {
            get { return Visible; }
        }

        public event EventHandler<EventArgs> VisibleChanged;
        #endregion
    }
}
