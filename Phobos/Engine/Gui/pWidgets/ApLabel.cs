using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Phobos.Engine;
using Phobos.Engine.Gui.PWidgets.Events;

//TODO: text and font changed Event management
namespace Phobos.Engine.Gui.PWidgets {
    abstract class APLabel : APWidget {

        #region Fields and propreties
        #region Events and delegates
        public delegate void TextChangedHandler( APLabel sender, EventArgs e );

        public event TextChangedHandler TextChanged;

        #endregion
        #region Fields

        protected string text;

        #endregion
        #region Properties

        public string Text {
            get {
                return text;
            }
            set {
                if( text != value ) {
                    text = value;
                    OnTextChanged();
                }
            }
        }

        #endregion
        #endregion

        #region Constructeurs

        public APLabel( int x, int y, int width, int height, string text )
            : base( x, y, width, height ) {
            this.Text = text;

        }

        #endregion
        #region Methods
        #region Events handling

        protected void OnTextChanged() {
            if( TextChanged != null ) {
                TextChanged( this, EventArgs.Empty );
            }
        }

        #endregion
        #endregion

    }
}
