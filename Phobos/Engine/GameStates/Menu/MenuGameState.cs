using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Gui.PWidgets;
using Phobos.Engine.Gui.PWidgets.Events;
using Phobos.Engine.Gui.PWidgets.System;
using Phobos.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.GameStates.Menu {
    class MenuGameState : AGameState{

        PSDialog menuBG;
        PSButton exitButton;
        PSButton gameButton;
        PSButton optionButton;
        PSButton uiDebugButton;

        public MenuGameState( GameStateManager manager )
            : base( ) {
                Status = GameStateStatus.Active;
        }

        protected override void LoadContent() {

            menuBG = new PSDialog( null, 32, 32, 192, GameEngine.Instance.Window.ClientBounds.Height - 64 );
            gameButton = new PSButton(gameButton, 64, 140, "Game");
            gameButton.Action += delegate( APButton sender, ActionEvent e )
            {
                ServicesManager.GetService<GameStateManager>().getGameState(GameStateList.GAME).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            optionButton = new PSButton( optionButton, 64, 180, "Options" );
            uiDebugButton = new PSButton( uiDebugButton, 64, 220, "Debug UI" );
            uiDebugButton.Action += delegate( APButton sender, ActionEvent e ) {
                ServicesManager.GetService<GameStateManager>().getGameState( GameStateList.UIDEBUG ).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            exitButton = new PSButton( exitButton, 64, 260, "Exit" );
            exitButton.Action += delegate( APButton sender, ActionEvent e ) {
                GameEngine.Instance.Exit();
            };

            base.LoadContent();
        }
        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            base.Draw( gameTime );
            GameEngine.spriteBatch.Begin();
            menuBG.Draw( gameTime );
            gameButton.Draw( gameTime );
            optionButton.Draw( gameTime );
            uiDebugButton.Draw( gameTime );
            exitButton.Draw( gameTime );
            GameEngine.spriteBatch.End();
            base.Draw( gameTime );

        }
        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            base.Update( gameTime );
            menuBG.Update( gameTime );
            gameButton.Update( gameTime );
            optionButton.Update( gameTime );
            uiDebugButton.Update( gameTime );
            exitButton.Update( gameTime );
        }
        #endregion
    }
}
