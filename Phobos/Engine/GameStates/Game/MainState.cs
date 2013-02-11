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
using Phobos.Engine.View;

namespace Phobos.Engine.GameStates.Game {
    class MainState : AGameState{

        PSButton returnButton;
        PSButton exitButton;
        SolidEntity spriteSolGrass;
        SolidEntity testAvatar;
        SpriteBatch spriteBatch;
        SceneManager sceneManager;

        public MainState( GameStateManager manager )
            : base() {
            Status = GameStateStatus.Active;
        }

        public override void Initialize()
        {
            base.Initialize();

            #region SceneManager
            sceneManager = new SceneManager();
            sceneManager.AddScene(SceneList.SO, new Scene());
            sceneManager.AddScene(SceneList.SE, new Scene());
            sceneManager.AddScene(SceneList.NO, new Scene());
            sceneManager.AddScene(SceneList.NE, new Scene());
            sceneManager.Initialize();
            #endregion
            
            /* TEST */
            spriteSolGrass = new SolidEntity(new Vector3(41, 3, 0));
            spriteSolGrass.SpriteSheet = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            spriteSolGrass.SpriteSheetRect = new Rectangle(64, 32, 32, 32);
            spriteSolGrass.Width = 32;
            spriteSolGrass.Height = 32;
            spriteSolGrass.CenterSprite = new Vector2(16,8);

            testAvatar = new SolidEntity(new Vector3(42, 4, 0));
            testAvatar.SpriteSheet = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            testAvatar.SpriteSheetRect = new Rectangle(224, 0, 32, 64);
            testAvatar.Width = 32;
            testAvatar.Height = 64;
            testAvatar.CenterSprite = new Vector2(16, 60);

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
            if (Status != GameStateStatus.Active) return;

            sceneManager.Draw(gameTime);


            #region Test to draw sprites
            spriteBatch.Begin();
            int j = -20; 
            
            while (j < 20)
            {
                spriteSolGrass.X = 20;
                spriteSolGrass.Y = j;
                testAvatar.X = 20;
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
            spriteBatch.End(); 
            #endregion

            #region Test Bench Sprite
            GameEngine.spriteBatch.Begin();
            returnButton.Draw(gameTime);
            exitButton.Draw(gameTime);
            GameEngine.spriteBatch.End(); 
            #endregion

            base.Draw(gameTime);

        }

        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;

            base.Update( gameTime );
            returnButton.Update(gameTime);
            exitButton.Update(gameTime);
            sceneManager.Update(gameTime);
        }
        #endregion
    }
}
