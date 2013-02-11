using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Phobos.Engine.Gui.PWidgets.Events;

namespace Phobos.Engine.Gui.PWidgets {
    abstract class APButton : APWidget {
        #region Propreties & Fields
        #region Events and delegates

        public delegate void ActionHandler( APButton sender, ActionEvent e );

        public event ActionHandler Action;
        public event ActionHandler ActionKeyPressed;
        public event ActionHandler ActionKeyReleased;
        public event ActionHandler ActionKeyStatusChanged;

        #endregion
        #region Fields

        protected bool isActionKeyPressed = false;

        #endregion
        #endregion
        #region Constructors & Indexer

        public APButton( int x, int y, int width, int height )
            : base( x, y, width, height ) {

        }

        public APButton( APWidget parent, Rectangle _location )
            : base( _location ) {

        }
        #endregion
        #region Methods
        #region Accessors and mutators
        public bool IsActionKeyPressed {
            get {
                return isActionKeyPressed;
            }
            set {
                if( isActionKeyPressed != value ) {
                    isActionKeyPressed = value;
                    if( value ) {
                        OnActionKeyPressed();
                        OnActionKeyStatusChanged();
                    } else {
                        OnActionKeyReleased();
                        OnActionKeyStatusChanged();
                        OnAction();
                    }
                }
            }
        }
        #endregion
        #region Events handling
        protected void OnAction() {
            if( Action != null ) {
                Action( this, new ActionEvent() );
            }
        }

        protected void OnActionKeyPressed() {
            if( ActionKeyPressed != null ) {
                ActionKeyPressed( this, new ActionEvent() );
            }
        }

        protected void OnActionKeyReleased() {
            if( ActionKeyReleased != null ) {
                ActionKeyReleased( this, new ActionEvent() );
            }
        }

        protected void OnActionKeyStatusChanged() {
            if( ActionKeyStatusChanged != null ) {
                ActionKeyStatusChanged( this, new ActionEvent() );
            }
        }
        #endregion

        #region IUpdatable

        public override void Update( GameTime gameTime ) {
            base.Update( gameTime );
            if( IsVisible && IsEnabled && IsMouseover ) {
                if( Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed ) {
                    IsActionKeyPressed = true;
                } else if( IsActionKeyPressed == true ) {
                    IsActionKeyPressed = false;
                }
            }

        }
        #endregion
        #endregion
    }
}
