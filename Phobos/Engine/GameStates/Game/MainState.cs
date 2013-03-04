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
        PSButton btnCameraSO;
        PSButton btnCameraSE;
        PSButton btnCameraNO;
        PSButton brnCameraNE;
        PSButton btnColorSunrise;
        PSButton btnColorNoon;
        PSButton btnColorNight;
        PSButton btnColorEvening;
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

            returnButton = new PSButton(64, 64, "Options");
            returnButton.Action += delegate( APButton sender, ActionEvent e ) {
                GameStateManager.GetGameState( GameStateList.MENU ).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            exitButton = new PSButton(64, 128, "Exit");
            exitButton.Action += delegate(APButton sender, ActionEvent e)
            {
                GameEngine.Instance.Exit();

            };
            btnCameraSE = new PSButton(64, 200, "Camera SE");
            btnCameraSE.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().Camera.turnCamera(Orientation.SE);
            };
            btnCameraSO = new PSButton(64, 240, "Camera SW");
            btnCameraSO.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().Camera.turnCamera(Orientation.SO);
            };
            btnCameraNO = new PSButton(64, 280, "Camera NW");
            btnCameraNO.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().Camera.turnCamera(Orientation.NO);
            };
            brnCameraNE = new PSButton(64, 320, "Camera NE");
            brnCameraNE.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().Camera.turnCamera(Orientation.NE);
            };
            btnColorSunrise = new PSButton(64, 380, "Color Sunrise");
            btnColorSunrise.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().ConvergeColor = Scene.getInstance().SunriseColor;
            };
            btnColorNoon = new PSButton(64, 420, "Color Noon");
            btnColorNoon.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().ConvergeColor = Scene.getInstance().NoonColor;
            };
            btnColorEvening = new PSButton(64, 460, "Color Evening");
            btnColorEvening.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().ConvergeColor = Scene.getInstance().EveningColor;
            };
            btnColorNight = new PSButton(64, 500, "Color Night");
            btnColorNight.Action += delegate(APButton sender, ActionEvent e)
            {
                Scene.getInstance().ConvergeColor = Scene.getInstance().NightColor;
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
            btnCameraSE.Draw(gameTime);
            btnCameraSO.Draw(gameTime);
            brnCameraNE.Draw(gameTime);
            btnCameraNO.Draw(gameTime);
            btnColorNight.Draw(gameTime);
            btnColorEvening.Draw(gameTime);
            btnColorSunrise.Draw(gameTime);
            btnColorNoon.Draw(gameTime);
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
            btnCameraSE.Update(gameTime);
            btnCameraSO.Update(gameTime);
            brnCameraNE.Update(gameTime);
            btnCameraNO.Update(gameTime);
            btnColorNight.Update(gameTime);
            btnColorEvening.Update(gameTime);
            btnColorSunrise.Update(gameTime);
            btnColorNoon.Update(gameTime);
        }
        #endregion
    }
}
