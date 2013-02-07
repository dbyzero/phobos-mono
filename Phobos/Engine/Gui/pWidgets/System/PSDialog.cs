using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phobos.Engine.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Services;
using Microsoft.Xna.Framework.Content;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSDialog : APDialog{
        #region Propreties & Fields
        #region Protected

        protected static Texture2D sprite;
        protected static Rectangle topLeft = new Rectangle( 0, 0, 7, 7 );
        protected static Rectangle topRight = new Rectangle( 264, 0, 13, 7 );
        protected static Rectangle bottomLeft = new Rectangle( 0, 154, 7, 13 );
        protected static Rectangle bottomRight = new Rectangle( 264, 154, 13, 13 );
        protected static Rectangle top = new Rectangle( 7, 0, 6, 7 );
        protected static Rectangle left = new Rectangle( 0, 7, 7, 4 );
        protected static Rectangle right = new Rectangle( 264, 7, 13, 1 );
        protected static Rectangle bottom = new Rectangle( 7, 154, 1, 13 );
        protected static Rectangle texture = new Rectangle( 7, 7, 6, 4 );

        #endregion
        #endregion
        #region Constructors & Indexer

        public PSDialog( APWidget parent, int x, int y, int width, int height )
            : base( parent, x, y, width, height ) {

        }

        static PSDialog() {
            sprite = ServicesManager.GetService<ContentManager>().Load<Texture2D>( @"gui\system\psDialog" );
        }

        #endregion
        #region Methods
        #region IDrawable

        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime ) {
            base.Draw( gameTime );
            
            GameEngine.spriteBatch.Draw( sprite, new Rectangle( location.X, location.Y, topLeft.Width, topLeft.Height ), topLeft, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
            GameEngine.spriteBatch.Draw( sprite, new Rectangle( location.X + location.Width - topRight.Width, location.Y, topRight.Width, topRight.Height ), topRight, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
            GameEngine.spriteBatch.Draw( sprite, new Rectangle( location.X, location.Y + location.Height - bottomLeft.Height, bottomLeft.Width, bottomLeft.Height ), bottomLeft, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
            GameEngine.spriteBatch.Draw( sprite, new Rectangle( location.X + location.Width - bottomRight.Width, location.Y + location.Height - bottomRight.Height, bottomRight.Width, bottomRight.Height), bottomRight, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
            Rectangle drawRegion = location;
            /*drawRegion.X -= left.Width;
            drawRegion.Y -= top.Height;
            drawRegion.Height += top.Height + bottom.Height;
            drawRegion.Width += right.Width + left.Width;*/

            Rectangle dest = drawRegion;

            dest.X = location.X + topLeft.Width;
            dest.Y = location.Y;
            dest.Width = location.Width - topLeft.Width - topRight.Width;
            dest.Height = top.Height;

            GraphicalHelpers.fillRectangle( sprite, top, dest, GameEngine.spriteBatch );

            dest.X = location.X + topLeft.Width;
            dest.Y = location.Y + location.Height - bottomRight.Height;
            dest.Width = location.Width - bottomRight.Width - bottomLeft.Width;
            dest.Height = bottom.Height;

            GraphicalHelpers.fillRectangle( sprite, bottom, dest, GameEngine.spriteBatch );

            dest.X = location.X + location.Width - topRight.Width;
            dest.Y = location.Y + topRight.Height;
            dest.Width = right.Width;
            dest.Height = location.Height - topRight.Height - bottomRight.Height;

            GraphicalHelpers.fillRectangle( sprite, right, dest, GameEngine.spriteBatch );

            dest.X = location.X;
            dest.Y = location.Y + topLeft.Height;
            dest.Width = left.Width;
            dest.Height = location.Height - topLeft.Height - bottomLeft.Height;

            GraphicalHelpers.fillRectangle( sprite, left, dest, GameEngine.spriteBatch );

            dest.X = location.X + left.Width;
            dest.Y = location.Y + top.Height;
            dest.Width = location.Width - right.Width - left.Width;
            dest.Height = location.Height - bottom.Height - top.Height;

            GraphicalHelpers.fillRectangle( sprite, texture, dest, GameEngine.spriteBatch );

        }

        #endregion
        #endregion

    }
}
