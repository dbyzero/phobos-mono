using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.View
{
    class Camera        
    {
        private Rectangle frame;
        private int coefficient;
        public Camera(Rectangle cadre)
        {
            frame = cadre;
            coefficient = 1; 
        }

        public Rectangle Frame { 
            get { return frame; } 
            set { frame = value; } 
        }

        public int Coefficient { 
            get { return coefficient; } 
            set { coefficient = value; } 
        }

    }
}
