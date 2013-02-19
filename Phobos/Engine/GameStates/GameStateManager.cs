using Microsoft.Xna.Framework;
using Phobos.Engine.GameStates.Game;
using Phobos.Engine.GameStates.Menu;
using Phobos.Engine.GameStates.UiDebug;
using Phobos.Engine.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.GameStates {

    public enum GameStateList {
        MENU, GAME, UIDEBUG
        
    }

    static class GameStateManager {

        #region Fields & Properties
        #region Fields

        static Dictionary<GameStateList, AGameState> gameStates = new Dictionary<GameStateList, AGameState>();

        #endregion
        #region Properties

        #endregion
        #region Constructors & Indexer


        #endregion
        #endregion
        #region Methods

        public static void AddGameState( AGameState gameState, GameStateList stateIdentifier ) {
            gameStates.Add( stateIdentifier, gameState );
        }

        public static void RemoveGameState( GameStateList stateIdentifier ) {
            gameStates.Remove( stateIdentifier );
        }

        public static AGameState GetGameState( GameStateList stateIdentifier ) {
            return gameStates[ stateIdentifier ];
        }

        #region IGameComponent

        public static void Initialize() {
            AddGameState( new MainState(), GameStateList.GAME );
            AddGameState( new MenuGameState(), GameStateList.MENU );
            AddGameState( new UIDebugState(), GameStateList.UIDEBUG );

            foreach( KeyValuePair<GameStateList, AGameState> entry in gameStates ) {
                entry.Value.Initialize();
            }

        }

        #endregion

        #region IDrawable
        public static void Draw( GameTime gameTime ) {
            
            foreach( KeyValuePair<GameStateList, AGameState> entry in gameStates ) {
                entry.Value.Draw( gameTime );
            }

        }
        #endregion

        #region IUpdateable

        public static void Update( GameTime gameTime ) {
            GUIGlobalManager.UpdateMouseover();
            foreach( KeyValuePair<GameStateList, AGameState> entry in gameStates ) {
                entry.Value.Update( gameTime );
            }

        }

        #endregion

        #endregion

    }
}
