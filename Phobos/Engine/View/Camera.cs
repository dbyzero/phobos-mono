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
            return "Position:" + Position + " Width:" + Width + " Height:" + Height;        }

        public void turnCamera(Orientation orient)
        {
            Console.WriteLine(this);
            Vector2 old_position = Scene.getInstance().Camera.Position;
            switch(Scene.getInstance().Orientation) {
                case Orientation.SE :
                    switch (orient)
                    {
                        case Orientation.SO:
                            Scene.getInstance().Orientation = Orientation.SO;
                            break;
                        case Orientation.NE:
                            Scene.getInstance().Orientation = Orientation.NE;
                            break;
                        case Orientation.NO:
                            Scene.getInstance().Orientation = Orientation.NO;
                            Scene.getInstance().Camera.Position -= 
                                new Vector2(2 * old_position.X + Scene.getInstance().Camera.Width, 2 * old_position.Y + Scene.getInstance().Camera.Height);
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
                            break;
                        case Orientation.NE:
                            Scene.getInstance().Orientation = Orientation.NE;
                            Scene.getInstance().Camera.Position -= 
                                new Vector2(2 * old_position.X + Scene.getInstance().Camera.Width, 2 * old_position.Y + Scene.getInstance().Camera.Height);
                            break;
                        case Orientation.NO:
                            Scene.getInstance().Orientation = Orientation.NO;
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
                            break;
                        case Orientation.SO:
                            Scene.getInstance().Orientation = Orientation.SO;
                            Scene.getInstance().Camera.Position -= 
                                new Vector2(2 * old_position.X + Scene.getInstance().Camera.Width, 2 * old_position.Y + Scene.getInstance().Camera.Height);
                            break;
                        case Orientation.NO:
                            Scene.getInstance().Orientation = Orientation.NO;
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
                            Scene.getInstance().Camera.Position -= 
                                new Vector2(2 * old_position.X + Scene.getInstance().Camera.Width, 2 * old_position.Y + Scene.getInstance().Camera.Height);
                            break;
                        case Orientation.SO:
                            Scene.getInstance().Orientation = Orientation.SO;
                            break;
                        case Orientation.NE:
                            Scene.getInstance().Orientation = Orientation.NE;
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
