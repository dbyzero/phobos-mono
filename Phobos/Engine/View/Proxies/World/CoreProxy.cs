using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.View;
using Phobos.Engine.View.Proxies.Entities;
using Phobos.Engine.Models.World;

namespace Phobos.Engine.View.Proxies.World {
    class CoreProxy : DrawableEntity {
        private List<DrawableEntity>entities = new List<DrawableEntity>();
        public Core refCore;

        public CoreProxy( Vector3 position, int width, int height, Vector2 center, Texture2D texture, Color color )
            : base( position, width, height, center, texture, color, Orientation.S ) {
            CliffN = 0;
            CliffS = 0;
            CliffE = 0;
            CliffO = 0;
        }

        public int CliffN { get; set; }
        public int CliffS { get; set; }
        public int CliffE { get; set; }
        public int CliffO { get; set; }

        public void addEntity( DrawableEntity ent ) {
            entities.Add( ent );
        }

        public void removeEntity( DrawableEntity ent ) {
            entities.Remove( ent );
        }

        public override int Draw( SpriteBatch spriteBatch, GameTime gameTime, Scene scene ) {
            int count_sprite = 0;
            //TODO ? : Do not show hidden part
            int cliffToDraw = Math.Max( Math.Max( CliffS, CliffO ), Math.Max( CliffE, CliffN ) );
            for( int i = cliffToDraw ; i > 0 ; i-- ) {
                spriteBatch.Draw(
                    SpriteSheet,
                    new Rectangle(
                        ScreenRect.X,
                        ScreenRect.Y + (int) ( ( i - 1 ) * 16 * scene.Camera.Coefficient ),
                        (int) ( 32 * scene.Camera.Coefficient ),
                        (int) ( 32 * scene.Camera.Coefficient )
                    ),
                    new Rectangle( 192, 64, 32, 32 ),
                    SumColorLight,
                    0f,
                    scene.Camera.Position,
                    SpriteEffects.None,
                    0.000001f
                );
                count_sprite++;
            }

            count_sprite += base.Draw( spriteBatch, gameTime, scene );

            //draw owned entities
            foreach( DrawableEntity ent in entities ) {
                ent.Color = new Color(SumColorLight.R, SumColorLight.G, SumColorLight.B, ent.SumColorLight.A);
                count_sprite += ent.Draw( spriteBatch, gameTime, scene );
                ent.SumColorLight = ent.Color;
            }

            //return sprite number
            return count_sprite;
        }

        public override void checkCenter(Scene scene) {
            base.checkCenter(scene);
            foreach( DrawableEntity ent in entities ) {
                ent.checkCenter(scene);
            }
        }

        public void linkCliffs() {
        }

        public void calculCliffs(Scene scene) {
            if( scene.IsLoadedCore( (int) X, (int) Y - 1 ) ) {
                CliffN = (int) Math.Ceiling( Z - scene.GetCore( (int) X, (int) Y - 1 ).Z );
            } else {
                CliffN = (int) Math.Ceiling( Z - 0 );
            }

            if( scene.IsLoadedCore( (int) X, (int) Y + 1 ) ) {
                CliffS = (int) Math.Ceiling( Z - scene.GetCore( (int) X, (int) Y + 1 ).Z );
            } else {
                CliffS = (int) Math.Ceiling( Z - 0 );
            }

            if( scene.IsLoadedCore( (int) X + 1, (int) Y ) ) {
                CliffE = (int) Math.Ceiling( Z - scene.GetCore( (int) X + 1, (int) Y ).Z );
            } else {
                CliffE = (int) Math.Ceiling( Z - 0 );
            }

            if( scene.IsLoadedCore( (int) X - 1, (int) Y ) ) {
                CliffO = (int) Math.Ceiling( Z - scene.GetCore( (int) X - 1, (int) Y ).Z );
            } else {
                CliffO = (int) Math.Ceiling( Z - 0 );
            }
        }

        public override void applyLight(Color color) {
            base.applyLight(color);
            foreach (DrawableEntity ent in entities)
            {
                ent.applyLight(color);
            }
        }
    }
}
