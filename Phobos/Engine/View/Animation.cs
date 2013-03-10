using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using Phobos.Engine.View.Proxies.Entities;

namespace Phobos.Engine.View {
    class Animation {
        private Texture2D spriteSheet { get; set; }
        private SortedList<int, Frame> frames = new SortedList<int, Frame>();
        private IEnumerator<KeyValuePair<int, Frame>> frameEnumerator = null;
        private SpriteEffects spriteEffect = SpriteEffects.None;

        public Animation( Texture2D ss, SpriteEffects se = SpriteEffects.None ) {
            spriteSheet = ss;
            spriteEffect = se;
        }

        public void addFrame( Rectangle z, int ts ) {
            frames.Add( frames.Count, new Frame( z, ts ) );
            frameEnumerator = frames.GetEnumerator();
        }

        public int Draw( SpriteBatch spriteBatch, GameTime gameTime, DrawableEntity ent ) {
            Frame frame = getCurrentFrame( gameTime );

            spriteBatch.Draw(
                spriteSheet,
                ent.ScreenRect,
                getCurrentFrame( gameTime ).Zone,
                ent.Color,
                ent.Rotation,
                Scene.GetInstance().Camera.Position,
                spriteEffect,
                ent.Layer
                );

            return 1;
        }

        /**
         * <summary>
         * Saute de frame, si on arrive au bout du dictionnaire on recommence au debut
         * </summary>
         */
        private Frame goToNextFrame() {
            if( !frameEnumerator.MoveNext() ) {
                frameEnumerator.Reset();
                frameEnumerator.MoveNext();
            }
            return frames[ frameEnumerator.Current.Key ];
        }

        /**
         * <summary>
         * Recupere la frame courante a l'instant T (peut passer a une frame suivante)
         * </summary>
         */
        private Frame getCurrentFrame( GameTime gameTime ) {
            frames[ frameEnumerator.Current.Key ].StillDuration -= gameTime.ElapsedGameTime.Milliseconds;
            if( frames[ frameEnumerator.Current.Key ].StillDuration < 0 ) {
                frames[ frameEnumerator.Current.Key ].StillDuration = frames[ frameEnumerator.Current.Key ].OriginDuration;
                goToNextFrame();
            }
            return frames[ frameEnumerator.Current.Key ];
        }
    }
}
