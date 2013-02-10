using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phobos.Engine.View
{
    public enum SceneList
    {
        SE, SO, NE, NO
    }

    class SceneManager : DrawableGameComponent 
    {

        #region Fields
        private Dictionary<SceneList, Scene> scenes = new Dictionary<SceneList, Scene>();
        #endregion

        #region Methods
        
        public SceneManager()
            : base( GameEngine.Instance ) {

        }

        public void AddScene( SceneList sceneIdentifier, Scene scene ) {
            scenes.Add(sceneIdentifier, scene);
        }

        public void RemoveGameState( SceneList sceneIdentifier )
        {
            scenes.Remove(sceneIdentifier);
        }

        public Scene getGameState(SceneList sceneIdentifier)
        {
            return scenes[sceneIdentifier];
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (KeyValuePair<SceneList, Scene> entry in scenes)
            {
                entry.Value.Draw(gameTime);
                // Console.WriteLine("Draw " + entry.ToString());
            }

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<SceneList, Scene> entry in scenes)
            {
                entry.Value.Update(gameTime);
               // Console.WriteLine("Update " + entry.ToString());
            }

            base.Update(gameTime);
        }

        public override void Initialize() {

            foreach (KeyValuePair<SceneList, Scene> entry in scenes)
            {
                entry.Value.Initialize(  );
                Console.WriteLine(entry.ToString());
            }
            base.Initialize();
        }
        #endregion
    }
}
