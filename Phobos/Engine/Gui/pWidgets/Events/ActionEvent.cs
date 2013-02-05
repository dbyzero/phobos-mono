using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Phobos.Engine.Gui.PWidgets.Events {
    class ActionEvent : EventArgs {
        public MouseState mouse;
        public ActionEvent() {
            mouse = new MouseState();
        }
    }
}
