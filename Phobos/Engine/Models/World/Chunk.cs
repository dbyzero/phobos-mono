using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Models.Entities;
using Phobos.Engine.View;

namespace Phobos.Engine.Models.World
{
    class Chunk
    {
        
        public static int Chunk_Size = 30 ;
        private Core[,] cores = new Core[Chunk_Size, Chunk_Size];
        private double x, y;

        public double X { get; set; }
        public double Y { get; set; }

        public Chunk(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public void addCore(int x, int y , Core core) {
            cores[x,y] = core ;
        }

        public Core getCore(int x, int y)
        {
            return cores[x, y];
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
           for (int x = 0; x < Chunk_Size; x++)
            {
                for (int y = 0; y < Chunk_Size; y++)
                {
                    switch (Scene.getInstance().Orientation)
                    {
                        case Orientation.SE:
                            cores[x, y].Draw(sb, gameTime);
                            break;
                        case Orientation.SO :
                            cores[(Chunk_Size - 1) - x, y].Draw(sb, gameTime);
                            break;
                        case Orientation.NE:
                            cores[x, (Chunk_Size - 1) - y].Draw(sb, gameTime);
                            break;
                        case Orientation.NO:
                            cores[(Chunk_Size - 1) - x, (Chunk_Size - 1) - y].Draw(sb, gameTime);
                            break;
                    }
                    
                }
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void CalculCenterEntity()
        {
            for (int x = 0; x < Chunk_Size; x++)
            {
                for (int y = 0; y < Chunk_Size; y++)
                {
                    switch (Scene.getInstance().Orientation)
                    {
                        case Orientation.SE:
                            cores[x, y].checkCenter();
                            break;
                        case Orientation.SO:
                            cores[(Chunk_Size - 1) - x, y].checkCenter();
                            break;
                        case Orientation.NE:
                            cores[x, (Chunk_Size - 1) - y].checkCenter();
                            break;
                        case Orientation.NO:
                            cores[(Chunk_Size - 1) - x, (Chunk_Size - 1) - y].checkCenter();
                            break;
                    }

                }
            }
        }

        public void calculCliffs()
        {
            for (int x = 0; x < Chunk_Size; x++)
            {
                for (int y = 0; y < Chunk_Size; y++)
                {
                    cores[x, y].calculCliffs();
                }
            }
        }
    }
}
