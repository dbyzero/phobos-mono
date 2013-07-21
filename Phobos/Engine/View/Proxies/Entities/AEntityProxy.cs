using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.Engine.View.Proxies.Entities
    {
        abstract class AEntityProxy
        {
            protected Vector3 worldPosition;

            private AEntityProxy()
            {
            }

            public AEntityProxy(Vector3 wp)
            {
                worldPosition = wp;
            }

            #region Accessors and Mutators
            public virtual Vector3 WorldPosition {
                get { return worldPosition; }
                set
                {
                    worldPosition = value;
                }
            }

            //wrapper for worldPosition.X
            public virtual float X
            {
                get { return worldPosition.X; }
                set 
                { 
                    worldPosition.X = value; 
                }
            }

            //wrapper for worldPosition.Y
            public virtual float Y
            {
                get { return worldPosition.Y; }
                set
                {
                    worldPosition.Y = value;
                }
            }

            //wrapper for worldPosition.Z
            public virtual float Z
            {
                get { return worldPosition.Z; }
                set
                {
                    worldPosition.Z = value;
                }
            }

            #endregion 

            /**
             * <summary>Move entity in the world</summary>
             * 
             */
            public virtual void move(Vector3 v)
            {
                worldPosition += v;
            }
        }
    }
