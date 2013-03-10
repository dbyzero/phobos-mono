using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.View;

namespace Phobos.Engine.View.Proxies.Entities
{
    class DrawableEntity : AEntityProxy
    {
        private Texture2D spriteSheet;
        private Rectangle screenRect; //zone du sprite sur le screen
        private Vector2 centerSprite ; //point central du sprit
        private Orientation orientation = Orientation.S; //La direction vers laquelle le sprite regarde
        private Dictionary<Orientation, SpriteArea> sprites = new Dictionary<Orientation, SpriteArea>();
        private int width;
        private int height;
        protected Color color ; //Filtre de couleur appliquer au sprite lors du draw
        private float layer = 0.000001f;
        private float rotation = 0.0f;

        #region Constructors
        
        /**
         * <summary>
         * L'orientation est TL TR BL BR, il est independant de la camera on parle en pur rendu ecran
         * </summary>
         */
        public SpriteArea this[Orientation orientation]
        {
            get { return sprites[orientation]; }
            set { sprites[orientation] = value; }
        }

        //constructeur
        //note : ne pas appeler les mutator ou ca peux stackoverflow
        public DrawableEntity(Vector3 position, int width, int height, Vector2 center, Texture2D texture, Color color,Orientation orientation) : base(position)
        {
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

        /**
         * <summary>
         * Calcul la position du Tile a l'écran en fonction de l'orientation. 
         * Note : la camera s'applique lors du rendu
         * </summary>
         */
        public void calculateScreenRect()
        {
            switch (Scene.GetInstance().Orientation)
            {
                //Calcul pour SE
                case Orientation.SE :
                    screenRect.X = (int)((X * 16 - Y * 16 - centerSprite.X) * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Y = (int)((X * 8 + Y * 8 - Z * 16 - centerSprite.Y) * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Width = (int)(Width * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Height = (int)(Height * Scene.GetInstance().Camera.Coefficient);
                    break;
                //Calcul pour SO
                case Orientation.SO:
                    screenRect.X = (int)((X * 16 + Y * 16 - centerSprite.X) * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Y = (int)((Y * 8 - X * 8 - Z * 16 - centerSprite.Y) * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Width = (int)(Width * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Height = (int)(Height * Scene.GetInstance().Camera.Coefficient);
                    break;
                //Calcul pour NO
                case Orientation.NO:
                    screenRect.X = (int)((Y * 16 - X * 16 - centerSprite.X) * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Y = (int)((-1 * Y * 8 - X * 8 - Z * 16 - centerSprite.Y) * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Width = (int)(Width * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Height = (int)(Height * Scene.GetInstance().Camera.Coefficient);
                    break;
                //Calcul pour NE
                case Orientation.NE:
                    screenRect.X = (int)((-1 * X * 16 - Y * 16 - centerSprite.X) * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Y = (int)((X * 8 - Y * 8 - Z * 16 - centerSprite.Y) * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Width = (int)(Width * Scene.GetInstance().Camera.Coefficient);
                    screenRect.Height = (int)(Height * Scene.GetInstance().Camera.Coefficient);
                    break;
            }
        }

        /*
         * Retourne le nombre de sprite dessiné
         * 
         */
        public virtual int Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //si aucun sprite set, on n'affiche 0 sprite, obvious !
            if (sprites.Count == 0) return 0;

            SpriteArea sprite_to_draw;
            //if cannot get the animation for the orientation, get the first one
            if (!sprites.TryGetValue(Scene.GetInstance().Camera.getLookDirectionFromOrientation(Orientation), out sprite_to_draw))
            {
                sprite_to_draw = sprites.Values.First();
            }
            
            spriteBatch.Draw(
                SpriteSheet,
                ScreenRect,
                sprite_to_draw.Rectangle,
                color,
                rotation,
                Scene.GetInstance().Camera.Position,
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

        /**
         * <summary>
         * Verifie si le DrawEntity est au centre de l'écran, si oui le stock dans Scene.getInstance().CenterEntity
         * </summary>
         */
        public virtual void checkCenter()
        {
            if (ScreenRect.X > (Scene.GetInstance().Camera.Width / 2 + Scene.GetInstance().Camera.Position.X - Width / 2) * Scene.GetInstance().Camera.Coefficient)
            {
                if (ScreenRect.X < (Scene.GetInstance().Camera.Width / 2 + Scene.GetInstance().Camera.Position.X + Width / 2) * Scene.GetInstance().Camera.Coefficient)
                {
                    if (ScreenRect.Y > (Scene.GetInstance().Camera.Height / 2 + Scene.GetInstance().Camera.Position.Y - Height / 2) * Scene.GetInstance().Camera.Coefficient)
                    {
                        if (ScreenRect.Y < (Scene.GetInstance().Camera.Height / 2 + Scene.GetInstance().Camera.Position.Y + Height / 2) * Scene.GetInstance().Camera.Coefficient)
                        {
                            Scene.GetInstance().CenterEntity = this;
                        }
                    }
                }
            }
        
        }

        public override string ToString()
        {
            return WorldPosition.ToString();
        }
    }
}
