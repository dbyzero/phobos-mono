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
    static class MouseHandler {
        #region Fields & Properties

        #region Events & Delegates

        public delegate void MouseEventHandler( MouseEvent e );

        public static event MouseEventHandler LeftButtonChanged;
        public static event MouseEventHandler LeftButtonNotChanged;
        public static event MouseEventHandler LeftButtonPressed;
        public static event MouseEventHandler LeftButtonReleased;

        public static event MouseEventHandler RightButtonChanged;
        public static event MouseEventHandler RightButtonNotChanged;
        public static event MouseEventHandler RightButtonPressed;
        public static event MouseEventHandler RightButtonReleased;

        public static event MouseEventHandler MiddleButtonChanged;
        public static event MouseEventHandler MiddleButtonNotChanged;
        public static event MouseEventHandler MiddleButtonPressed;
        public static event MouseEventHandler MiddleButtonReleased;

        public static event MouseEventHandler ScrollWheelValueChanged;
        public static event MouseEventHandler ScrollWheelValueNotChanged;

        public static event MouseEventHandler XButton1Changed;
        public static event MouseEventHandler XButton1NotChanged;
        public static event MouseEventHandler XButton1Pressed;
        public static event MouseEventHandler XButton1Released;

        public static event MouseEventHandler XButton2Changed;
        public static event MouseEventHandler XButton2NotChanged;
        public static event MouseEventHandler XButton2Pressed;
        public static event MouseEventHandler XButton2Released;

        public static event MouseEventHandler PositionChanged;
        public static event MouseEventHandler PositionNotChanged;
        public static event MouseEventHandler HorizontalyShaked;
        #endregion

        #region Fields
        public static Texture2D cursorSprite;
        public static CursorType currentType;
        public static MouseState previousState;

        private static TimeSpan shakeDelay;
        private static byte shakeDirectionChangeNeeded = 4;
        private static bool goingToLeft = false;
        private static short shakeAmplitude = 25;
        private static int previousRelevanteShakeDirectionXChange;
        private static Queue<TimeSpan> previousDirectionChanges;
        #endregion
        #endregion

        public static void Initialize() {
            shakeDelay = new TimeSpan( (long) ( TimeSpan.TicksPerSecond * 2 ) );
            shakeDirectionChangeNeeded = 4;
            previousDirectionChanges = new Queue<TimeSpan>( shakeDirectionChangeNeeded );
            shakeAmplitude = 25;
            currentType = CursorType.SIMPLE;
            cursorSprite = ContentHelper.Load<Texture2D>( @"gui\cursors_basic" );

            #region ShakeEvents
            PositionChanged += delegate( MouseEvent e ) {
                if( HorizontalyShaked == null ) return; 
                if( e.previousMouseState.X > e.currentMouseState.X && goingToLeft || e.previousMouseState.X < e.currentMouseState.X && !goingToLeft ) {
                    //direction hasn't changed 
                } else {
                    //direction has changed
                    goingToLeft = !goingToLeft;
                    if( Math.Abs( previousRelevanteShakeDirectionXChange - e.currentMouseState.X ) >= shakeAmplitude ) {

                        //the distance between the last direction change is great enough.
                        previousRelevanteShakeDirectionXChange = e.currentMouseState.X;
                        previousDirectionChanges.Enqueue( e.gameTime.TotalGameTime );

                        if( previousDirectionChanges.Count() >= shakeDirectionChangeNeeded ) {
                            //5 change are registred;
                            if( ( previousDirectionChanges.Peek() + shakeDelay ) <= e.gameTime.TotalGameTime ) {
                                //the 5th previous change was in the good time intervals
                                //ITS A GOOD SHAKE ! The coktail will be good.

                                OnShaked( e.gameTime, e.currentMouseState );
                                previousDirectionChanges.Clear();
                            }
                        }
                    }


                }
            };

            
            #endregion
        }

        public static void UpdateCursor( GameTime gameTime ) {
            MouseState currentState = Mouse.GetState();
            #region Events
            #region Left Button
            if( currentState.LeftButton != previousState.LeftButton ) {
                OnLeftButtonChanged( gameTime, currentState );
            } else {
                OnLeftButtonNotChanged( gameTime, currentState );
            }
            #endregion
            #region Right Button
            if( currentState.RightButton != previousState.RightButton ) {
                OnRightButtonChanged( gameTime, currentState );
            } else {
                OnRightButtonNotChanged( gameTime, currentState );
            }
            #endregion
            #region Middle Button
            if( currentState.MiddleButton != previousState.MiddleButton ) {
                OnMiddleButtonChanged( gameTime, currentState );
            } else {
                OnMiddleButtonNotChanged( gameTime, currentState );
            }
            #endregion
            #region XButton1 Button
            if( currentState.XButton1 != previousState.XButton1 ) {
                OnXButton1Changed( gameTime, currentState );
            } else {
                OnXButton1NotChanged( gameTime, currentState );
            }
            #endregion
            #region XButton2 Button
            if( currentState.XButton2 != previousState.XButton2 ) {
                OnXButton2Changed( gameTime, currentState );
            } else {
                OnXButton2NotChanged( gameTime, currentState );
            }
            #endregion
            #region ScrollWheel
            if( currentState.ScrollWheelValue != previousState.ScrollWheelValue ) {
                OnScrollWheelValueChanged( gameTime, currentState );
            } else {
                OnScrollWheelValueNotChanged( gameTime, currentState );
            }
            #endregion
            #region Position
            if( currentState.X != previousState.X || currentState.Y != previousState.Y ) {
                OnPositionChanged( gameTime, currentState );
            } else {
                OnPositionNotChanged( gameTime, currentState );
            }
            #region Shake detection
            //shake event is triggered when the mouse change 'shakeDirectionChange' times during 'shakeDelay' with a minimum 'shakeAmplitude' distance between changes.
            #endregion
            #endregion
            #endregion

            previousState = Mouse.GetState();
        }

        public static void DrawCursor() {
            GameEngine.spriteBatch.Begin();
            switch( currentType ) {
                case CursorType.SIMPLE:
                    GameEngine.spriteBatch.Draw( cursorSprite, new Vector2( Mouse.GetState().X, Mouse.GetState().Y ), new Rectangle( 0, 0, 32, 32 ), Color.White );
                    break;
                case CursorType.WAITING:
                    GameEngine.spriteBatch.Draw( cursorSprite, new Vector2( Mouse.GetState().X, Mouse.GetState().Y ), new Rectangle( 32, 0, 32, 32 ), Color.White );
                    break;
                case CursorType.ACTION:
                    GameEngine.spriteBatch.Draw( cursorSprite, new Vector2( Mouse.GetState().X, Mouse.GetState().Y ), new Rectangle( 64, 0, 32, 32 ), Color.White );
                    break;
            }
            GameEngine.spriteBatch.End();
        }

        #region Events handlers
        #region Left Button
        private static void OnLeftButtonPressed( GameTime gameTime, MouseState currentState ) {
            if( LeftButtonPressed != null ) {
                LeftButtonPressed( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnLeftButtonReleased( GameTime gameTime, MouseState currentState ) {
            if( LeftButtonReleased != null ) {
                LeftButtonReleased( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnLeftButtonChanged( GameTime gameTime, MouseState currentState ) {
            if( LeftButtonChanged != null ) {
                LeftButtonChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
            if( previousState.LeftButton == ButtonState.Pressed ) {
                OnLeftButtonPressed( gameTime, currentState );
            } else {
                OnLeftButtonReleased( gameTime, currentState );
            }
        }

        private static void OnLeftButtonNotChanged( GameTime gameTime, MouseState currentState ) {
            if( LeftButtonNotChanged != null ) {
                LeftButtonNotChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }
        #endregion
        #region Right Button
        private static void OnRightButtonPressed( GameTime gameTime, MouseState currentState ) {
            if( RightButtonPressed != null ) {
                RightButtonPressed( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnRightButtonReleased( GameTime gameTime, MouseState currentState ) {
            if( RightButtonReleased != null ) {
                RightButtonReleased( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnRightButtonChanged( GameTime gameTime, MouseState currentState ) {
            if( RightButtonChanged != null ) {
                RightButtonChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
            if( previousState.RightButton == ButtonState.Pressed ) {
                OnRightButtonPressed( gameTime, currentState );
            } else {
                OnRightButtonReleased( gameTime, currentState );
            }
        }

        private static void OnRightButtonNotChanged( GameTime gameTime, MouseState currentState ) {
            if( RightButtonNotChanged != null ) {
                RightButtonNotChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }
        #endregion
        #region Middle Button
        private static void OnMiddleButtonPressed( GameTime gameTime, MouseState currentState ) {
            if( MiddleButtonPressed != null ) {
                MiddleButtonPressed( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnMiddleButtonReleased( GameTime gameTime, MouseState currentState ) {
            if( MiddleButtonReleased != null ) {
                MiddleButtonReleased( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnMiddleButtonChanged( GameTime gameTime, MouseState currentState ) {
            if( MiddleButtonChanged != null ) {
                MiddleButtonChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
            if( previousState.MiddleButton == ButtonState.Pressed ) {
                OnMiddleButtonPressed( gameTime, currentState );
            } else {
                OnMiddleButtonReleased( gameTime, currentState );
            }
        }

        private static void OnMiddleButtonNotChanged( GameTime gameTime, MouseState currentState ) {
            if( MiddleButtonNotChanged != null ) {
                MiddleButtonNotChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }
        #endregion
        #region XButton1 Button
        private static void OnXButton1Pressed( GameTime gameTime, MouseState currentState ) {
            if( XButton1Pressed != null ) {
                XButton1Pressed( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnXButton1Released( GameTime gameTime, MouseState currentState ) {
            if( XButton1Released != null ) {
                XButton1Released( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnXButton1Changed( GameTime gameTime, MouseState currentState ) {
            if( XButton1Changed != null ) {
                XButton1Changed( new MouseEvent( gameTime, previousState, currentState ) );
            }
            if( previousState.XButton1 == ButtonState.Pressed ) {
                OnXButton1Pressed( gameTime, currentState );
            } else {
                OnXButton1Released( gameTime, currentState );
            }
        }

        private static void OnXButton1NotChanged( GameTime gameTime, MouseState currentState ) {
            if( XButton1NotChanged != null ) {
                XButton1NotChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }
        #endregion
        #region XButton2 Button
        private static void OnXButton2Pressed( GameTime gameTime, MouseState currentState ) {
            if( XButton2Pressed != null ) {
                XButton2Pressed( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnXButton2Released( GameTime gameTime, MouseState currentState ) {
            if( XButton2Released != null ) {
                XButton2Released( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnXButton2Changed( GameTime gameTime, MouseState currentState ) {
            if( XButton2Changed != null ) {
                XButton2Changed( new MouseEvent( gameTime, previousState, currentState ) );
            }
            if( previousState.XButton2 == ButtonState.Pressed ) {
                OnXButton2Pressed( gameTime, currentState );
            } else {
                OnXButton2Released( gameTime, currentState );
            }
        }

        private static void OnXButton2NotChanged( GameTime gameTime, MouseState currentState ) {
            if( XButton2NotChanged != null ) {
                XButton2NotChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }
        #endregion
        #region ScrollWheel
        private static void OnScrollWheelValueChanged( GameTime gameTime, MouseState currentState ) {
            if( ScrollWheelValueChanged != null ) {
                ScrollWheelValueChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnScrollWheelValueNotChanged( GameTime gameTime, MouseState currentState ) {
            if( ScrollWheelValueNotChanged != null ) {
                ScrollWheelValueNotChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }
        #endregion
        #region Position
        private static void OnPositionChanged( GameTime gameTime, MouseState currentState ) {
            if( PositionChanged != null ) {
                PositionChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnPositionNotChanged( GameTime gameTime, MouseState currentState ) {
            if( PositionNotChanged != null ) {
                PositionNotChanged( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        private static void OnShaked( GameTime gameTime, MouseState currentState ) {
            if( HorizontalyShaked != null ) {
                HorizontalyShaked( new MouseEvent( gameTime, previousState, currentState ) );
            }
        }

        #endregion
        #endregion
    }
}
