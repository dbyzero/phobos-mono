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
        Dictionary<Orientation, Animation> anims = new Dictionary<Orientation, Animation>() ;

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

        //set animations by index
        public Animation this[Orientation orientation]
        {
            get { return anims[orientation]; }
            set { anims[orientation] = value; }
        }

        public override int Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Animation anim_to_draw ;
            //if cannot get the animation for the orientation, get the first one
            /*Console.WriteLine(
                "Avatar look at " + Scene.getInstance().Camera.getLookDirectionFromOrientation(Orientation)
                + " for a scene orientated from "+Scene.getInstance().Orientation
                + " and an entiry look at "+base.Orientation
                + " end orientation setted is " + anims.TryGetValue(Scene.getInstance().Camera.getLookDirectionFromOrientation(Orientation), out anim_to_draw)
            );*/
            if (!anims.TryGetValue(Scene.getInstance().Camera.getLookDirectionFromOrientation(Orientation), out anim_to_draw))
	        {
                anim_to_draw = anims.Values.First();
	        }

            if (this == Scene.getInstance().CenterEntity)
            {
                this.color = Color.Yellow;
            }
            else
            {
                this.color = new Color(new Vector4(
                                   0.8f, 0.8f, 0.8f, 1.0f
                                   )
                               );
            }
            return anim_to_draw.Draw(spriteBatch, gameTime, this);
        }
    }
}
