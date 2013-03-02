using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.View
{
    //cannot be a classe because StillDuration varying
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
