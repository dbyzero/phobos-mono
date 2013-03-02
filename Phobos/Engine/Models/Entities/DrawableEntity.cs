using System;
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

        private Rectangle screenRect; //zone du sprite sur le screen

        private Vector2 centerSprite ; //point central du sprit
        private Orientation orientation = Orientation.S;

        Dictionary<Orientation, Sprite> sprites = new Dictionary<Orientation, Sprite>();

        private int width;
        private int height;

        protected Color color ;
        private float layer = 0.000001f;
        private float rotation = 0.0f;

        #region Methods

        #region Constructors

        
        private DrawableEntity() : base()
        {
        }

        //set sprite as index
        public Sprite this[Orientation orientation]
        {
            get { return sprites[orientation]; }
            set { sprites[orientation] = value; }
        }

        /* Note : do not call mutator or it will make a lot of recaculate position on the screen */
        public DrawableEntity(Vector3 position, int width, int height, Vector2 center, Texture2D texture, Color color,Orientation orientation)
        {
            this.worldPosition = position;
            this.width = width;
            this.height = height;
            this.centerSprite = center;
            this.spriteSheet = texture;
            this.color = color;
            this.orientation = orientation;
        }
        #endregion

        #region Accessors and mutators
        //On surcharge les mutator de position pour recalculer la zone du sprite sur le screen

        public Orientation Orientation { get { return orientation; } set { orientation = value; } }

        public float Rotation { get { return rotation; } set { rotation = value; } }

        public float Layer { get { return layer; } set { layer = value; } }

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

        public Color Color
        {
            get{return color;}
            set { color = value ;}
        }
        #endregion 

        public void calculateScreenRect()
        {
            switch (Scene.getInstance().Orientation)
            {
                //Calcul pour SE
                case Orientation.SE :
                    screenRect.X = (int)((X * 16 - Y * 16 - centerSprite.X) * Scene.getInstance().Camera.Coefficient);
                    screenRect.Y = (int)((X * 8 + Y * 8 - Z * 16 - centerSprite.Y) * Scene.getInstance().Camera.Coefficient);
                    screenRect.Width = (int)(Width * Scene.getInstance().Camera.Coefficient);
                    screenRect.Height = (int)(Height * Scene.getInstance().Camera.Coefficient);
                    break;
                //Calcul pour SO
                case Orientation.SO:
                    screenRect.X = (int)((X * 16 + Y * 16 - centerSprite.X) * Scene.getInstance().Camera.Coefficient);
                    screenRect.Y = (int)((Y * 8 - X * 8 - Z * 16 - centerSprite.Y) * Scene.getInstance().Camera.Coefficient);
                    screenRect.Width = (int)(Width * Scene.getInstance().Camera.Coefficient);
                    screenRect.Height = (int)(Height * Scene.getInstance().Camera.Coefficient);
                    break;
                //Calcul pour NO
                case Orientation.NO:
                    screenRect.X = (int)((Y * 16 - X * 16 - centerSprite.X) * Scene.getInstance().Camera.Coefficient);
                    screenRect.Y = (int)((-1 * Y * 8 - X * 8 - Z * 16 - centerSprite.Y) * Scene.getInstance().Camera.Coefficient);
                    screenRect.Width = (int)(Width * Scene.getInstance().Camera.Coefficient);
                    screenRect.Height = (int)(Height * Scene.getInstance().Camera.Coefficient);
                    break;
                //Calcul pour NE
                case Orientation.NE:
                    screenRect.X = (int)((-1 * X * 16 - Y * 16 - centerSprite.X) * Scene.getInstance().Camera.Coefficient);
                    screenRect.Y = (int)((X * 8 - Y * 8 - Z * 16 - centerSprite.Y) * Scene.getInstance().Camera.Coefficient);
                    screenRect.Width = (int)(Width * Scene.getInstance().Camera.Coefficient);
                    screenRect.Height = (int)(Height * Scene.getInstance().Camera.Coefficient);
                    break;
            }
        }

        /*
         * return number of sprite drawed
         * params : SpriteBatch owning the draw call
         * 
         */
        public virtual int Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (this == Scene.getInstance().CenterEntity) {
                this.color = Color.Yellow;
            } else {
                this.color = new Color(new Vector4(0.8f, 0.8f, 0.8f, 1.0f));
            }

            Sprite sprite_to_draw;
           /*
            Console.WriteLine(
                "Tile look at " + Scene.getInstance().Camera.getLookDirectionFromOrientation(Orientation)
                + " for a scene orientated from " + Scene.getInstance().Orientation
                + " and an entiry look at " + orientation
                + " end orientation setted is " + sprites.TryGetValue(Scene.getInstance().Camera.getLookDirectionFromOrientation(Orientation), out sprite_to_draw)
            );
            */
            //if cannot get the animation for the orientation, get the first one
            if (!sprites.TryGetValue(Scene.getInstance().Camera.getLookDirectionFromOrientation(Orientation), out sprite_to_draw))
            {
                sprite_to_draw = sprites.Values.First();
            }


            spriteBatch.Draw(
                SpriteSheet,
                ScreenRect,
                sprite_to_draw.Rectangle,
                color,
                rotation,
                Scene.getInstance().Camera.Position,
                sprite_to_draw.SpriteEffect,
                layer
                );
            return 1;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public override void move(Vector3 v)
        {
            base.move(v);
        }

        public virtual void checkCenter()
        {
            if (ScreenRect.X > (Scene.getInstance().Camera.Width / 2 + Scene.getInstance().Camera.Position.X - Width / 2) * Scene.getInstance().Camera.Coefficient)
            {
                if (ScreenRect.X < (Scene.getInstance().Camera.Width / 2 + Scene.getInstance().Camera.Position.X + Width / 2) * Scene.getInstance().Camera.Coefficient)
                {
                    if (ScreenRect.Y > (Scene.getInstance().Camera.Height / 2 + Scene.getInstance().Camera.Position.Y - Height / 2) * Scene.getInstance().Camera.Coefficient)
                    {
                        if (ScreenRect.Y < (Scene.getInstance().Camera.Height / 2 + Scene.getInstance().Camera.Position.Y + Height / 2) * Scene.getInstance().Camera.Coefficient)
                        {
                            Scene.getInstance().CenterEntity = this;
                        }
                    }
                }
            }
        
        }

        #endregion

        public override string ToString()
        {
            return WorldPosition.ToString();
        }
    }
}
