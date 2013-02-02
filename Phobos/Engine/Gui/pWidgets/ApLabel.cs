using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Phobos.Engine;

//TODO: text and font changed Event management
namespace Phobos.Engine.Gui.pWidgets {
    abstract class ApLabel : ApWidget {

        #region Fields and propreties

        #region Events
        public delegate void TextChangeHandler( object sender, TextChangedEvent e );
        public event TextChangeHandler TextChanged;

        public class TextChangedEvent : EventArgs {
            public string newText;
            public string previousText;
            public TextChangedEvent( string _newText, string _previousText ) {
                newText = _newText;
                previousText = _previousText;
            }
        }
        #endregion

        #region Private
        private string text;

        #endregion

        #endregion

        #region Constructeurs

        public ApLabel( ApWidget parent, int x, int y, int width, int height, string text )
            : base( parent, x, y, width, height ) {
            this.Text = text;

        }

        #endregion

        #region Methods

        public string Text {
            get {
                return text;
            }
            set {
                OnTextChanged( value, text );
                text = value;
            }
        }

        protected void OnTextChanged( string _new, string _previous ) {
            if( TextChanged != null ) {
                TextChanged( this, new TextChangedEvent( _new, _previous ) );
            }
        }

        #endregion

    }
}
