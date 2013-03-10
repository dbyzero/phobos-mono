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

        #region ascessor & mutator
        public Vector2 Position { get; set; }
        public int Width { get ; set; }
        public int Height { get ; set;  } 
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
                        Scene.GetInstance().CalculPositionsEntitiesHandler() ;
                    }
            }
        }
        #endregion

        public Camera(int coeff = 1)
        {
            cameraFillScreen();
            coefficient = coeff;
        }

        public Camera(float X, float Y, int coeff = 1) : this(coeff)
        {
            Position = new Vector2(X, Y);
        }

        /**
         * <summary>
         * Redimmentionne la camera pour remplir la fenetre
         * </summary>
         */
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
            Scene.GetInstance().CalculCenterEntity(); ;
        }

        /**
         * <summary>
         * Retourne la direction regandant une entité en fonction de son orientation et de celle du monde
         * </summary>
         */
        public Orientation getLookDirectionFromOrientation(Orientation orient) {
            switch (Scene.GetInstance().Orientation) {
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
            //Si on sait pas, toujours regarder en bas a gauche
            return Orientation.BL;
        }

        public void turnCamera(Orientation orient)
        {
            if (Scene.GetInstance().Orientation != orient)
            {
                //turn scene
                Scene.GetInstance().Orientation = orient;

                //recalcule tiles position
                Scene.GetInstance().CalculPositionsEntitiesHandler();

                //calcule the vector to keep the same center, la position est divisé par le coeff pour satisfaire la vierge marie des transformations.
                Vector2 shift_vector;
                shift_vector.X = Scene.GetInstance().Camera.Width / 2 - (Scene.GetInstance().CenterEntity.ScreenRect.X / Scene.GetInstance().Camera.Coefficient - Scene.GetInstance().Camera.Position.X);
                shift_vector.Y = Scene.GetInstance().Camera.Height / 2 - (Scene.GetInstance().CenterEntity.ScreenRect.Y / Scene.GetInstance().Camera.Coefficient - Scene.GetInstance().Camera.Position.Y);

                //move the sceEEEeeene
                Scene.GetInstance().Camera.Position -= shift_vector;

                //recalcul new center (normally the same one)
                Scene.GetInstance().CalculCenterEntity();
            }
            
        }

    }
}
