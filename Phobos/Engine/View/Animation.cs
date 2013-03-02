using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phobos.Engine.Models.Entities;
using System.Collections;

namespace Phobos.Engine.View
{
    class Animation
    {
        Texture2D spriteSheet { get; set; }
        SortedList<int,Frame> frames = new SortedList<int,Frame>();
        IEnumerator<KeyValuePair<int, Frame>> frameEnumerator = null;
        SpriteEffects spriteEffect = SpriteEffects.None;

        public Animation(Texture2D ss, SpriteEffects se = SpriteEffects.None)
        {
            spriteSheet = ss;
            spriteEffect = se ;
        }

        public void addFrame(Rectangle z, int ts)
        {
            frames.Add(frames.Count,new Frame(z,ts)) ;
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
                spriteEffect,
                ent.Layer
                );

            return 1;
        }

        //can loop to the first animation
        private Frame goToNextFrame()
        {
            if (!frameEnumerator.MoveNext()){
                frameEnumerator.Reset();
                frameEnumerator.MoveNext();
            }
            return frames[frameEnumerator.Current.Key];
        }

        //get current frame depending on update time
        //can be improved to jump through more than 1 animation
        private Frame getCurrentFrame(GameTime gameTime)
        {
            frames[frameEnumerator.Current.Key].StillDuration -= gameTime.ElapsedGameTime.Milliseconds;
            if (frames[frameEnumerator.Current.Key].StillDuration < 0)
            {
                frames[frameEnumerator.Current.Key].StillDuration = frames[frameEnumerator.Current.Key].OriginDuration;
                goToNextFrame();
            }
            return frames[frameEnumerator.Current.Key] ;
        }
    }
}
