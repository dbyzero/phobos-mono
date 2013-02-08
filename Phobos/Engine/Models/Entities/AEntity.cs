    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    namespace Phobos.Engine.Entities
    {
        abstract class AEntity
        {
            protected Vector3 worldPosition;

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

            public Vector2 getScreenPosition()
            {
                //TODO
                //x : getX() * 16 - getY() * 16  + getDecalX()
                //y : getX() * 8 + getY() * 8 - getZ() * 16 - getHeight() + getDecalY()
                //x relative to camera : Ingame.getInstance().getCamera().getCoeffZoom() * (getInScreenCoordX() - Ingame.getInstance().getCamera().getX())
                //y relative to camera : Ingame.getInstance().getCamera().getCoeffZoom() * (getInScreenCoordY() - Ingame.getInstance().getCamera().getY())
                return Vector2.Zero;
            }
            #endregion 
            #endregion
        }
    }
