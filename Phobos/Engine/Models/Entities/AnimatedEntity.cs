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

        public void addAnimation(Orientation or, Animation an)
        {
            anims.Add(or, an);
        }

        public virtual int Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Animation anim_to_draw ;
            //if cannot get the animation for the orientation, get the first one
            if (!anims.TryGetValue(Scene.getInstance().Camera.getLookDirectionFromOrientation(Orientation), out anim_to_draw))
	        {
                anim_to_draw = anims[0];
	        }
            return anim_to_draw.Draw(spriteBatch, gameTime, this);
        }
    }
}
