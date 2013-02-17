using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Models.Entities;

namespace Phobos.Engine.Models.World
{
    class Chunk
    {
        /// <summary>
        /// test // pb avec les autres camera...
        /// </summary>
        private List<Core> cores = new List<Core>() ;
        private double x, y;

        public double X { get; set; }
        public double Y { get; set; }

        public Chunk(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public void addCore(Core core) {
            cores.Add(core) ;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
           
            foreach (Core core in cores)
            {
                core.Draw(sb, gameTime);
            }
        }
    }
}
