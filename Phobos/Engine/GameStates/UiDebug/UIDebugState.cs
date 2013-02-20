using Microsoft.Xna.Framework;
using Phobos.Engine.Gui;
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
        Dictionary<GUIStratas, GUIStrataManager> stratasManager;
        List<APWidget> Components;
        #endregion

        #region Constructors & Indexer
        public UIDebugState()
            : base() {
            Components = new List<APWidget>();
            Status = GameStateStatus.Inactive;
            stratasManager = new Dictionary<GUIStratas, GUIStrataManager>();
        }

        #endregion

        #region Methods
        protected override void LoadContent() {
            stratasManager.Add( GUIStratas.BACKGROUND, new GUIStrataManager( GUIStratas.BACKGROUND ) );
            stratasManager.Add( GUIStratas.CONTROLS, new GUIStrataManager( GUIStratas.CONTROLS ) );
            stratasManager.Add( GUIStratas.DEFAULT, new GUIStrataManager( GUIStratas.DEFAULT ) );
            stratasManager.Add( GUIStratas.IMPORTANT, new GUIStrataManager( GUIStratas.IMPORTANT ) );
            stratasManager.Add( GUIStratas.DIALOG, new GUIStrataManager( GUIStratas.DIALOG ) );
            stratasManager.Add( GUIStratas.FULLSCREEN, new GUIStrataManager( GUIStratas.FULLSCREEN ) );
            stratasManager.Add( GUIStratas.FULLSCREEN_DIALOG, new GUIStrataManager( GUIStratas.FULLSCREEN_DIALOG ) );
            stratasManager.Add( GUIStratas.SYSTEM, new GUIStrataManager( GUIStratas.SYSTEM ) );
            stratasManager.Add( GUIStratas.SYSTEM_DIALOG, new GUIStrataManager( GUIStratas.SYSTEM_DIALOG ) );

            PSButton button0 = new PSButton( 4, GameEngine.Instance.Window.ClientBounds.Height - 38, "Menu" );
            button0.Action += delegate( APButton sender, ActionEvent e ) {
                Status = GameStateStatus.Inactive;
               GameStateManager.GetGameState( GameStateList.MENU ).Status = GameStateStatus.Active;
            };
            Components.Add( button0 );

            PSButton button1 = new PSButton( 5, 5, "ClickMe !" );
            this.Components.Add( button1 );
            PSButton button2 = new PSButton( 5, 40, "HideMe!" );
            button2.IsVisible = false;
            PSButton button3 = new PSButton( 5, 75, "Disabled" );
            button3.IsEnabled = false;

            PSCheckBox checkbox1 = new PSCheckBox( 140, 5 );
            PSCheckBox checkbox2 = new PSCheckBox( 140, 25 );
            checkbox2.IsEnabled = false;
            
            APRadioGroup radioGroup = new APRadioGroup( 0, 0, 0, 0 );
            PSRadioButton radioButton1 = new PSRadioButton( 140, 45 );
            radioGroup.Add( radioButton1 );
            PSRadioButton radioButton2 = new PSRadioButton( 140, 65 );
            radioGroup.Add( radioButton2 );
            PSRadioButton radioButton3 = new PSRadioButton( 140, 85 );
            radioGroup.Add( radioButton3 );
            PSRadioButton radioButton4 = new PSRadioButton( 140, 105 );
            radioGroup.Add( radioButton4 );
            PSRadioButton radioButton5 = new PSRadioButton( 140, 125 );
            radioGroup.Add( radioButton5 );
            radioButton3.IsEnabled = false;

            radioGroup.Add( radioButton1 );
            radioGroup.Add( radioButton2 );
            radioGroup.Add( radioButton3 );
            radioGroup.Add( radioButton4 );
            radioGroup.Add( radioButton5 );

            
            PSDialog dialog1 = new PSDialog( 200, 5, 256, 256, stratasManager[ GUIStratas.DIALOG ].BuildLayer( "dialogTest1") );
            PSDialog dialog2 = new PSDialog( 160, 25, 256, 256, stratasManager[ GUIStratas.BACKGROUND ].BuildLayer( "dialogTest2" ) );
            PSButton button4 = new PSButton( 5, 75, "DONOT!" );

            PSCheckBox checkbox3 = new PSCheckBox( 0, 5 );
            dialog1.Add( checkbox3 );
            PSCheckBox checkbox4 = new PSCheckBox( 0, 25 );
            dialog1.Add( checkbox4 );
            checkbox4.IsEnabled = false;

            dialog2.Add( button4 );

            button4.MouseoverChanged += delegate( APWidget sender, EventArgs e ) {
                if( sender.IsMouseover ) {
                    Console.WriteLine( "La souris rentre ... ET DEVRAIT PAS" );
                }
            };
            button4.MouseoverChanged += delegate( APWidget sender, EventArgs e ) {
                if( !sender.IsMouseover ) {
                    Console.WriteLine( "...puis ressort ET DEVRAIT PAS." );
                }
            };

            this.Components.Add( radioGroup );
            this.Components.Add( button2 );
            this.Components.Add( button3 );
            this.Components.Add( checkbox1 );
            this.Components.Add( checkbox2 );
            base.LoadContent();
        }

        #region IDrawable
        public override void Draw( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;


            GameEngine.spriteBatch.Begin();
            foreach( APWidget widget in Components ) {
                widget.Draw( gameTime );
            }

            foreach( int strata in Enum.GetValues( typeof( GUIStratas ) ) ) {
                
                if( stratasManager[ (GUIStratas) strata ] != null ) {

                    foreach( GUILayer layer in stratasManager[ (GUIStratas) strata ].StrataLayers.Values ) {
                        foreach( APWidget widget in layer.RegistredWidgets ) {
                            widget.Draw( gameTime );
                        }
                    }
                }
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

            foreach( int strata in Enum.GetValues( typeof( GUIStratas ) ) ) {

                if( stratasManager[ (GUIStratas) strata ] != null ) {

                    foreach( GUILayer layer in stratasManager[ (GUIStratas) strata ].StrataLayers.Values ) {
                        foreach( APWidget widget in layer.RegistredWidgets ) {
                            widget.Update( gameTime );
                        }
                    }
                }
            }

            base.Update( gameTime );

        }

        #endregion
        #endregion
    }
}
