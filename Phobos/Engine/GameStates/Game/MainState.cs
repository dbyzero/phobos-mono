using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Phobos.Engine.Gui.PWidgets.Events;
using Phobos.Engine.Gui.PWidgets.System;
using Phobos.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Models.Entities;
using Phobos.Engine.Gui.PWidgets;

namespace Phobos.Engine.GameStates.Game {
    class MainState : AGameState{

        PSButton returnButton;
        PSButton exitButton;
        SolidEntity entity1 ;
        SpriteBatch spriteBatch;

        public MainState( GameStateManager manager )
            : base() {
            Status = GameStateStatus.Active;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            /* TEST */
            entity1 = new SolidEntity(new Vector3(41, 3, 0));
            entity1.SpriteSheet = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            entity1.SpriteRect = new Rectangle(0, 0, 32, 32);
            entity1.Width = 32;
            entity1.Height = 32;
            spriteBatch = new SpriteBatch(GameEngine.Instance.GraphicsDevice);
        }

        protected override void LoadContent() {

            returnButton = new PSButton(returnButton, 64, 64, "Retour");
            returnButton.Action += delegate( APButton  sender, ActionEvent e ) {
                ServicesManager.GetService<GameStateManager>().getGameState( GameStateList.MENU ).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            exitButton = new PSButton(exitButton, 64, 128, "Exit");
            exitButton.Action += delegate(APButton sender, ActionEvent e)
            {
                GameEngine.Instance.Exit();
            };
            base.LoadContent();
        }

        #region IDrawable

        public override void Draw( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            
            GameEngine.spriteBatch.Begin();
            returnButton.Draw(gameTime);
            exitButton.Draw(gameTime);
            GameEngine.spriteBatch.End();
            base.Draw( gameTime );

            Rectangle areaToDraw ;
            spriteBatch.Begin();

            areaToDraw = new Rectangle(
                (int)entity1.ScreenPosition.X,
                (int)entity1.ScreenPosition.Y,
                entity1.Width,
                entity1.Height);
            spriteBatch.Draw(
                entity1.SpriteSheet, 
                areaToDraw, 
                entity1.SpriteRect,
                Color.White);

            spriteBatch.End();

        }

        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;

            base.Update( gameTime );
            returnButton.Update(gameTime);
            exitButton.Update(gameTime);
            Console.WriteLine(entity1.WorldPosition.ToString());
            Console.WriteLine(entity1.ScreenPosition);

            /** TEST KeyBoard SPACE BAR */
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) entity1.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) entity1.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) entity1.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) entity1.Y += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) entity1.Z += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl)) entity1.Z -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl)) entity1.Z -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W)) entity1.move(new Vector3(10,10,10));
        }
        #endregion
    }
}
