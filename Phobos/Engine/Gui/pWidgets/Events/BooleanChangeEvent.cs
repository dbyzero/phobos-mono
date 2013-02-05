using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets.Events {
    class BooleanChangeEvent : EventArgs{
        public bool newValue;
        public string fieldName;

        public BooleanChangeEvent(bool _newValue, string _fieldName) {
            newValue = _newValue;
            fieldName = _fieldName;
        }
    }
}
