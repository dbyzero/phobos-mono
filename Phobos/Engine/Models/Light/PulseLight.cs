﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.Models.Light
{
    class PulseLight : ALight
    {
        public PulseLight(int radius, Vector3 position, Color color)
            : base(radius, position, color)
        {
        }
    }
}
