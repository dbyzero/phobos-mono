using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Phobos.Engine.Gui.pWidgets
{
    abstract class ApButton : ApWidget
    {
        #region Propreties & Fields
        #region Events

        public delegate void ActionHandler( object sender, ActionEvent e );
        public event ActionHandler Action;
        public class ActionEvent : EventArgs {
            public MouseState mouse;
            public ActionEvent() {
                mouse = new MouseState();
            }
        }

        #endregion

        #region Public
        public bool IsActionKeyPressed = false;
        #endregion

        #region Protected

        #endregion

        #endregion

        #region Constructeurs

        public ApButton(ApWidget parent, int x, int y, int width, int height)
            : base(parent, x, y, width, height)
        {
            location = new Rectangle(x, y, width, height);
            MouseHoverChanged += delegate( object sender, ApWidget.MouseHoverEvent e ) {
                if( isActivated ) {
                    if( e.hover ) {
                        isMouseOver = true;
                    } else {
                        isMouseOver = false;
                    }
                }
            };
        }

        #endregion

        #region Methods
        protected void OnAction() {
            if( Action != null ) {
                Action( this, new ActionEvent() );
            }
        }

        public override void Update( GameTime gameTime ) {
            base.Update( gameTime );
            if( isActivated && isMouseOver ) {
                if( Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed ) {
                    IsActionKeyPressed = true;
                } else if( IsActionKeyPressed == true ) {
                    OnAction();
                    IsActionKeyPressed = false;
                }
            }

        }
        #endregion
    }
}
