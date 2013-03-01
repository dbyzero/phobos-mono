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
        public TimeSpan OriginDuration { get; set; }
        public TimeSpan StillDuration { get; set; }
        public Rectangle Zone { get; set; }

        public Frame(Rectangle z, TimeSpan ts)
        {
            OriginDuration = ts;
            StillDuration = ts;
            Zone = z;
        }
    }
}
