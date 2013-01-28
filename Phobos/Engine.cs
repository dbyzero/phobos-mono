using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Phobos.Configurations.Keyboard;
using Phobos.Configurations;
using System.Configuration;
using Nini.Config;
using System.Windows.Forms;

namespace Phobos.Engine
{

    class GameEngine : Microsoft.Xna.Framework.Game
    {

        #region Fields & Properties
        GraphicsDeviceManager manager;
        SpriteBatch spriteBatch;
        DisplayModeCollection displayModes;
        #endregion

        #region Constructors
        public GameEngine(){
            this.manager = new GraphicsDeviceManager(this);
            PhobosConfigurationManager.Init("Phobos.ini");
            PhobosConfigurationManager.SetAutoSave(true);
            Content.RootDirectory = "assets";
            
        }
        #endregion

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            #region Initialisation des paramètres graphiques.
            // TODO: Géré la config externe
            this.displayModes = GraphicsDevice.Adapter.SupportedDisplayModes;
            this.Window.AllowUserResizing = false;
            this.Window.Title = "Phobos";
            #endregion

            #region Initialisation du clavier
            string currentLayout = (new WindowsKeyboardExtractor()).getCurrentSystemLayout();
            if (PhobosConfigurationManager.source.Configs["Keyboard"] == null || PhobosConfigurationManager.source.Configs["Keyboard"].Get("lastKeyboardLayout") == null)
            {
                Console.WriteLine("boum");
                PhobosConfigurationManager.set("Keyboard", "lastKeyboardLayout", currentLayout);
            }
            else
            {
                PhobosConfigurationManager.set("Keyboard", "lastKeyboardLayout", currentLayout);
            }
            #endregion
            // TODO: Add your initialization logic here
            
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // Add your drawing code here
            base.Draw(gameTime); // Garder en fin de procédure ?
        }

        /// <summary>
        /// Cette méthode permet de mettre le jeu en pleine écran tout en conservant l'état de fenêtre (alt tab plus rapides :P);
        /// </summary>
        protected void SetWindowedFullScreen()
        {
            //On met a jour la résolution d'écran sur la résolution du bureau;
            DisplayMode screen = this.displayModes.Last<DisplayMode>();
            this.manager.PreferredBackBufferWidth = screen.Width;
            this.manager.PreferredBackBufferHeight = screen.Height;
            this.manager.PreferredBackBufferFormat = screen.Format;
            this.manager.ApplyChanges(); 

            //on masque récupère la fenêtre.
            Form MyGameForm = (Form)Form.FromHandle(this.Window.Handle);
            MyGameForm.FormBorderStyle = FormBorderStyle.None; //Retrait de la bordure
            MyGameForm.Location = new System.Drawing.Point(0, 0); // on place la fenêtre correctement à l'origine de l'écran.
        }
    }
    
}
