using Phobos.Engine.Gui.PWidgets.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets {
    class APRadioButton : APButton {

        #region Fields & Propreties
        #region Events and delegates
        public delegate void SelectedChangeHandler( object sender, SimpleValueChangedEvent<bool> e );

        public event SelectedChangeHandler SelectedChange;

        #endregion
        #region Fields

        protected bool isSelected = false;

        #endregion
        #region Properties
        public bool Selected {
            get {
                return isSelected;
            }
            set {
                if( isSelected != value ) {
                    isSelected = value;
                    OnSelectedChange();
                }
            }
        }
        #endregion
        #endregion
        #region Constructors / Indexers
        public APRadioButton( int x, int y, int width, int height )
            : base( x, y, width, height ) {

            #region Bases events
                Action += delegate( APButton sender, ActionEvent e ) {
                if( IsEnabled && IsVisible ) {
                    if( !Selected ) {
                        if( parent is APRadioGroup ) {
                            ( parent as APRadioGroup ).SetSelectedRadioButton( this );
                        }
                    }
                }

            };
            #endregion
        }
        #endregion
        #region Methods
        #region Event handling

        protected void OnSelectedChange() {
            if( SelectedChange != null ) {
                SelectedChange( this, new SimpleValueChangedEvent<bool>( Selected ) );
            }
        }

        #endregion

        #region IDrawable
        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime ) {
            
        }
        #endregion
        #endregion
    }
}
