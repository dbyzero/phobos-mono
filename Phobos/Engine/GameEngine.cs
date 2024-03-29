﻿    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Phobos.Configurations.Keyboard;
using Phobos.Configurations;
using System.Configuration;
using Nini.Config;
using System.Windows.Forms;
using Phobos.Engine.Gui;
using Phobos.Engine.Gui.PWidgets;
using Phobos.Engine.Gui.PWidgets.System;
using Phobos.Engine.Gui.PWidgets.Events;
using Phobos.Engine.Inputs.MouseInput;
using Phobos.Engine.GameStates;
using Phobos.Engine.GameStates.Game;
using Phobos.Engine.GameStates.Menu;
using Phobos.Engine.GameStates.UiDebug;
using Phobos.Engine.Services;

namespace Phobos.Engine
{

    class GameEngine : Microsoft.Xna.Framework.Game
    {

        #region fields
        private static GameEngine instance { get; set; }

        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager deviceManager;
        private DisplayModeCollection displayModes;
        #endregion

        #region Constructors / Instanciateur
        private GameEngine(){
            this.deviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public static GameEngine Instance {
            get {
                if (instance == null) {
                    instance = new GameEngine();
                }
                return instance;
            }
        }

        #endregion

        #region Ascesseur
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }
        public GraphicsDeviceManager DeviceManager
        {
            get { return deviceManager; }
        }

        #endregion

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {

            //this.deviceManager.GraphicsDevice.SamplerStates[0].Filter = TextureFilter.Linear;

            #region Services
            ServicesManager.AddService<GraphicsDevice>( GraphicsDevice );
            ServicesManager.AddService<ContentManager>( Content );
            
            #endregion

            #region Initialisation des paramètres graphiques.
            // TODO: Géré la config externe
            displayModes = ServicesManager.GetService<GraphicsDevice>().Adapter.SupportedDisplayModes;
            Window.AllowUserResizing = false;
            Window.Title = "Phobos v0.0";
            //set windows to 1280x768
            deviceManager.PreferredBackBufferWidth = 1280;
            deviceManager.PreferredBackBufferHeight = 640;

            //Limitation des refresh a 60 par seconde
            deviceManager.SynchronizeWithVerticalRetrace = false; //uncomment to got real FPS
            //IsFixedTimeStep = true;
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 13);

            deviceManager.ApplyChanges();

            #endregion

            #region Initialisation du clavier
            string currentLayout = (new WindowsKeyboardExtractor()).getCurrentSystemLayout();
            /*if (PhobosConfigurationManager.Source.Configs["Keyboard"] == null || PhobosConfigurationManager.Source.Configs["Keyboard"].Get("lastKeyboardLayout") == null)
            {
                
                PhobosConfigurationManager.set("Keyboard", "lastKeyboardLayout", currentLayout);
            }
            else
            {
                Console.WriteLine("bam");
                PhobosConfigurationManager.set("Keyboard", "lastKeyboardLayout", currentLayout);
            }*/
            #endregion

            ContentHelper.Initialize();
            MouseHandler.Initialize();
            MouseHandler.HorizontalyShaked += delegate( MouseEvent e ) {
                //Console.WriteLine( "Dont shake it too much or it will hurt !" );
            };
            #region GameStateManager
            GameStateManager.Initialize();
            #endregion


            //SetWindowedFullScreen();
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            if(spriteBatch == null) spriteBatch = new SpriteBatch(GraphicsDevice);
            
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
            GameStateManager.Update( gameTime );
            MouseHandler.UpdateCursor( gameTime );
            base.Update( gameTime );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GameStateManager.Draw( gameTime );
            
            base.Draw( gameTime );
            MouseHandler.DrawCursor();
        }

        /// <summary>
        /// Cette méthode permet de mettre le jeu en pleine écran tout en conservant l'état de fenêtre (alt tab plus rapides :P);
        /// </summary>
        protected void SetWindowedFullScreen()
        {
            //On met a jour la résolution d'écran sur la résolution du bureau;
            this.deviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.deviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            this.deviceManager.PreferredBackBufferFormat = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Format;
            this.deviceManager.ApplyChanges();

            //on masque récupère la fenêtre.
            Form MyGameForm = (Form)Form.FromHandle(this.Window.Handle);
            MyGameForm.FormBorderStyle = FormBorderStyle.None; //Retrait de la bordure
            MyGameForm.Location = new System.Drawing.Point(0, 0); // on place la fenêtre correctement à l'origine de l'écran.
        }

    }
    
}
