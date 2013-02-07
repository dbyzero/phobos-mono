    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Phobos.Engine.Graphics;

    namespace Phobos.Engine.Entities
    {
        abstract class AEntity
        {
            protected Sprite sprite;
            protected Vector2 worldPosition;
            protected Vector2 screenPosition;
            protected Layer layer;
        }
    }
