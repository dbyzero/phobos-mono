﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.View;

namespace Phobos.Engine.Models.Entities
{
    class DrawableEntity : AEntity
    {
        private Texture2D spriteSheet;

        private Rectangle spriteSheetRect; //zone du sprite sur sa sheet
        private Rectangle screenRect; //zone du sprite sur le screen

        private Vector2 centerSprite ; //point central du sprit

        private int width;
        private int height;

        private Color color = Color.White;
        private float layer = 0.000001f;
        private float rotation = 0.0f;

        #region Methods

        #region Constructors

        
        private DrawableEntity() : base()
        {
        }

        /* Note : do not call mutator or it will make a lot of recaculate position on the screen */
        public DrawableEntity(Vector3 position, int width, int height, Vector2 center, Texture2D texture, Rectangle texturePosition)
        {
            this.worldPosition = position;
            this.width = width;
            this.height = height;
            this.centerSprite = center;
            this.spriteSheet = texture;
            this.spriteSheetRect = texturePosition;
            calculateScreenRect();

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
                //calculateScreenRect();
            }
        }

        public override float X
        {
            get { return base.X; }
            set
            {
                base.X = value;
                //calculateScreenRect();
            }
        }

        public override float Y
        {
            get { return base.Y; }
            set
            {
                base.Y = value;
                //calculateScreenRect();
            }
        }

        public override float Z
        {
            get { return base.Z; }
            set
            {
                base.Z = value;
                //calculateScreenRect();
            }
        }

        
        public int Width
        {
            get { return width; }
            set { 
                width = value;
                //calculateScreenRect();
            }
        }

        public int Height
        {
            get { return height; }
            set { 
                height = value;
                //calculateScreenRect();
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
            screenRect.X = (int)(X * 16 - Y * 16) * Scene.getInstance().currentCamera().Coefficient;
            screenRect.Y = (int)(X * 8 + Y * 8 - Z * 16) * Scene.getInstance().currentCamera().Coefficient;
            screenRect.Width = Width * Scene.getInstance().currentCamera().Coefficient;
            screenRect.Height = Height * Scene.getInstance().currentCamera().Coefficient;
        }

        /*
         * return number of sprite drawed
         * params : SpriteBatch owning the draw call
         * 
         */
        public virtual int Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
           spriteBatch.Draw(
                SpriteSheet,
                ScreenRect,
                SpriteSheetRect,
                color,
                rotation,
                centerSprite + Scene.getInstance().currentCamera().Position,
                SpriteEffects.None,
                layer
                );
            return 1;
        }

        public override void move(Vector3 v)
        {
            base.move(v);
            //calculateScreenRect();
        }


        #endregion
    }
}