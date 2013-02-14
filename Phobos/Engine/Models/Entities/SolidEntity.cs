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

        private Rectangle spriteSheetRect; //zone du sprite sur sa sheet
        private Rectangle screenRect; //zone du sprite sur le screen

        private Vector2 centerSprite ; //point central du sprit

        private Int16 width;
        private Int16 height;

        #region Methods

        #region Constructor
        public SolidEntity(Vector3 position) : base(position)
        {
        }
        #endregion

        #region Accessors and mutators
        //On surcharge les mutator de position pour recalculer la zone du sprite sur le screen

        public override Vector3 WorldPosition
        {
            get { return base.WorldPosition; }
            set
            {
                base.WorldPosition = value;
                calculateScreenRect();
            }
        }

        public override float X
        {
            get { return base.X; }
            set
            {
                base.X = value;
                calculateScreenRect();
            }
        }

        public override float Y
        {
            get { return base.Y; }
            set
            {
                base.Y = value;
                calculateScreenRect();
            }
        }

        public override float Z
        {
            get { return base.Z; }
            set
            {
                base.Z = value;
                calculateScreenRect();
            }
        }

        
        public Int16 Width
        {
            get { return width; }
            set { 
                width = value;
                calculateScreenRect();
            }
        }

        public Int16 Height
        {
            get { return height; }
            set { 
                height = value;
                calculateScreenRect();
            }
        }

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }

        public Rectangle SpriteSheetRect
        {
            get { return spriteSheetRect; }
            set { spriteSheetRect = value; }
        }

        public Rectangle ScreenRect
        {
            get { return screenRect; }
            set { screenRect = value ; }
        }

        public Vector2 CenterSprite
        {
            get { return centerSprite; }
            set { centerSprite = value; }
        }
        #endregion 

        public void calculateScreenRect()
        {
            screenRect.X = (int)(X * 16 - Y * 16) - (int)CenterSprite.X;
            screenRect.Y = (int)(X * 8 + Y * 8 - Z * 16) - (int)CenterSprite.Y;
            screenRect.Width = Width;
            screenRect.Height = Height;
        }

        /*
         * return number of sprite drawed
         * params : SpriteBatch owning the draw call
         * 
         */
        public int Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(
                SpriteSheet,
                ScreenRect,
                SpriteSheetRect,
                new Color(255, 255, 255, 255));
            return 1;
        }

        public override void move(Vector3 v)
        {
            base.move(v);
            calculateScreenRect();
        }


        #endregion
    }
}
