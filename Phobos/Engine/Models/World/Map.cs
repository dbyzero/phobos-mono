using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Models.World {
    class Map {
        #region Fields & Properties
        public const int MAP_SIZE = 3;
        protected Dictionary<int, Dictionary<int, Chunk>> chunks;

        #endregion
        #region Constructors, Builders & Indexer
        public Map() {
            chunks = new Dictionary<int, Dictionary<int, Chunk>>();
        }

        public static Map BuildFlatMap( int size = MAP_SIZE ){
            Map map = new Map();

            for( int i = 0 ; i < MAP_SIZE ; i++ ) {
                for( int j = 0 ; j < MAP_SIZE ; j++ ) {
                    map[ i, j ] = Chunk.BuildFlatChunk( new Vector2( i * Chunk.CHUNKS_SIZE, j * Chunk.CHUNKS_SIZE) );
                    map[ i, j ].refMap = map;
                }
            }

            return map;
        }

        public Chunk this[ int x, int y ] {
            get {
                return chunks[ x ][ y ];
            }
            set {
                if( !chunks.ContainsKey( x ) ) {
                    chunks.Add( x, new Dictionary<int, Chunk>() );
                    chunks[ x ].Add( y, value );
                } else {
                    if( !chunks[ x ].ContainsKey( y ) ) {
                        chunks[ x ][ y ] = value;
                    } else {
                        chunks[ x ].Add( y, value );
                    }
                }
            }
        }

        #endregion
    }
}
