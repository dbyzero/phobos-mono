using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.Engine.View
{
    struct Sprite
    {
        Rectangle rectangle ;
        SpriteEffects spriteEffect ;

        public Rectangle Rectangle {
            set { rectangle = value; }
            get { return rectangle ;}
        }

        public SpriteEffects SpriteEffect
        {
            set { spriteEffect = value; }
            get { return spriteEffect; }
        }

        public Sprite(Rectangle r, SpriteEffects se)
        {
            rectangle = r ;
            spriteEffect = se ;
        }
    }
}
