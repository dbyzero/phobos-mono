using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phobos.Engine.Models.Entities ;
using Phobos.Engine;

namespace Phobos.Engine.View
{
    class Camera        
    {
        private Vector2 position = Vector2.Zero;
        int width, height;
        private float coefficient;
        private int minZoom = 1;
        private int maxZoom = 10;

        public Camera(int coeff = 1)
        {
            cameraFillScreen();
            coefficient = coeff;
        }

        public Camera(float X,float Y,int coeff = 1)
        {
            Position = new Vector2(X, Y);
            cameraFillScreen();
            coefficient = coeff;
        }

        #region ascessor & mutator
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; } 
        #endregion

        public float Coefficient { 
            get { return coefficient; }
            set {
                    float prevCoefficient = coefficient;
                    coefficient = value;
                    coefficient = Math.Min(maxZoom, coefficient);
                    coefficient = Math.Max(minZoom, coefficient);

                    /* calcul new camera resolution */
                    Width = (int)(GameEngine.Instance.DeviceManager.PreferredBackBufferWidth / coefficient);
                    Height = (int)(GameEngine.Instance.DeviceManager.PreferredBackBufferHeight / coefficient);

                    /* redim sprites if coeff change */
                    if (prevCoefficient != coefficient)
                    {
                        Scene.getInstance().calculRenderEntitiesHandler() ;
                    }
            } 
        }

        public void cameraFillScreen()
        {
            Width = GameEngine.Instance.DeviceManager.PreferredBackBufferWidth;
            Height = GameEngine.Instance.DeviceManager.PreferredBackBufferHeight;
        }

        public override string ToString()
        {
            return "Position:" + Position + " Width:" + Width + " Height:" + Height;
        }

        public void turn180()
        {
            Scene.getInstance().Camera.Position -=
                new Vector2(
                    2 * Scene.getInstance().Camera.Position.X + 
                    Scene.getInstance().Camera.Width,
                    
                    2 * Scene.getInstance().Camera.Position.Y + 
                    Scene.getInstance().Camera.Height
                );
        }

        public void turn90Left()
        {
            Scene.getInstance().Camera.Position =
                new Vector2(
                    -2 * (Scene.getInstance().Camera.Position.Y),
                    (Scene.getInstance().Camera.Position.X) / 2
                );
                           
        }

        public void turn90Right()
        {
            Scene.getInstance().Camera.Position =
                new Vector2(
                    2 * (Scene.getInstance().Camera.Position.Y),
                    (-Scene.getInstance().Camera.Position.X) / 2
                );

        }

        public void turnCamera(Orientation orient)
        {
            Console.WriteLine(this);
            switch(Scene.getInstance().Orientation) {
                case Orientation.SE :
                    switch (orient)
                    {
                        case Orientation.SO:
                            Scene.getInstance().Orientation = Orientation.SO;
                            turn90Left();
                            break;
                        case Orientation.NE:
                            Scene.getInstance().Orientation = Orientation.NE;
                            turn90Right();
                            break;
                        case Orientation.NO:
                            Scene.getInstance().Orientation = Orientation.NO;
                            turn180();
                            break;
                        default:
                            return;
                    }
                    break ;
                case Orientation.SO :
                    switch (orient)
                    {
                        case Orientation.SE:
                            Scene.getInstance().Orientation = Orientation.SE;
                            turn90Right();
                            break;
                        case Orientation.NE:
                            Scene.getInstance().Orientation = Orientation.NE;
                            turn180();
                            break;
                        case Orientation.NO:
                            Scene.getInstance().Orientation = Orientation.NO;
                            turn90Left();
                            break;
                        default:
                            return;
                    }
                    break ;
                case Orientation.NE :
                    switch (orient)
                    {
                        case Orientation.SE:
                            Scene.getInstance().Orientation = Orientation.SE;
                            turn90Left();
                            break;
                        case Orientation.SO:
                            Scene.getInstance().Orientation = Orientation.SO;
                            turn180();
                            break;
                        case Orientation.NO:
                            Scene.getInstance().Orientation = Orientation.NO;
                            turn90Right();
                            break;
                        default:
                            return;
                    }
                    break ;
                case Orientation.NO :
                    switch (orient)
                    {
                        case Orientation.SE:
                            Scene.getInstance().Orientation = Orientation.SE;
                            turn180();
                            break;
                        case Orientation.SO:
                            Scene.getInstance().Orientation = Orientation.SO;
                            turn90Right();
                            break;
                        case Orientation.NE:
                            Scene.getInstance().Orientation = Orientation.NE;
                            turn90Left();
                            break;
                        default:
                            return;
                    }
                    break ;
            }
            Scene.getInstance().calculRenderEntitiesHandler();
            Console.WriteLine(this);
        }

    }
}
