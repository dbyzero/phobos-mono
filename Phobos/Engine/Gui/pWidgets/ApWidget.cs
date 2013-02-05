using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Phobos.Engine;
using Phobos.Engine.Inputs.MouseInput;
using Phobos.Engine.Gui.PWidgets.Events;

namespace Phobos.Engine.Gui.PWidgets {
    abstract class APWidget : DrawableGameComponent {

        #region Fields and propreties
        #region Events and delegates

        public delegate void MouseoverChangeHandler( object sender, BooleanChangeEvent e );
        public delegate void ActivatedChangeHandler( object sender, BooleanChangeEvent e );

        public event MouseoverChangeHandler MouseoverChange;
        public event ActivatedChangeHandler ActivatedChange;

        #endregion
        #region Public

        public Rectangle location;
        public Rectangle mouseArea;
        public GameComponentCollection Children;
        public APWidget Parent;

        #endregion
        #region Protected

        protected bool isActivated = true;
        protected bool isMouseover = false;

        #endregion
        #endregion
        #region Constructors / Indexers

        public APWidget( APWidget parent, int x, int y, int width, int height )
            : base( GameEngine.Instance ) {
            this.Parent = parent;
            location = new Rectangle( x, y, width, height );
            mouseArea = location;
        }

        public APWidget( APWidget parent, Rectangle _location )
            : base( GameEngine.Instance ) {
            this.Parent = parent;
            location = _location;
            mouseArea = _location;
        }

        #endregion

        #region Methods

        #region Transformations

        /// <summary>
        /// Applique une translation au Widget ainsi qu'à tous ses enfants.
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public virtual void Translate( int dx, int dy ) {
            this.location.X += dx;
            this.location.Y += dy;

            foreach( APWidget Child in Children ) {
                Child.Translate( dx, dy );
            }
        }

        #endregion
        #region Accessors and mutators
        public bool Activated{
            get {
                return isActivated;
            }
            set {
                if( isActivated != value ) {
                    isActivated = value;
                    OnActivatedChange();
                }
            }
        }

        public bool Mouseover {
            get {
                return isMouseover;
            }
            set {
                if( isMouseover != value ) {
                    isMouseover = value;
                    OnMouseOverChanged();
                }
            }
        }
        #endregion
        #region Events handling

        protected void OnActivatedChange() {
            if( ActivatedChange != null ) {
                ActivatedChange( this, new BooleanChangeEvent( Activated, "Activated" ) );
            }
        }

        protected void OnMouseOverChanged() {
            if( MouseoverChange != null ) {
                MouseoverChange( this, new BooleanChangeEvent( Mouseover, "MouseHover" ) );
            }
        }

        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            base.Update( gameTime );
            //TODO: Verification du layer et du focus.
            Mouseover = mouseArea.Contains( new Point( Mouse.GetState().X, Mouse.GetState().Y ) );

        }

        #endregion
        #endregion
    }
}
