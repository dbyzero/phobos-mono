using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.GameStates {
    #region Enumerations
    public enum GameStateStatus {
        Active, Activating, Leaving, Inactive
    }
    #endregion

    abstract class AGameState : DrawableGameComponent{
        #region Fields & Properties
        #region Fields

        protected GameStateManager stateManager;
        private GameStateStatus status;

        #endregion

        #region Properites

        public GameStateStatus Status {
            get {
                return status;
            }
            set {
                status = value;
            }
        }

        #endregion

        #endregion
        #region Constructors & Indexer

        public AGameState(GameStateManager manager) : base(GameEngine.Instance){
            status = GameStateStatus.Inactive;
            stateManager = manager;
        }

        #endregion
        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            base.Draw( gameTime );
        }
        #endregion
        #region IUpdateable
        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            base.Update( gameTime );
        }
        #endregion
    }
}
