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

namespace Phobos.Engine.GameStates.Game {
    class MainState : AGameState{

        PSButton menuButton ;
        SolidEntity entity1 ;

        public MainState( GameStateManager manager )
            : base( ) {
                Status = GameStateStatus.Active;
        }

        protected override void LoadContent() {

            menuButton = new PSButton(menuButton, 64, 64, "Menu");
            menuButton.Action += delegate( object sender, ActionEvent e ) {
                ServicesManager.GetService<GameStateManager>().getGameState( GameStateList.MENU ).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            base.LoadContent();
            entity1 = new SolidEntity(Vector3.Zero);
        }
        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            base.Draw( gameTime );
            GameEngine.spriteBatch.Begin();
            menuButton.Draw( gameTime );
            GameEngine.spriteBatch.End();
            base.Draw( gameTime );

        }
        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            base.Update( gameTime );
            menuButton.Update( gameTime );
            Console.WriteLine(entity1.WorldPosition.ToString());
        }
        #endregion
    }
}
