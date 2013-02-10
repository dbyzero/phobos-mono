using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phobos.Engine.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.Engine.Models.Entities
{
    class SolidEntity : AEntity
    {
        private Texture2D spriteSheet;
        private Rectangle spriteRect;
        private Int16 width;
        private Int16 height;

        #region Methods
        public SolidEntity(Vector3 vector3) : base( vector3 )
        {
        }
        #region Accessors and mutators
        public Int16 Width
        {
            get { return width; }
            set { width = value; }
        }

        public Int16 Height
        {
            get { return height; }
            set { height = value; }
        }

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }
        public Rectangle SpriteRect
        {
            get { return spriteRect; }
            set { spriteRect = value; }
        }
        #endregion 
        #endregion
    }
}
