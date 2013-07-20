using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phobos.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Phobos.Engine.Gui.PWidgets.System {
    class PSDialog : APDialog{
        #region Fields
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

        public PSDialog( int x, int y, int width, int height, GUILayer layer)
            : base( x, y, width, height, layer ) {
            if(layer != null)
                layer.Register( this );
        }

        static PSDialog() {
            sprite = ServicesManager.GetService<ContentManager>().Load<Texture2D>( @"gui\system\psDialog" );
        }

        #endregion
        #region Methods
        #region IDrawable

        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime ) {

            Rectangle drawLocation = AbsoluteLocation;
            drawLocation.X -= left.Width -3;
            drawLocation.Y -= top.Height -3;
            drawLocation.Height += bottom.Height + top.Height -3;
            drawLocation.Width += right.Width + left.Width - 3;

            GameEngine.Instance.SpriteBatch.Draw( sprite, new Rectangle( drawLocation.X, drawLocation.Y, topLeft.Width, topLeft.Height ), topLeft, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f );
            GameEngine.Instance.SpriteBatch.Draw(sprite, new Rectangle(drawLocation.X + drawLocation.Width - topRight.Width, drawLocation.Y, topRight.Width, topRight.Height), topRight, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            GameEngine.Instance.SpriteBatch.Draw(sprite, new Rectangle(drawLocation.X, drawLocation.Y + drawLocation.Height - bottomLeft.Height, bottomLeft.Width, bottomLeft.Height), bottomLeft, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            GameEngine.Instance.SpriteBatch.Draw(sprite, new Rectangle(drawLocation.X + drawLocation.Width - bottomRight.Width, drawLocation.Y + drawLocation.Height - bottomRight.Height, bottomRight.Width, bottomRight.Height), bottomRight, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            Rectangle dest = drawLocation;

            dest.X = drawLocation.X + topLeft.Width;
            dest.Y = drawLocation.Y;
            dest.Width = drawLocation.Width - topLeft.Width - topRight.Width;
            dest.Height = top.Height;

            GraphicalHelpers.fillRectangle( sprite, top, dest, GameEngine.Instance.SpriteBatch );

            dest.X = drawLocation.X + topLeft.Width;
            dest.Y = drawLocation.Y + drawLocation.Height - bottomRight.Height;
            dest.Width = drawLocation.Width - bottomRight.Width - bottomLeft.Width;
            dest.Height = bottom.Height;

            GraphicalHelpers.fillRectangle( sprite, bottom, dest, GameEngine.Instance.SpriteBatch );

            dest.X = drawLocation.X + drawLocation.Width - topRight.Width;
            dest.Y = drawLocation.Y + topRight.Height;
            dest.Width = right.Width;
            dest.Height = drawLocation.Height - topRight.Height - bottomRight.Height;

            GraphicalHelpers.fillRectangle( sprite, right, dest, GameEngine.Instance.SpriteBatch );

            dest.X = drawLocation.X;
            dest.Y = drawLocation.Y + topLeft.Height;
            dest.Width = left.Width;
            dest.Height = drawLocation.Height - topLeft.Height - bottomLeft.Height;

            GraphicalHelpers.fillRectangle( sprite, left, dest, GameEngine.Instance.SpriteBatch );

            dest.X = drawLocation.X + left.Width;
            dest.Y = drawLocation.Y + top.Height;
            dest.Width = drawLocation.Width - right.Width - left.Width;
            dest.Height = drawLocation.Height - bottom.Height - top.Height;

            GraphicalHelpers.fillRectangle( sprite, texture, dest, GameEngine.Instance.SpriteBatch );
            //Montre la drawlocation et l'absolutelocation.
            /*Texture2D rect = new Texture2D( ServicesManager.GetService<GraphicsDevice>(), 1, 1 );
            rect.SetData( new[] { Color.Blue } );
            GameEngine.Instance.SpriteBatch.Draw( rect, drawLocation, Color.Blue );
            rect.SetData( new[] { Color.Red } );
            GameEngine.Instance.SpriteBatch.Draw( rect, AbsoluteLocation, Color.Red );*/

            foreach( APWidget child in children ) {
                child.Draw( gameTime );
            }
        }

        #endregion
        #endregion

    }
}
