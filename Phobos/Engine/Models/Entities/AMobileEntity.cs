using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Controllers.Motion.Model;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.Models.Entities {
    class AMobileEntity : ALocalizedEntity {
        #region Fields & Properties
        protected Vector3 velocity;
        protected List<AImpulse> impulses;
        protected PhysicsProperties physics;

        #endregion

        #region Constructors & Indexer
        public AMobileEntity( Vector3 location )
            : base( location ) {
            impulses = new List<AImpulse>();
            physics = PhysicsProperties.BasicBody;
        }

        #endregion
    }
}
