using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets.Events {
    public class StringChangeEvent : EventArgs {
        public string newText;
        public string previousText;
        public StringChangeEvent( string _newText, string _previousText ) {
            newText = _newText;
            previousText = _previousText;
        }
    }
}
