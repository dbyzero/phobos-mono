using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phobos.Engine.Gui.PWidgets.Events;

namespace Phobos.Engine.Gui.PWidgets {
    abstract class APCheckBox : APButton {

        #region Fields & Propreties
        #region Events and delegates
        public delegate void CheckedChangeHandler(object sender, BooleanChangeEvent e);

        public event CheckedChangeHandler CheckedChange;

        #endregion

        #region Protected
        protected bool isChecked = false;
        #endregion
        #endregion

        #region Constructors / Indexers
        public APCheckBox( APWidget parent, int x, int y, int width, int height )
            : base( parent, x, y, width, height ) {
            #region Bases events
            Action += delegate( object sender, ActionEvent e ) {
                if(Activated && Visible){
                    if( Checked ) {
                        Checked = false;
                    } else {
                        Checked = true;
                    }
                }
                
            };
            #endregion
        }
        #endregion

        #region Methods
        #region Accessors and mutators
        public bool Checked {
            get {
                return isChecked;
            }
            set {
                if( isChecked != value ) {
                    isChecked = value;
                    OnCheckedChange();
                }
            }
        }
        #endregion

        #region Event handling

        protected void OnCheckedChange() {
            if(CheckedChange != null){
                CheckedChange( this, new BooleanChangeEvent( Checked, "Checked" ) );
            }
        }

        #endregion

        #region IUpdateable

        public override void Update(GameTime gameTime){
 	         base.Update(gameTime);
        }

        #endregion
        #endregion
    }
}
