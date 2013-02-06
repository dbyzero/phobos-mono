using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.GameStates {

    public enum GameStateList {
        MENU, GAME, UIDEBUG
    }

    class GameStateManager : DrawableGameComponent {

        #region Fields & Properties
        #region Fields

        private Dictionary<GameStateList, AGameState> gameStates = new Dictionary<GameStateList, AGameState>();

        #endregion
        #region Properties

        #endregion
        #region Constructors & Indexer

        public GameStateManager()
            : base( GameEngine.Instance ) {

        }

        #endregion
        #endregion

        #region Methods

        public void AddGameState( AGameState gameState, GameStateList stateIdentifier ) {
            gameStates.Add( stateIdentifier, gameState );
        }

        public void RemoveGameState( GameStateList stateIdentifier ) {
            gameStates.Remove( stateIdentifier );
        }

        public AGameState getGameState( GameStateList stateIdentifier ) {
            return gameStates[ stateIdentifier ];
        }

        #region IGameComponent

        public override void Initialize() {
            
            foreach( KeyValuePair<GameStateList, AGameState> entry in gameStates ) {
                entry.Value.Initialize(  );
            }
            base.Initialize();
        }

        #endregion

        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            
            foreach( KeyValuePair<GameStateList, AGameState> entry in gameStates ) {
                entry.Value.Draw( gameTime );
            }

            base.Draw( gameTime );
        }
        #endregion

        #region IUpdateable

        public override void Update( GameTime gameTime ) {

            foreach( KeyValuePair<GameStateList, AGameState> entry in gameStates ) {
                entry.Value.Update( gameTime );
            }

            base.Update( gameTime );
        }

        #endregion

        #endregion

    }
}
