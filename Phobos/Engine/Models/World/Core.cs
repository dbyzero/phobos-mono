using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Models.Entities;
using Phobos.Engine.View.Proxies.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Models.World {
    class Core {
        #region Fields & Properties
        public static float CORE_SIZE = 16f;
        public Vector2 location = new Vector2();
        public Chunk refChunk;
        public Dictionary<float, Layer> layerList;
        public List<AEntity> entities;

        #endregion

        #region Constructors, Builder & Indexer
        private Core( Vector2 location) {
            layerList = new Dictionary<float,Layer>();
            entities = new List<AEntity>();
        } 

        /// <summary>
        /// This method is used to access to the core located at the location. This method should only be called after map generation 
        /// so no concrete Core are created, they are only read from other source (save file or db).
        /// 
        /// </summary>
        /// <param name="mapLocation"></param>
        /// <returns></returns>
        public static Core BuildCore( Vector2 location ) {
            return new Core( location );
        }


        /// <summary>
        /// This test method just create a simple core with a single layer of dirt starting at height 0.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Core BuildSimpleCore( Vector2 location, float height ) {
            Core core = new Core( location );
            core.layerList.Add( height, Layer.DIRT );

            return core;
        }

        #endregion

        #region tests
        public float GetHeight() {
            float max = float.MinValue;
            foreach( float h in layerList.Keys ) {
                max = ( h > max ) ? h : max;
            }

            return max;
        }
        #endregion

        public CoreProxy BuildProxy() {
            Texture2D text = GameEngine.Instance.Content.Load<Texture2D>( @"spriteSheets\temp_sprite" );
            CoreProxy proxy = new CoreProxy(new Vector3(location.X, location.Y, this.GetHeight()),32,16, new Vector2(16, 8),
                text, new Color(new Vector4(0.8f, 0.8f, 0.8f, 1.0f)));
            proxy.refCore = this;

            return proxy;
        }
    }
}
