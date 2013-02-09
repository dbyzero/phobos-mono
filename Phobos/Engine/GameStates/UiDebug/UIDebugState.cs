using Microsoft.Xna.Framework;
using Phobos.Engine.Gui.PWidgets;
using Phobos.Engine.Gui.PWidgets.Events;
using Phobos.Engine.Gui.PWidgets.System;
using Phobos.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.GameStates.UiDebug {
    class UIDebugState : AGameState {

        #region Fields & Properties
        List<APWidget> Components;

        #endregion

        #region Constructors & Indexer
        public UIDebugState( GameStateManager manager )
            : base() {
            Components = new List<APWidget>();
            Status = GameStateStatus.Inactive;
        }

        #endregion

        #region Methods
        protected override void LoadContent() {
            PSButton button0 = new PSButton( null, 4, GameEngine.Instance.Window.ClientBounds.Height - 38, "Menu" );
            button0.Action += delegate( APButton sender, ActionEvent e ) {
                Status = GameStateStatus.Inactive;
                ServicesManager.GetService<GameStateManager>().getGameState( GameStateList.MENU ).Status = GameStateStatus.Active;
            };
            Components.Add( button0 );

            PSButton button1 = new PSButton( null, 5, 5, "ClickMe !" );
            this.Components.Add( button1 );
            PSButton button2 = new PSButton( null, 5, 40, "HideMe!" );
            button2.IsVisible = false;

            button1.Action += delegate( APButton sender, ActionEvent e ) {
                button2.IsVisible = true;
                Console.WriteLine( "Bravo, joli clic" );
            };
            button1.MouseoverChanged += delegate( APWidget sender, EventArgs e ) {
                if( sender.IsMouseover ) {
                    Console.WriteLine( "La souris rentre..." );
                    button1.ButtonText = "Dessus";
                }
            };
            button1.MouseoverChanged += delegate( APWidget sender, EventArgs e ) {
                if( !sender.IsMouseover ) {
                    Console.WriteLine( "...puis ressort." );
                    button1.ButtonText = "Plus dessus";
                }
            };
            button2.Action += delegate( APButton sender, ActionEvent e ) {
                button2.IsVisible = false;
                Console.WriteLine( "Ni vus,ni connus" );
            };
            PSButton button3 = new PSButton( null, 5, 75, "Disabled" );
            button3.IsEnabled = false;

            PSCheckBox checkbox1 = new PSCheckBox( null, 140, 5 );
            PSCheckBox checkbox2 = new PSCheckBox( null, 140, 25 );
            checkbox2.IsEnabled = false;
            APRadioGroup radioGroup = new APRadioGroup( null, 0, 0, 0, 0 );
            PSRadioButton radioButton1 = new PSRadioButton( radioGroup, 140, 45 );
            PSRadioButton radioButton2 = new PSRadioButton( radioGroup, 140, 65 );
            PSRadioButton radioButton3 = new PSRadioButton( radioGroup, 140, 85 );
            PSRadioButton radioButton4 = new PSRadioButton( radioGroup, 140, 105 );
            PSRadioButton radioButton5 = new PSRadioButton( radioGroup, 140, 125 );
            radioButton3.IsEnabled = false;

            radioGroup.Add( radioButton1 );
            radioGroup.Add( radioButton2 );
            radioGroup.Add( radioButton3 );
            radioGroup.Add( radioButton4 );
            radioGroup.Add( radioButton5 );

            PSDialog dialog1 = new PSDialog( null, 200, 5, 256, 256 );

            this.Components.Add( dialog1 );
            this.Components.Add( checkbox1 );
            this.Components.Add( checkbox2 );
            this.Components.Add( radioGroup );
            this.Components.Add( button2 );
            this.Components.Add( button3 );

            base.LoadContent();
        }

        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;

            GameEngine.spriteBatch.Begin();
            foreach( APWidget widget in Components ) {
                widget.Draw( gameTime );
            }
            GameEngine.spriteBatch.End();
            base.Draw( gameTime );

        }
        #endregion
        #region IUpdateable
        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;
            base.Update( gameTime );
            foreach( APWidget widget in Components ) {
                widget.Update( gameTime );
            }
            base.Update( gameTime );

        }
        #endregion
        #endregion
    }
}
