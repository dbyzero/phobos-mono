using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.View
{
    class Scene : DrawableGameComponent
    {
        private Camera camera;
        private Boolean active ;

        public Boolean Active {
            get { return active; }
            set { active = value; }
        }

        public Scene()
            : base(GameEngine.Instance)
        {
            camera = new Camera(Rectangle.Empty);
            active = false;
        }

        #region Ascessor et mutator
        
        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Active) return;
            //TODO DRAW SCENE
            base.Draw(gameTime);
        }
        
        public override void Update(GameTime gameTime)
        {
            if (!active) return;
            base.Update(gameTime);
        }

        #endregion
    }
}