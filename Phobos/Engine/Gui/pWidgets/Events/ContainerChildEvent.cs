using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets.Events {
    class ContainerChildEvent : EventArgs{
        public APWidget child;

        public ContainerChildEvent( APWidget _child ) {
            child = _child;
        }
    }
}
