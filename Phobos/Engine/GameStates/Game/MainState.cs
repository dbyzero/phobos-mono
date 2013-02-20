using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
using System.Diagnostics;
using Phobos.Engine.Models.World;

namespace Phobos.Engine.GameStates.Game {
    class MainState : AGameState{

        PSButton returnButton;
        PSButton exitButton;
        PSButton cameraSO;
        PSButton cameraSE;
        PSButton cameraNO;
        PSButton cameraNE;
        Scene scene ;

        public MainState()
            : base() {
            Status = GameStateStatus.Active;
        }

        public override void Initialize()
        {
            base.Initialize();
            scene = Scene.getInstance();
            scene.Initialize();
        }

        protected override void LoadContent() {

            returnButton = new PSButton(64, 64, "Retour");
            returnButton.Action += delegate( APButton sender, ActionEvent e ) {
                GameStateManager.GetGameState( GameStateList.MENU ).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            exitButton = new PSButton(64, 128, "Exit");
            exitButton.Action += delegate(APButton sender, ActionEvent e)
            {
                GameEngine.Instance.Exit();

            };
            cameraSE = new PSButton(64, 200, "Camera SE");
            cameraSE.Action += delegate(APButton sender, ActionEvent e)
            {
                //Console.WriteLine(scene.Camera);
                Scene.getInstance().Camera.turnCamera(Orientation.SE);
                //Console.WriteLine(scene.Camera);
            };
            cameraSO = new PSButton(64, 240, "Camera SO");
            cameraSO.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().Camera.turnCamera(Orientation.SO);
            };
            cameraNO = new PSButton(64, 280, "Camera NO");
            cameraNO.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().Camera.turnCamera(Orientation.NO);
            };
            cameraNE = new PSButton(64, 320, "Camera NE");
            cameraNE.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().Camera.turnCamera(Orientation.NE);
            };
            base.LoadContent();
        }

        #region IDrawable

        public override void Draw( GameTime gameTime ) {
            if (Status != GameStateStatus.Active) return;

            scene.Draw(gameTime);

            #region UI
            GameEngine.spriteBatch.Begin();
            returnButton.Draw(gameTime);
            exitButton.Draw(gameTime);
            cameraSE.Draw(gameTime);
            cameraSO.Draw(gameTime);
            cameraNE.Draw(gameTime);
            cameraNO.Draw(gameTime);
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
            scene.Update(gameTime);
            cameraSE.Update(gameTime);
            cameraSO.Update(gameTime);
            cameraNE.Update(gameTime);
            cameraNO.Update(gameTime);
        }
        #endregion
    }
}
