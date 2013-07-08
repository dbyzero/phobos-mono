using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine {
    class GraphicalHelpers {

        /// <summary>
        /// Remplis une zone destionation avec un texture source en utilisant un certain spriteBatch.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="spriteBatch"></param>
        public static void fillRectangle( Texture2D sprite, Rectangle source, Rectangle destination, SpriteBatch spriteBatch ) {

            int iterX = destination.Width / source.Width;
            int restX = destination.Width % source.Width;
            int iterY = destination.Height / source.Height;
            int restY = destination.Height % source.Height;

            Rectangle dest = new Rectangle( destination.X, destination.Y, source.Width, source.Height );

            for( int i = 0 ; i < iterX ; i++ ) {
                for( int j = 0 ; j < iterY ; j++ ) {
                    spriteBatch.Draw( sprite, dest, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                    dest.Y += dest.Height;
                }
                if( restY > 0 ) {
                    spriteBatch.Draw( sprite, new Rectangle( dest.X, dest.Y, dest.Width, restY ),
                        new Rectangle( source.X, source.Y, source.Width, restY ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                }

                dest.Y = destination.Y;
                dest.X += dest.Width;
            }
            if( restX > 0 ) {
                for( int j = 0 ; j < iterY ; j++ ) {
                    spriteBatch.Draw( sprite, new Rectangle( dest.X, dest.Y, restX, dest.Height ),
                         new Rectangle( source.X, source.Y, restX, dest.Height ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                    dest.Y += dest.Height;
                }
                if( restY > 0 ) {
                    spriteBatch.Draw( sprite, new Rectangle( dest.X, dest.Y, restX, restY ),
                        new Rectangle( source.X, source.Y, restX, restY ), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
                }
            }
        }
    }
}
