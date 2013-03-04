using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.Engine.View
{
    struct SpriteArea
    {
        Rectangle rectangle ;
        SpriteEffects spriteEffect ;

        public Rectangle Rectangle {
            private set { rectangle = value; }
            get { return rectangle ;}
        }

        public SpriteEffects SpriteEffect
        {
            private set { spriteEffect = value; }
            get { return spriteEffect; }
        }

        public SpriteArea(Rectangle r, SpriteEffects se)
        {
            rectangle = r ;
            spriteEffect = se ;
        }
    }
}
