using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.Gui.pWidgets {
    abstract class ApDialog : ApWidget {
        #region Fields and Properties

        #region Private
        private bool hasFocus = true;

        #endregion

        #endregion

        #region Constructors / Indexers
        public ApDialog( ApWidget parent, Rectangle location )
            : base( parent, location ) {

        }

        #endregion

        #region Methods
        public bool HasFocus{
            get {
                return hasFocus;
            }
            set {
                hasFocus = value;
            }
        }
        #endregion

    }
}
