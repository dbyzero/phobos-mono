using Phobos.Engine.Gui.PWidgets.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Gui.PWidgets {
    abstract class APContainer : APWidget {
        #region Fields & Properties
        #region Events & Delegates

        public event EventHandler<ContainerChildEvent> ChildAdded;
        public event EventHandler<ContainerChildEvent> ChildRemoved;

        #endregion
        #region Fields

        protected List<APWidget> children;

        #endregion
        #endregion

        #region Constructors & Indexer
        public APContainer( APWidget parent, int x, int y, int width, int height )
            : base( parent, x, y, width, height ) {
            children = new List<APWidget>();
        }
        #endregion

        #region Methods
        public override void Translate( int dx, int dy ) {
            base.Translate( dx, dy );
            foreach( APWidget Child in children ) {
                Child.Translate( dx, dy );
            }
        }

        public void Add( APWidget item ) {
            children.Add( item );
            OnChildAdded( item );
        }

        public void Remove( APWidget item ) {
            children.Remove( item );
            OnChildRemoved( item );
        }

        #region Events handling

        private void OnChildAdded( APWidget child ) {
            if( ChildAdded != null ) {
                ChildAdded( this, new ContainerChildEvent( child ) );
            }
        }

        private void OnChildRemoved( APWidget child ) {
            if( ChildRemoved != null ) {
                ChildRemoved( this, new ContainerChildEvent( child ) );
            }
        }

        #endregion
        #endregion
    }
}
