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

        #region mutator et acsessor
        public double X { get; set; }
        public double Y { get; set; }
        #endregion

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

        /**
         * <summary>Ordre d'affichage en fonction de l'orientation de la scene</summary>
         */
        public int Draw(SpriteBatch sb, GameTime gameTime)
        {
            int count_sprite = 0;
            for (int x = 0; x < Chunk_Size; x++)
            {
                for (int y = 0; y < Chunk_Size; y++)
                {
                    switch (Scene.getInstance().Orientation)
                    {
                        case Orientation.SE:
                            count_sprite += cores[x, y].Draw(sb, gameTime);
                            break;
                        case Orientation.SO :
                            count_sprite += cores[(Chunk_Size - 1) - x, y].Draw(sb, gameTime);
                            break;
                        case Orientation.NE:
                            count_sprite += cores[x, (Chunk_Size - 1) - y].Draw(sb, gameTime);
                            break;
                        case Orientation.NO:
                            count_sprite += cores[(Chunk_Size - 1) - x, (Chunk_Size - 1) - y].Draw(sb, gameTime);
                            break;
                    }
                }
            }
            return count_sprite;
        }

        public void Update(GameTime gameTime)
        {
        }

        /**
         * <summary>
         * Verifie si parmis tout ses tiles, certains sont au centre de la camera
         * Ordre de calcul en fonction de l'orientation de la scene pour que le plus proche element de la camera soit selectionné
         * </summary>
         */
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

        /**
         * <summary>
         * Calcul le cliff a afficher pour tous les tiles
         * </summary>
         */
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
