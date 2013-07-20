using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.View.Proxies.World;
using Phobos.Engine.View.Proxies.Entities;
using Microsoft.Xna.Framework.Input;
using Phobos.Engine.Models.World;
using Phobos.Models.World.Generators;
using Phobos.Engine.Models.Light;
using Phobos.Engine.Controllers.Light;
using Phobos.Engine.View;
using Phobos.Engine;

namespace Phobos.Test
{
    interface ITest
    {
        void Initialize(Scene scene) ;
        void Update(Scene scene) ;
        void Draw(Scene scene) ;
    }
}
