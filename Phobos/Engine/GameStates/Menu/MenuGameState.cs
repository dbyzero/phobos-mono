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

        public MenuGameState()
            : base( ) {
                Status = GameStateStatus.Active;
        }

        protected override void LoadContent() {

            menuBG = new PSDialog( 32, 32, 192, GameEngine.Instance.Window.ClientBounds.Height - 64, null );
            gameButton = new PSButton(32, 64, "Game");
            gameButton.Action += delegate( APButton sender, ActionEvent e )
            {
                GameStateManager.GetGameState(GameStateList.GAME).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            menuBG.Add( gameButton );
            optionButton = new PSButton( 32, 104, "Options" );
            menuBG.Add( optionButton );
            uiDebugButton = new PSButton( 32, 144, "Debug UI" );
            uiDebugButton.Action += delegate( APButton sender, ActionEvent e ) {
                GameStateManager.GetGameState( GameStateList.UIDEBUG ).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            menuBG.Add( uiDebugButton );
            exitButton = new PSButton( 32, 184, "Exit" );
            exitButton.Action += delegate( APButton sender, ActionEvent e ) {
                GameEngine.Instance.Exit();
            };
            menuBG.Add( exitButton );
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
