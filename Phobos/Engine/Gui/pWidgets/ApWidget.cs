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

namespace Phobos.Engine.Gui.pWidgets {
    abstract class ApWidget : DrawableGameComponent {

        #region Fields and propreties
        #region Events and delegates
        public delegate void MouseHoverHandler( object sender, MouseHoverEvent e );
        public delegate void ActivateStatusHandler( object sender, ActivatedStatusEvent e );

        public event MouseHoverHandler MouseEntering;
        public event MouseHoverHandler MouseLeaving;
        public event MouseHoverHandler MouseHoverChanged;
        public event MouseHoverHandler MouseMoved;
        public event ActivateStatusHandler ActivatedStatusChanged;
        public event ActivateStatusHandler Activated;
        public event ActivateStatusHandler Desactivated;
        #endregion

        #region Public

        public Rectangle location;
        public Rectangle mouseArea;
        public GameComponentCollection Children;
        public ApWidget Parent;
        public bool isActivated = true;
        public bool isMouseOver = false;
        public Layer layer;

        #endregion

        #region Protected

        #endregion

        #endregion

        #region Constructors / Indexers

        public ApWidget( ApWidget parent, int x, int y, int width, int height )
            : base( GameEngine.Instance ) {
            this.Parent = parent;
            location = new Rectangle( x, y, width, height );
            mouseArea = location;
        }

        public ApWidget( ApWidget parent, Rectangle _location )
            : base( GameEngine.Instance ) {
            this.Parent = parent;
            location = _location;
            mouseArea = _location;
        }

        #endregion

        #region Events

        public class ActivatedStatusEvent : EventArgs {
            public bool activated;
            public ActivatedStatusEvent( bool _activated ) {
                activated = _activated;
            }
        }

        public class MouseHoverEvent : EventArgs {
            public bool hover;
            public MouseHoverEvent( bool isMouseHover ) {
                hover = isMouseHover;
            }
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

            foreach( ApWidget Child in Children ) {
                Child.Translate( dx, dy );
            }
        }

        #endregion

        protected void OnActivation() {
            if( Activated != null ) {
                Activated( this, new ActivatedStatusEvent( true ) );
            }
        }

        protected void OnDesactivation() {
            if( Desactivated != null ) {
                Desactivated( this, new ActivatedStatusEvent( false ) );
            }
        }

        protected void OnActivatedStatusChanged() {
            if( ActivatedStatusChanged != null ) {
                ActivatedStatusChanged( this, new ActivatedStatusEvent( isActivated ) );
            }
        }

        protected void OnMouseEntering() {
            if( MouseEntering != null ) {
                MouseEntering( this, new MouseHoverEvent( true ) );
            }
        }

        protected void OnMouseLeaving() {
            if( MouseLeaving != null ) {
                MouseLeaving( this, new MouseHoverEvent( false ) );
            }
        }

        protected void OnMouseHoverChanged() {
            if( MouseHoverChanged != null ) {
                MouseHoverChanged( this, new MouseHoverEvent( isMouseOver ) );
            }
        }

        public abstract override void Draw( GameTime gameTime );

        public override void Update( GameTime gameTime ) {
            base.Update( gameTime );
            //TODO: Verification du layer et du focus.
            if( mouseArea.Contains( new Point( Mouse.GetState().X, Mouse.GetState().Y ) ) ) {
                if( !isMouseOver ) {
                    isMouseOver = true;
                    OnMouseHoverChanged();
                    OnMouseEntering();
                }
            } else {
                if( isMouseOver ) {
                    this.isMouseOver = false;
                    OnMouseHoverChanged();
                    OnMouseLeaving();
                }
            }
        }

        #endregion
    }
}
