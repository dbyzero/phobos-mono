using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Models.World;

namespace Phobos.Engine.View
{
    class Scene : DrawableGameComponent
    {
        private Camera cameraNO;
        private Camera cameraNE;
        private Camera cameraSO;
        private Camera cameraSE;
        SpriteBatch spriteBatch;

        private Camera activeCamera;

        /// <summary>
        /// test // pb avec les autres camera...
        /// </summary>
        private SortedDictionary<VectorComparable2, Chunk> chunks = new SortedDictionary<VectorComparable2, Chunk>();

        public Scene()
            : base(GameEngine.Instance)
        {
            cameraNO = new Camera(Orientation.NO);
            cameraNE = new Camera(Orientation.NE);
            cameraSO = new Camera(Orientation.SO);
            cameraSE = new Camera(Orientation.SE);
            activeCamera = cameraSE;
            spriteBatch = new SpriteBatch(GameEngine.Instance.GraphicsDevice);
            
            chunks.Add(new VectorComparable2(1,2), new Chunk());
            chunks.Add(new VectorComparable2(2,1), new Chunk());
            VectorComparable2 v1 = new VectorComparable2(1, 2);
            VectorComparable2 v2 = new VectorComparable2(1, 1);
            Console.WriteLine(v1.CompareTo(v1));
        }

        #region Ascessor et mutator
        
        public Camera currentCamera
        {
            get { return activeCamera; }
            set { activeCamera = value; }
        }
        #endregion

        public Camera switchCamera(Orientation or)
        {
            switch (or)
            {
                case Orientation.SE:
                    activeCamera = cameraSE;
                    break;
                case Orientation.SO:
                    activeCamera = cameraSO;
                    break;
                case Orientation.NE:
                    activeCamera = cameraNE;
                    break;
                case Orientation.NO:
                    activeCamera = cameraNO;
                    break;
            }
            return activeCamera;
        }

        public void move(Vector2 v)
        {
            activeCamera.move(v);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

    }
}