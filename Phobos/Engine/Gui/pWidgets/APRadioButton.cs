using Phobos.Engine.Gui.PWidgets.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets {
     class APRadioButton : APButton{

        #region Fields & Propreties
        #region Events and delegates
        public delegate void SelecteChangeHandler(object sender, BooleanChangeEvent e);

        public event SelecteChangeHandler Unselecte;
        public event SelecteChangeHandler Selecte;
        public event SelecteChangeHandler SelecteChange;

        #endregion

        #region Protected
        protected bool isSelected = false;
        #endregion
        #endregion

        #region Constructors / Indexers
        public APRadioButton( APWidget parent, int x, int y, int width, int height )
            : base( parent, x, y, width, height ) {

            #region Bases events
            Action += delegate( object sender, ActionEvent e ) {
                if(Activated && Visible){
                    if( !Selected ) {
                        if( Parent is APRadioGroup ) {
                            ( Parent as APRadioGroup ).SetSelectedRadioButton( this );
                        }
                    }
                }
                
            };
            #endregion
        }
        #endregion

        #region Methods
        #region Accessors and mutators
        public bool Selected {
            get {
                return isSelected;
            }
            set {
                isSelected = value;
                if( value ) {
                    OnSelecte();
                    
                } else {
                    OnUnselecte();
                }
            }
        }
        #endregion

        #region Event handling
        protected void OnSelecte() {
            if( Selecte != null ) {
                Selecte( this, new BooleanChangeEvent( true, "IsSelected" ) );
                OnSelecteChange();
            }
        }

        protected void OnUnselecte() {
            if( Unselecte != null ) {
                Unselecte( this, new BooleanChangeEvent( false, "IsSelected" ) );
                OnSelecteChange();
            }
        }

        protected void OnSelecteChange() {
            if( SelecteChange != null ) {
                SelecteChange( this, new BooleanChangeEvent( Selected, "IsSelected" ) );
            }
        }
        #endregion

        #region IUpdateable
        public override void Update( Microsoft.Xna.Framework.GameTime gameTime ) {
            base.Update( gameTime );
        }   
        #endregion
        #endregion
    }
}
