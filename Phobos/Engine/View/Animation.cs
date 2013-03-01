using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phobos.Engine.Models.Entities;

namespace Phobos.Engine.View
{
    class Animation
    {
        private Texture2D spriteSheet { get; set; }
        private SortedSet<Frame> frames;
        private IEnumerator<Frame> frameEnumerator = null;

        public Animation(Texture2D ss)
        {
            spriteSheet = ss;
        }

        public void addFrame(Rectangle z, TimeSpan ts)
        {
            frames.Add(new Frame(z,ts)) ;
            frameEnumerator = frames.GetEnumerator();
        }

        public int Draw(SpriteBatch spriteBatch, GameTime gameTime, DrawableEntity ent)
        {

            Frame frame = getCurrentFrame(gameTime);
            spriteBatch.Draw(
                spriteSheet,
                ent.ScreenRect,
                getCurrentFrame(gameTime).Zone,
                ent.Color,
                ent.Rotation,
                Scene.getInstance().Camera.Position,
                SpriteEffects.None,
                ent.Layer
                );

            return 1;
        }

        //can loop to the first animation
        private Frame goToNextFrame()
        {
            if (!frameEnumerator.MoveNext())
                frameEnumerator.Reset();

            return frameEnumerator.Current;
        }

        //get current frame depending on update time
        //can be improved to jump through more than 1 animation
        private Frame getCurrentFrame(GameTime gameTime)
        {
            frameEnumerator.Current.StillDuration -= gameTime.ElapsedGameTime;
            if (frameEnumerator.Current.StillDuration.TotalMilliseconds < 0)
            {
                goToNextFrame();
            }
            return frameEnumerator.Current;
        }
    }
}
