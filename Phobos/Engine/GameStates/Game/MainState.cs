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

namespace Phobos.Engine.GameStates.Game {
    class MainState : AGameState{

        PSButton returnButton ;
        SolidEntity entity1 ;

        public MainState()
            : base() {
            Status = GameStateStatus.Inactive;
        }

        protected override void LoadContent() {

            returnButton = new PSButton(64, 64, "Retour");
            returnButton.Action += delegate( APButton sender, ActionEvent e ) {
                GameStateManager.GetGameState( GameStateList.MENU ).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            base.LoadContent();
            entity1 = new SolidEntity(Vector3.Zero);
        }

        #region IDrawable

        public override void Draw( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            
            GameEngine.spriteBatch.Begin();
            returnButton.Draw( gameTime );
            GameEngine.spriteBatch.End();
            base.Draw( gameTime );

        }

        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            base.Update( gameTime );
            returnButton.Update( gameTime );
            Console.WriteLine(entity1.WorldPosition.ToString());
        }
        #endregion
    }
}
