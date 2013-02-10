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
        SolidEntity spriteSolGrass;
        SolidEntity testAvatar;
        SpriteBatch spriteBatch;

        public MainState( GameStateManager manager )
            : base() {
            Status = GameStateStatus.Active;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            /* TEST */
            spriteSolGrass = new SolidEntity(new Vector3(41, 3, 0));
            spriteSolGrass.SpriteSheet = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            spriteSolGrass.SpriteSheetRect = new Rectangle(64, 32, 32, 32);
            spriteSolGrass.Width = 32;
            spriteSolGrass.Height = 32;
            spriteSolGrass.CenterSprite = new Vector2(16,8);

            testAvatar = new SolidEntity(new Vector3(42, 4, 0));
            testAvatar.SpriteSheet = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            testAvatar.SpriteSheetRect = new Rectangle(32, 0, 32, 32);
            testAvatar.Width = 32;
            testAvatar.Height = 32;
            testAvatar.CenterSprite = new Vector2(16, 28);

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
            
            spriteBatch.Begin();

            #region Test to draw sprites
            int j = -20;
            spriteSolGrass.X = 40;
            spriteSolGrass.Y = 20;
            spriteSolGrass.X = 39;
            spriteSolGrass.Y = 19;
            while (j < 20)
            {
                spriteSolGrass.X = 40;
                spriteSolGrass.Y = j;
                testAvatar.X = 40;
                testAvatar.Y = j;
                int i = 0;
                while (i < 40)
                {
                    spriteSolGrass.X += 1;
                    spriteSolGrass.Y = j;
                    spriteSolGrass.Draw(spriteBatch);

                    testAvatar.X += 1;
                    testAvatar.Y = j;
                    testAvatar.Draw(spriteBatch);
                    i++;
                }
                j++;
            } 
            #endregion
            spriteBatch.End(); 

            GameEngine.spriteBatch.Begin();
            returnButton.Draw(gameTime);
            exitButton.Draw(gameTime);
            GameEngine.spriteBatch.End();
            base.Draw(gameTime);

        }

        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;

            base.Update( gameTime );
            returnButton.Update(gameTime);
            exitButton.Update(gameTime);

            /** TEST KeyBoard SPACE BAR */
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) spriteSolGrass.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) spriteSolGrass.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) spriteSolGrass.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) spriteSolGrass.Y += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) spriteSolGrass.Z += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl)) spriteSolGrass.Z -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl)) spriteSolGrass.Z -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W)) spriteSolGrass.move(new Vector3(10,10,10));
        }
        #endregion
    }
}
