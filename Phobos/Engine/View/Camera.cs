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
        private int minZoom = 1;
        private int maxZoom = 10;
        private float coefficient;

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
        public int Width { get ; set; }
        public int Height { get ; set;  } 
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

        /**
         * <summary>
         * This function return the Orientation to draw depending on scene orientation and entity orientation
         * </summary>
         */
        public Orientation getLookDirectionFromOrientation(Orientation orient) {
            switch (Scene.getInstance().Orientation) {
                case Orientation.SE:
                    switch (orient)
                    {
                        case Orientation.S:
                            return Orientation.BL;
                        case Orientation.O:
                            return Orientation.TL;
                        case Orientation.N:
                            return Orientation.TR;
                        case Orientation.E:
                            return Orientation.BR;
                    }
                    break;
                case Orientation.SO:
                    switch (orient)
                    {
                        case Orientation.S:
                            return Orientation.BR;
                        case Orientation.O:
                            return Orientation.BL;
                        case Orientation.N:
                            return Orientation.TL;
                        case Orientation.E:
                            return Orientation.TR;
                    }
                    break;
                case Orientation.NE:
                    switch (orient)
                    {
                        case Orientation.S:
                            return Orientation.TL;
                        case Orientation.O:
                            return Orientation.TR;
                        case Orientation.N:
                            return Orientation.BR;
                        case Orientation.E:
                            return Orientation.BL;
                    }
                    break;
                case Orientation.NO:
                    switch (orient)
                    {
                        case Orientation.S:
                            return Orientation.TR;
                        case Orientation.O:
                            return Orientation.BR;
                        case Orientation.N:
                            return Orientation.BL;
                        case Orientation.E:
                            return Orientation.TL;
                    }
                    break;
            }
            //for unmanaged orientation, allow look to the south
            return Orientation.BL;
        }

        public void turnCamera(Orientation orient)
        {
            if (Scene.getInstance().Orientation != orient)
            {
                //turn scene
                Scene.getInstance().Orientation = orient;

                //recalcule tiles position
                Scene.getInstance().calculPositionsEntitiesHandler();

                //calcule the vector to keep the same center, la position est divisé par le coeff pour satisfaire la vierge marie des transformations.
                Vector2 shift_vector;
                shift_vector.X = Scene.getInstance().Camera.Width / 2 - (Scene.getInstance().CenterEntity.ScreenRect.X / Scene.getInstance().Camera.Coefficient - Scene.getInstance().Camera.Position.X);
                shift_vector.Y = Scene.getInstance().Camera.Height / 2 - (Scene.getInstance().CenterEntity.ScreenRect.Y / Scene.getInstance().Camera.Coefficient - Scene.getInstance().Camera.Position.Y);

                //move the sceEEEeeene
                Scene.getInstance().Camera.Position -= shift_vector;

                //recalcul new center (normally the same one)
                Scene.getInstance().CalculCenterEntity();
            }
            
        }

    }
}
