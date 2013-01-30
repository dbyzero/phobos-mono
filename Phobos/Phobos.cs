using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Phobos.Engine;

namespace Phobos
{
    class Phobos
    {
        static void Main(string[] args)
        {
            using (GameEngine engine = GameEngine.Instance)
            {
                engine.Run();
            }

            Console.Read();
        }
    }
}
