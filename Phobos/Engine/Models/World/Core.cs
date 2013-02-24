using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Models.Entities;

namespace Phobos.Engine.Models.World
{
    class Core : DrawableEntity
    {
        private List<DrawableEntity>entities = new  List<DrawableEntity>() ;

        public Core(Vector3 position, int width, int height, Vector2 center, Texture2D texture, Rectangle texturePosition,Color color) 
            : base(position, width, height, center, texture, texturePosition,color) 
        {
        }
       
        public void addEntity(DrawableEntity ent) {
            entities.Add(ent) ;
        }

        public void removeEntity(DrawableEntity ent)
        {
            entities.Remove(ent);
        }
            
        public override int Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            int count_sprite = base.Draw( spriteBatch,  gameTime) ;
            foreach(DrawableEntity ent in entities) {
                count_sprite += ent.Draw(spriteBatch, gameTime);
            }
            return count_sprite ;

        }

    }
}
