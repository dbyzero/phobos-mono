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
        private Rectangle frame;
        private float coefficient;
        private Orientation projection;

        public Camera(Orientation p = Orientation.SE, float coeff = 1)
        {
            reinitCamera();
            projection = p;
            coefficient = coeff;
        }

        public Rectangle Frame { 
            get { return frame; } 
            set { frame = value; } 
        }

        public float Coefficient { 
            get { return coefficient; } 
            set { coefficient = value; } 
        }

        public void reinitCamera()
        {
            frame.X = 0;
            frame.Y = 0;
            frame.Width = GameEngine.Instance.DeviceManager.PreferredBackBufferWidth;
            frame.Height = GameEngine.Instance.DeviceManager.PreferredBackBufferHeight;
        }

        public void cameraFillScreen()
        {
            frame.Width = GameEngine.Instance.DeviceManager.PreferredBackBufferWidth;
            frame.Height = GameEngine.Instance.DeviceManager.PreferredBackBufferHeight;
        }

        public void move(Vector2 mouvement)
        {
            frame.X += (int)mouvement.X;
            frame.Y += (int)mouvement.Y;
        }

        public void ProjectToScreen(SolidEntity entity)
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
