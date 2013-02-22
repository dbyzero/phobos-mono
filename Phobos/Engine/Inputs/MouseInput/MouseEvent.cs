using System;
using Phobos.Engine.Inputs.MouseInput;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.Inputs.MouseInput {
    public class MouseEvent : EventArgs {
        public readonly MouseState currentMouseState;
        public readonly MouseState previousMouseState;
        public readonly GameTime gameTime;
        public MouseEvent( GameTime _gameTime, MouseState _previousMouseState, MouseState _currentMouseState ) {
            currentMouseState = _currentMouseState;
            previousMouseState = _previousMouseState;
            gameTime = _gameTime;
        }
    }
}
