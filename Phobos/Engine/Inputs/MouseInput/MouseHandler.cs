using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Phobos.Engine;
using Phobos.Engine.Content;

namespace Phobos.Engine.Inputs.MouseInput {
    class MouseHandler {
        public static Texture2D cursorSprite;
        public static CursorType currentType;

        public enum CursorType{SIMPLE, WAITING, ACTION}

        public static void Initialize() {
            currentType = CursorType.SIMPLE;
            cursorSprite = ContentHelper.Load<Texture2D>( @"gui\cursors_basic" );
        }

        public static void DrawCursor(){
            GameEngine.spriteBatch.Begin();
            switch( currentType ) {
                case CursorType.SIMPLE:
                    GameEngine.spriteBatch.Draw( cursorSprite, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Rectangle( 0, 0, 32, 32 ), Color.White);
                    break;
                case CursorType.WAITING:
                    GameEngine.spriteBatch.Draw( cursorSprite, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Rectangle( 32, 0, 32, 32 ), Color.White);
                    break;
                case CursorType.ACTION:
                    GameEngine.spriteBatch.Draw( cursorSprite, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Rectangle( 64, 0, 32, 32 ), Color.White);
                    break;
            }
            GameEngine.spriteBatch.End();
        }
    }
}
