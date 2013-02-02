using System;
using Phobos.Engine.Inputs.MouseInput;
using Microsoft.Xna.Framework.Input;

namespace Phobos.Engine.Inputs.MouseInput {
    public class MouseEvent : EventArgs {
        MouseState mouseState;

        public MouseEvent( MouseState _mouseState ) {
            mouseState = _mouseState;
        }
    }
}
