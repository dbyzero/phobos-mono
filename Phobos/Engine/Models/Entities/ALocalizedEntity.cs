using Microsoft.Xna.Framework;
using Phobos.Engine.Controllers.Motion.Model;
using Phobos.Engine.Models.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Models.Entities {
    class ALocalizedEntity : AEntity {
        #region Fields & Properties
        protected Vector3 location;
        protected List<ACollisionBoundary> collisions;

        public Vector3 Location {
            get {
                return location;
            }
            set {
                location = value;
            }
        }

        #endregion
        #region Constructors & Indexer

        public ALocalizedEntity( Vector3 location )
            : base() {
            this.location = location;
            collisions = new List<ACollisionBoundary>();
        }

        #endregion
    }
}
