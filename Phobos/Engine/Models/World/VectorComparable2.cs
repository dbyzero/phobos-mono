using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.Models.World
{
    class VectorComparable2 : IComparable
    {
        private float x, y;

        public float X
        {
            get;
            set;
        }
        public float Y
        {
            get;
            set;
        }

        public VectorComparable2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public int CompareTo(Object vec)
        {
            VectorComparable2 v = (VectorComparable2)vec;
            if (v.Y > y) return 1;
            if (v.Y < y) return -1;
            if (v.X > x) return 1;
            if (v.X < x) return -1;
            return 0;
        }
    }
}
