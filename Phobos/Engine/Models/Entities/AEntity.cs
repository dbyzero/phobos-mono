    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    namespace Phobos.Engine.Entities
    {
        abstract class AEntity
        {
            private Vector3 worldPosition;

            #region method
            public AEntity()
            {
                worldPosition = Vector3.Zero;
            }
            public AEntity(Vector3 wp)
            {
                worldPosition = wp;
            }

            #region Accessors and mutators
            public virtual Vector3 WorldPosition {
                get { return worldPosition; }
                set
                {
                    worldPosition = value;
                }
            }

            public virtual float X
            {
                get { return worldPosition.X; }
                set 
                { 
                    worldPosition.X = value; 
                }
            }

            public virtual float Y
            {
                get { return worldPosition.Y; }
                set
                {
                    worldPosition.Y = value;
                }
            }

            public virtual float Z
            {
                get { return worldPosition.Z; }
                set
                {
                    worldPosition.Z = value;
                }
            }

            #endregion 

            public virtual void move(Vector3 v)
            {
                worldPosition += v;
            }

            #endregion
        }
    }
