using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phobos.Engine.Entities;

namespace Phobos.Engine.Models.Entities
{
    class SolidEntity : AEntity
    {
        private Vector3 vector3;

        public SolidEntity(Vector3 vector3) : base( vector3 )
        {
        }
        public Vector3 WorldPosition
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }
    }
}
