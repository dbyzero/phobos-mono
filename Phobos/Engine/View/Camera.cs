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
        public int Width { get { return width; } set { width = value ;} }
        public int Height { get { return height; } set { height = value; } } 
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
                        Scene.getInstance().calculPositionsEntitiesHandler() ;
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

        public void move(Vector2 shift)
        {
            Position += shift;
            Scene.getInstance().CalculCenterEntity(); ;
        }

        public void turnCamera(Orientation orient)
        {
            //turn scene
            Scene.getInstance().Orientation = orient;

            //recalcule tiles position
            Scene.getInstance().calculPositionsEntitiesHandler();

            //calcule the vector to keep the same center
            Vector2 shift_vector ;
            shift_vector.X = Scene.getInstance().Camera.Width / 2 - (Scene.getInstance().CenterEntity.ScreenRect.X - Scene.getInstance().Camera.Position.X);
            shift_vector.Y = Scene.getInstance().Camera.Height / 2 - (Scene.getInstance().CenterEntity.ScreenRect.Y - Scene.getInstance().Camera.Position.Y);

            //apply vector
            Scene.getInstance().Camera.Position -= shift_vector;
            
            //recalcul new center (normally the same one)
            Scene.getInstance().CalculCenterEntity(); ;
            
        }

    }
}
