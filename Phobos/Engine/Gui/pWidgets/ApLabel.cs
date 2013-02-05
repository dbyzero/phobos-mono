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
        public delegate void TextChangeHandler( object sender, StringChangeEvent e );

        public event TextChangeHandler TextChange;
        #endregion

        #region Private
        private string text;

        #endregion

        #endregion

        #region Constructeurs

        public APLabel( APWidget parent, int x, int y, int width, int height, string text )
            : base( parent, x, y, width, height ) {
            this.Text = text;

        }

        #endregion

        #region Methods
        #region Accessors and mutators
        public string Text {
            get {
                return text;
            }
            set {
                if( text != value ) {
                    OnTextChanged( value, text );
                    text = value;
                }
            }
        }
        #endregion

        #region Events handling
        protected void OnTextChanged( string _new, string _previous ) {
            if( TextChange != null ) {
                TextChange( this, new StringChangeEvent( _new, _previous ) );
            }
        }
        #endregion
        #endregion

    }
}
