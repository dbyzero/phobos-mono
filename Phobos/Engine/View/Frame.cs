using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.View
{
    /**
     * <summary>Ne peut etre une classe car StillDuration evolut avec le temps</summary>
     */
    public class Frame
    {
        public int OriginDuration { get; set; }
        public int StillDuration { get; set; }
        public Rectangle Zone { get; set; }

        public Frame(Rectangle z, int duration_ms)
        {
            OriginDuration = duration_ms;
            StillDuration = duration_ms;
            Zone = z;
        }
    }
}
