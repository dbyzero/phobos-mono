using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets.Events {
    public class SimpleValueChangedEvent<T> : EventArgs {
        public T newValue;

        public SimpleValueChangedEvent( T _newValue ) {
            newValue = _newValue;
        }
    }
}
