    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    namespace Phobos.Engine.Entities
    {
        abstract class AEntity
        {
            private Vector3 worldPosition;
            private Vector2 screenPosition;

            #region method
            public AEntity()
            {
                worldPosition = Vector3.Zero;
                calculateScreenPosition();
            }
            public AEntity(Vector3 wp)
            {
                worldPosition = wp;
                calculateScreenPosition();
            }

            #region Accessors and mutators
            public Vector3 WorldPosition {
                get { return worldPosition; }
                set
                {
                    worldPosition = value;
                    calculateScreenPosition();
                }
            }

            public float X
            {
                get { return worldPosition.X; }
                set 
                { 
                    worldPosition.X = value; 
                    calculateScreenPosition(); 
                }
            }

            public float Y
            {
                get { return worldPosition.Y; }
                set
                {
                    worldPosition.Y = value;
                    calculateScreenPosition();
                }
            }

            public float Z
            {
                get { return worldPosition.Z; }
                set
                {
                    worldPosition.Z = value;
                    calculateScreenPosition();
                }
            }

            public Vector2 ScreenPosition {
                get { return screenPosition; }
            }
            #endregion 

            public void move(Vector3 v) {
                worldPosition += v;
            }

            public void calculateScreenPosition()
            {
                screenPosition.X = X * 16 - Y * 16;
                screenPosition.Y = X * 8 + Y * 8 - Z * 16;
            }
            #endregion
        }
    }
