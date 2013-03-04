using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.Engine.Models.Entities
{
    class AnimatedEntity : DrawableEntity
    {
        private Dictionary<Orientation, Animation> anims = new Dictionary<Orientation, Animation>() ;

        //constructor
        public AnimatedEntity(
                Vector3 position,
                int width, int height,
                Vector2 center,
                Texture2D texture,
                Color color,
                Orientation orientation
            ) : base (
                position,
                width, height,
                center,
                texture,
                color,
                orientation
            ) { }

        //Comme pour le drawEntiry les rendu par orientation TL TR BL BR sont stocké en index
        public new Animation this[Orientation orientation]
        {
            get { return anims[orientation]; }
            set { anims[orientation] = value; }
        }

        //retourne le nombre de sprite affiché
        public override int Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Animation anim_to_draw ;
            //if cannot get the animation for the orientation, get the first one
            if (!anims.TryGetValue(Scene.getInstance().Camera.getLookDirectionFromOrientation(Orientation), out anim_to_draw))
	        {
                anim_to_draw = anims.Values.First();
            }

            //affiche l'animation
            return anim_to_draw.Draw(spriteBatch, gameTime, this);
        }
    }
}
