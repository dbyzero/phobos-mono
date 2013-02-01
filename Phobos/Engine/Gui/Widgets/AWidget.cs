using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine;

namespace Phobos.Gui.Widgets {
    abstract class AWidget : DrawableGameComponent{

        #region Propriétés

        public Point location;
        public IList<AWidget> children;
        public AWidget parent;

        #endregion

        #region Constructeurs

        public AWidget(AWidget parent, int x, int y) : base(GameEngine.Instance){
            this.parent = parent;
            this.location = new Point(x, y);
        }

        #endregion

        #region Méthodes publiques
        #region Transformations

        /// <summary>
        /// Applique une translation au Widget ainsi qu'à tous ses enfants.
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public virtual void Translate(int dx, int dy) {
            this.location.X += dx;
            this.location.Y += dy;

            foreach (AWidget child in children) {
                child.Translate(dx, dy);
            }
        }

        #endregion

        public abstract override void Draw(GameTime gameTime);

        #endregion
    }
}
