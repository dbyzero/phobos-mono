using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets.Events {
    public class ValueChangedEvent<T> : SimpleValueChangedEvent<T> {
        public T previousValue;
        public ValueChangedEvent( T _newValue, T _previousValue )
            : base( _newValue ) {

                previousValue = _previousValue;
        }
    }
}
