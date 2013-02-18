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

        public void ProjectToScreen(DrawableEntity entity,Orientation projection)
        {
            switch (projection)
            {
                case Orientation.NE:
                    throw new Exception("Camera projection not yet set");
                    //break;
                case Orientation.NO:
                    throw new Exception("Camera projection not yet set");
                    //break;
                case Orientation.SE:
                    Rectangle tmp_rect = entity.ScreenRect;
                    tmp_rect.X = (int)(entity.X * 16 - entity.Y * 16) - (int)entity.CenterSprite.X;
                    tmp_rect.Y = (int)(entity.X * 8 + entity.Y * 8 - entity.Z * 16) - (int)entity.CenterSprite.Y;
                    tmp_rect.Width = entity.Width;
                    tmp_rect.Height = entity.Height;
                    entity.ScreenRect = tmp_rect;
                    break;
                case Orientation.SO:
                    throw new Exception("Camera projection not yet set");
                    //break;
                default:
                    throw new Exception("Camera have a unknow projection");
            }
        }

    }
}
