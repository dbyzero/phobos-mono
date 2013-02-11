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
    abstract class APWidget {

        #region Fields & Properties
        #region Events & delegates

        public delegate void MouseoverChangedHandler( APWidget sender, EventArgs e );
        public delegate void EnabledChangedHandler( APWidget sender, EventArgs e );
        public delegate void VisibleChangedHandler( APWidget sender, EventArgs e );

        public event MouseoverChangedHandler MouseoverChanged;
        public event MouseoverChangedHandler MouseEntered;
        public event MouseoverChangedHandler MouseLeaved;
        public event EnabledChangedHandler EnabledChanged;
        public event EnabledChangedHandler Enabled;
        public event EnabledChangedHandler Disabled;
        public event VisibleChangedHandler VisibleChanged;
        public event VisibleChangedHandler Shown;
        public event VisibleChangedHandler Hidden;

        #endregion
        #region Properties
        public bool IsEnabled {
            get {
                return isEnabled;
            }
            set {
                if( isEnabled != value ) {
                    isEnabled = value;
                    OnEnabledChange();
                    if( value ) {
                        OnEnabled();
                    } else {
                        OnDisabled();
                    }
                }
            }
        }

        public bool IsMouseover {
            get {
                return isMouseover;
            }
            set {
                if( isMouseover != value ) {
                    isMouseover = value;
                    OnMouseoverChanged();
                    if( value ) {
                        OnMouseEntered();
                    } else {
                        OnMouseLeaved();
                    }
                }
            }
        }

        public bool IsVisible {
            get { 
                return isVisible; 
            }
            set {
                if( isVisible != value ) {
                    isVisible = value;
                    OnVisibleChange();
                    if( value ) {
                        OnShown();
                    } else {
                        OnHidden();
                    }
                }
            }
        }

        public Rectangle Location {
            get {
                return location;
            }
            set {
                if( location != value ) {
                    location = value;
                }
            }
        }

        public APWidget Parent {
            get {
                return parent;
            }
            set {
                if( parent != value ) {
                    parent = value;
                }
            }
        }

        #endregion
        #region Fields

        protected APWidget parent = null;
        protected Rectangle location;
        protected bool isEnabled = true;
        protected bool isMouseover = false;
        protected bool isVisible = true;

        #endregion
        #endregion
        #region Constructors & Indexer

        public APWidget( int x, int y, int width, int height ) {
            location = new Rectangle( x, y, width, height );
        }

        public APWidget( Rectangle _location ) {
            location = _location;
        }

        #endregion
        #region Methods
        #region Location & Transformations

        /// <summary>
        /// Applique une translation au Widget ainsi qu'à tous ses enfants.
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public virtual void Translate( int dx, int dy ) {
            location.X += dx;
            location.Y += dy;
        }

        public Rectangle AbsoluteLocation {
            get {
                if( parent == null ) {
                    return Location;
                } else {

                    return new Rectangle(
                        parent.AbsoluteLocation.X + Location.X,
                        parent.AbsoluteLocation.Y + Location.Y,
                        Location.Width,
                        Location.Height );

                }
            }
        }

        #endregion
        #region Events handling

        protected void OnEnabledChange() {
            if( EnabledChanged != null ) {
                EnabledChanged( this, EventArgs.Empty );
            }
        }

        protected void OnEnabled(){
            if( Enabled != null ) {
                Enabled( this, EventArgs.Empty );
            }
        }

        protected void OnDisabled() {
            if( Disabled != null ) {
                Disabled( this, EventArgs.Empty );
            }
        }

        protected void OnShown() {
            if( Shown != null ) {
                Shown( this, EventArgs.Empty );
            }
        }

        protected void OnHidden() {
            if( Hidden != null ) {
                Hidden( this, EventArgs.Empty );
            }
        }

        protected void OnVisibleChange() {
            if( VisibleChanged != null ) {
                VisibleChanged( this, EventArgs.Empty );
            }
        }

        protected void OnMouseoverChanged() {
            if( MouseoverChanged != null ) {
                MouseoverChanged( this, new SimpleValueChangedEvent<bool>( IsMouseover ) );
            }
        }

        protected void OnMouseLeaved() {
            if( MouseLeaved != null ) {
                MouseLeaved( this, new SimpleValueChangedEvent<bool>( IsMouseover ) );
            }
        }

        protected void OnMouseEntered() {
            if( MouseEntered != null ) {
                MouseEntered( this, new SimpleValueChangedEvent<bool>( IsMouseover ) );
            }
        }


        #endregion
        #region IUpdateable

        public virtual void Update( GameTime gameTime ) {
            //TODO: Verification du layer et du focus.
            IsMouseover = AbsoluteLocation.Contains( new Point( Mouse.GetState().X, Mouse.GetState().Y ) );
        }

        #endregion
        #region IDrawable

        public abstract void Draw( GameTime gameTime );

        #endregion
        #endregion
    }
}
