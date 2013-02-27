using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Models.Entities;
using Phobos.Engine.View;

namespace Phobos.Engine.Models.World
{
    class Core : DrawableEntity
    {
        private List<DrawableEntity>entities = new  List<DrawableEntity>() ;        

        public Core(Vector3 position, int width, int height, Vector2 center, Texture2D texture, Rectangle texturePosition,Color color) 
            : base(position, width, height, center, texture, texturePosition,color) 
        {
            CliffN = 0;
            CliffS = 0;
            CliffE = 0;
            CliffO = 0;
        }

        public int CliffN
        {
            get;
            set;
        }

        public int CliffS{
            get;
            set;
        }

        public int CliffE {
            get;
            set;
        }

        public int CliffO{
            get;
            set;
        }
       
        public void addEntity(DrawableEntity ent) {
            entities.Add(ent) ;
        }

        public void removeEntity(DrawableEntity ent)
        {
            entities.Remove(ent);
        }

        public override int Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int count_sprite = 0 ;
            //TODO : Do not show hidden part
            int cliffToDraw = Math.Max(Math.Max(CliffS, CliffO), Math.Max(CliffE, CliffN)) ;
            for (int i = cliffToDraw; i > 0; i--)
            {
                spriteBatch.Draw(
                    SpriteSheet,
                    new Rectangle(
                        ScreenRect.X, //-1 because wall are 34 larges to keep black borders for cliff
                        ScreenRect.Y + (int)((i-1) * 16 * Scene.getInstance().Camera.Coefficient),
                        (int)(32 * Scene.getInstance().Camera.Coefficient),
                        (int)(32 * Scene.getInstance().Camera.Coefficient)
                    ),
                    new Rectangle(192,64, 32, 32),
                    Color,
                    0f,
                    Scene.getInstance().Camera.Position,
                    SpriteEffects.None,
                    0.000001f
                );
                count_sprite++;
            }

            count_sprite += base.Draw( spriteBatch,  gameTime) ;

            foreach(DrawableEntity ent in entities) {
                count_sprite += ent.Draw(spriteBatch, gameTime);
            }
            return count_sprite ;

        }

        public override void checkCenter()
        {
            base.checkCenter() ;
            foreach(DrawableEntity ent in entities) {
                ent.checkCenter() ;
            }
        }

        public void linkCliffs()
        {
        }

        public void calculCliffs()
        {
            try
            {
                CliffN = (int)Math.Ceiling(Z - Scene.getInstance().getCore((int)X, (int)Y - 1).Z);
            }
            catch (IndexOutOfRangeException e)
            {
                CliffN = (int)Math.Ceiling(Z - 0);
                //Console.WriteLine("!!! ERROR : Core[" + X + "," + ((int)Y - 1) + "] is out of range ");
            }
            catch (KeyNotFoundException e)
            {
                CliffN = (int)Math.Ceiling(Z - 0);
                //Console.WriteLine("!!! ERROR : Core[" + X + "," + ((int)Y + 1) + "] is out of range ");
            }

            try
            {
                CliffS = (int)Math.Ceiling(Z - Scene.getInstance().getCore((int)X, (int)Y + 1).Z);
            }
            catch (IndexOutOfRangeException e)
            {
                CliffS = (int)Math.Ceiling(Z - 0);
                //Console.WriteLine("!!! ERROR : Core[" + X + "," + ((int)Y + 1) + "] is out of range ");
            }
            catch (KeyNotFoundException e)
            {
                CliffS = (int)Math.Ceiling(Z - 0);
                //Console.WriteLine("!!! ERROR : Core[" + X + "," + ((int)Y + 1) + "] is out of range ");
            }

            try
            {
                CliffE = (int)Math.Ceiling(Z - Scene.getInstance().getCore((int)X + 1, (int)Y).Z);
            }
            catch (IndexOutOfRangeException e)
            {
                CliffE = (int)Math.Ceiling(Z - 0);
                //Console.WriteLine("!!! ERROR : Core[" + ((int)X + 1) + "," + Y + "] is out of range ");
            }
            catch (KeyNotFoundException e)
            {
                CliffE = (int)Math.Ceiling(Z - 0);
                //Console.WriteLine("!!! ERROR : Core[" + ((int)X + 1) + "," + Y + "] is out of range ");
            }

            try
            {
                CliffO = (int)Math.Ceiling(Z - Scene.getInstance().getCore((int)X - 1, (int)Y).Z);
            }
            catch (IndexOutOfRangeException e)
            {
                CliffO = (int)Math.Ceiling(Z - 0);
                //Console.WriteLine("!!! ERROR : Core[" + ((int)X - 1) + "," + Y + "] is out of range ");
            }
            catch (KeyNotFoundException e)
            {
                CliffO = (int)Math.Ceiling(Z - 0);
                //Console.WriteLine("!!! ERROR : Core[" + ((int)X - 1) + "," + Y + "] is out of range ");
            }
        }
    }
}
