using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Models.World {
    class Chunk {
        #region Fields & Properties
        public const int CHUNKS_SIZE = 8;
        public Map refMap;
        public Core[,] cores;
        public Vector2 location;

        #endregion
        #region Constructors, Builders & Indexer
        /// <summary>
        /// The chunks constructor should not be called after the map generation, so use BuildChunk to access to the Chunks;
        /// </summary>
        /// <param name="size"></param>
        private Chunk( int size = CHUNKS_SIZE ) {
            cores = new Core[ size, size ];
        }

        /// <summary>
        /// This method create an instance of the Chunk using data from other source (save file or db).
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Chunk BuildChunk( Vector2 location ) {
            return new Chunk();
        }


        /// <summary>
        /// This method create a flat chunk for testing purpose. The first layer of childs core is set to 0 and DIRT.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Chunk BuildFlatChunk( Vector2 location ) {
            Chunk chunk = new Chunk();
            chunk.location = location;
            for( int i = 0 ; i < CHUNKS_SIZE ; i++ ) {
                for( int j = 0 ; j < CHUNKS_SIZE ; j++ ) {
                    chunk[ i, j ] = Core.BuildSimpleCore( new Vector2( location.X + i, location.Y + j), 0f );
                    chunk[ i, j ].refChunk = chunk;
                }
            }

            return chunk;
        }

        public Core this[ int x, int y ] {
            get {
                return cores[ x, y ];
            }
            set {
                cores[ x, y ] = value;
            }
        }


        #endregion
        #region

        #endregion

    }
}
