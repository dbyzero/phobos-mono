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
using Phobos.Engine.Services;

namespace Phobos.Engine.Gui.PWidgets {

    abstract class APWidget {


        #region Fields & Properties
        #region Events & delegates

        public delegate void MouseoverChangedHandler( APWidget sender, EventArgs e );
        public delegate void EnabledChangedHandler( APWidget sender, EventArgs e );
        public delegate void VisibleChangedHandler( APWidget sender, EventArgs e );
        public delegate void LocationChangedHandler( APWidget sender, ValueChangedEvent<Rectangle> e );

        public event MouseoverChangedHandler MouseoverChanged;
        public event MouseoverChangedHandler MouseEntered;
        public event MouseoverChangedHandler MouseLeaved;
        public event EnabledChangedHandler EnabledChanged;
        public event EnabledChangedHandler Enabled;
        public event EnabledChangedHandler Disabled;
        public event VisibleChangedHandler VisibilityChanged;
        public event VisibleChangedHandler Shown;
        public event VisibleChangedHandler Hidden;
        public event LocationChangedHandler LocationChanged;

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
                    OnLocationChange( value, location );
                    location = value;
                }
            }
        }

        public Rectangle MouseoverArea {
            get {
                return mouseoverArea;
            }
            set {
                if( mouseoverArea != value ) {
                    mouseoverArea = value;
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
                    if( parent.Layer != null ) {
                        RegisterToLayer( parent.Layer );
                    }

                }
            }
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

        public GUILayer Layer {
            get {
                return layer;
            }
            set {
                if( layer != value ) {
                    layer = value;
                    value.Register( this );
                }
            }
        }

        public Rectangle AbsoluteMouseoverArea {
            get {
                if( parent == null ) {
                    return mouseoverArea;
                } else {
                    return new Rectangle(
                        parent.AbsoluteMouseoverArea.X + mouseoverArea.X,
                        parent.AbsoluteMouseoverArea.Y + mouseoverArea.Y,
                        mouseoverArea.Width,
                        mouseoverArea.Height );
                }
            }
        }

        #endregion
        #region Fields

        protected APWidget parent = null;
        protected Rectangle location;
        protected Rectangle mouseoverArea;
        protected bool isEnabled = true;
        protected bool isMouseover = false;
        protected bool isVisible = true;
        protected GUILayer layer;

        #endregion
        #endregion
        #region Constructors & Indexer

        public APWidget( int x, int y, int width, int height ) {
            location = new Rectangle( x, y, width, height );
            mouseoverArea = location;

            #region Base event
            LocationChanged += delegate( APWidget sender, ValueChangedEvent<Rectangle> e ) {
                MouseoverArea = new Rectangle(
                    MouseoverArea.X + e.newValue.X - e.previousValue.X,
                    MouseoverArea.Y + e.newValue.Y - e.previousValue.Y,
                    MouseoverArea.Width + e.newValue.Width - e.previousValue.Width,
                    MouseoverArea.Height + e.newValue.Height - e.previousValue.Height );
            };
            #endregion
        }

        public APWidget( Rectangle _location ) {
            location = _location;
            mouseoverArea = _location;
        }

        #endregion
        #region Methods
        #region Transformations

        /// <summary>
        /// Apply a basic translation to the widget.
        /// </summary>
        /// <remarks>
        /// If the widget implements children, you have to override this method to call Translate on each child.
        /// </remarks>
        /// <param name="dx"><typeparamref name="int"/></param>
        /// <param name="dy"><typeparamref name="int"/></param>
        public virtual void Translate( int dx, int dy ) {
            Rectangle result = new Rectangle( location.X + dx, location.Y + dy, location.Width, location.Height );
            OnLocationChange( result, location );

        }

        #endregion
        #region Events handling
        protected void OnLocationChange( Rectangle _newValue, Rectangle _previousValue ) {
            if( LocationChanged != null ) {
                LocationChanged( this, new ValueChangedEvent<Rectangle>( _newValue, _previousValue ) );
            }
        }

        protected void OnEnabledChange() {
            if( EnabledChanged != null ) {
                EnabledChanged( this, EventArgs.Empty );
            }
        }

        protected void OnEnabled() {
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
            if( VisibilityChanged != null ) {
                VisibilityChanged( this, EventArgs.Empty );
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
        #region Layer

        public void RegisterToLayer( GUILayer layer ) {
            layer.Register( this );
        }

        #endregion
        #region IUpdateable

        public virtual void Update( GameTime gameTime ) {
            //TODO: Verification du layer et du focus.

            IsMouseover = AbsoluteMouseoverArea.Contains( new Point( Mouse.GetState().X, Mouse.GetState().Y ) );
        }

        #endregion
        #region IDrawable

        public abstract void Draw( GameTime gameTime );

        #endregion
        #endregion
    }
}
