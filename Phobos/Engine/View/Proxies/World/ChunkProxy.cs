using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.View;
using Phobos.Engine.View.Proxies;
using Phobos.Engine.Models.World;

namespace Phobos.Engine.View.Proxies.World
{
    class ChunkProxy
    {
        public Chunk refChunk;

        public CoreProxy[,] cores = new CoreProxy[Chunk.CHUNKS_SIZE, Chunk.CHUNKS_SIZE];
        private float x, y;

        #region mutator et acsessor
        public double X { get; set; }
        public double Y { get; set; }
        #endregion

        public ChunkProxy(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        //le dit indexer
        public CoreProxy this[int x, int y]
        {
            get
            {
                return cores[x, y];
            }
            set
            {
                cores[x, y] = value;
            }
        }

        /**
         * <summary>Ordre d'affichage en fonction de l'orientation de la scene</summary>
         */
        public int Draw(SpriteBatch sb, GameTime gameTime)
        {
            int count_sprite = 0;
            for (int x = 0; x < Chunk.CHUNKS_SIZE; x++)
            {
                for (int y = 0; y < Chunk.CHUNKS_SIZE; y++)
                {
                    switch (Scene.getInstance().Orientation)
                    {
                        case Orientation.SE:
                            count_sprite += cores[x, y].Draw(sb, gameTime);
                            break;
                        case Orientation.SO:
                            count_sprite += cores[(Chunk.CHUNKS_SIZE - 1) - x, y].Draw(sb, gameTime);
                            break;
                        case Orientation.NE:
                            count_sprite += cores[x, (Chunk.CHUNKS_SIZE - 1) - y].Draw(sb, gameTime);
                            break;
                        case Orientation.NO:
                            count_sprite += cores[(Chunk.CHUNKS_SIZE - 1) - x, (Chunk.CHUNKS_SIZE - 1) - y].Draw(sb, gameTime);
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
            for (int x = 0; x < Chunk.CHUNKS_SIZE; x++)
            {
                for (int y = 0; y < Chunk.CHUNKS_SIZE; y++)
                {
                    switch (Scene.getInstance().Orientation)
                    {
                        case Orientation.SE:
                            cores[x, y].checkCenter();
                            break;
                        case Orientation.SO:
                            cores[(Chunk.CHUNKS_SIZE - 1) - x, y].checkCenter();
                            break;
                        case Orientation.NE:
                            cores[x, (Chunk.CHUNKS_SIZE - 1) - y].checkCenter();
                            break;
                        case Orientation.NO:
                            cores[(Chunk.CHUNKS_SIZE - 1) - x, (Chunk.CHUNKS_SIZE - 1) - y].checkCenter();
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
            for (int x = 0; x < Chunk.CHUNKS_SIZE; x++)
            {
                for (int y = 0; y < Chunk.CHUNKS_SIZE; y++)
                {
                    cores[x, y].calculCliffs();
                }
            }
        }
    }
}
