﻿using System;
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
using Phobos.Engine.Content;
using Phobos.Engine.Inputs.MouseInput;

namespace Phobos.Engine
{

    class GameEngine : Microsoft.Xna.Framework.Game
    {

        #region Propriété
        GraphicsDeviceManager manager;
        public static SpriteBatch spriteBatch;
        DisplayModeCollection displayModes;
        public static Stack<string> contentToLoad;
        private static GameEngine singleton;
        public static bool loading;

        #endregion

        #region Constructors / Instanciateur
        private GameEngine(){
            this.manager = new GraphicsDeviceManager(this);
            PhobosConfigurationManager.Init("Phobos.ini");
            PhobosConfigurationManager.SetAutoSave(true);
            ContentHelper.Initialize();
            Content.RootDirectory = "Content";
            contentToLoad = new Stack<string>();
            loading = false;
            //TODO: déterminer si un dictionnaire est bien nécessaire pour éviter les chargement multiples d'une meme ressource.
        }

        public static GameEngine Instance {
            get {
                if (singleton == null) {
                    singleton = new GameEngine();
                }
                return singleton;
            }
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
            
            base.Initialize();
            
            #region Initialisation des paramètres graphiques.
            // TODO: Géré la config externe
            this.displayModes = GraphicsDevice.Adapter.SupportedDisplayModes;
            this.Window.AllowUserResizing = false;
            this.Window.Title = "Phobos";
            #endregion

            #region Initialisation du clavier
            string currentLayout = (new WindowsKeyboardExtractor()).getCurrentSystemLayout();
            if (PhobosConfigurationManager.Source.Configs["Keyboard"] == null || PhobosConfigurationManager.Source.Configs["Keyboard"].Get("lastKeyboardLayout") == null)
            {
                
                PhobosConfigurationManager.set("Keyboard", "lastKeyboardLayout", currentLayout);
            }
            else
            {
                Console.WriteLine("bam");
                PhobosConfigurationManager.set("Keyboard", "lastKeyboardLayout", currentLayout);
            }
            #endregion
            
            
            
            // TODO: Add your initialization logic here
            
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            loading = true;
            // Create a new SpriteBatch, which can be used to draw textures.
            if(spriteBatch == null) spriteBatch = new SpriteBatch(GraphicsDevice);

            // use this.Content to load your game content here
            #region Elements d'UI
            MouseHandler.Initialize();
            PSButton button1 = new PSButton( null, 5, 5, "ClickMe !" );
            this.Components.Add( button1 );
            PSButton button2 = new PSButton( null, 5, 40, "HideMe!" );

            button2.Visible = false;
            button1.Action += delegate( object sender, ActionEvent e ) {
                button2.Visible = true;
                Console.WriteLine( "Bravo, joli clic" );
            };
            button1.MouseoverChange += delegate( object sender, BooleanChangeEvent e ) {
                if( e.newValue ) {
                    Console.WriteLine( "La souris rentre..." );
                    button1.ButtonText = "Dessus";
                }
                
            };
            button1.MouseoverChange += delegate( object sender, BooleanChangeEvent e ) {
                if( !e.newValue ) {
                    Console.WriteLine( "...puis ressort." );
                    button1.ButtonText = "Plus dessus";
                }
            };
            button2.Action += delegate( object sender, ActionEvent e ) {
                button2.Visible = false;
                Console.WriteLine( "Ni vus,ni connus" );
            };
            PSButton button3 = new PSButton( null, 5, 75, "Disabled" );
            button3.Activated = false;

            PSCheckBox checkbox1 = new PSCheckBox( null, 140, 5 );
            PSCheckBox checkbox2 = new PSCheckBox( null, 140, 25 );
            checkbox2.Activated = false;
            APRadioGroup radioGroup = new APRadioGroup( null, 0, 0, 0, 0 );
            PSRadioButton radioButton1 = new PSRadioButton( radioGroup, 140, 45 );
            PSRadioButton radioButton2 = new PSRadioButton( radioGroup, 140, 65 );
            PSRadioButton radioButton3 = new PSRadioButton( radioGroup, 140, 85 );
            PSRadioButton radioButton4 = new PSRadioButton( radioGroup, 140, 105 );
            PSRadioButton radioButton5 = new PSRadioButton( radioGroup, 140, 125 );
            radioButton3.Activated = false;
            
            radioGroup.AddRadioButton( radioButton1 );
            radioGroup.AddRadioButton( radioButton2 );
            radioGroup.AddRadioButton( radioButton3 );
            radioGroup.AddRadioButton( radioButton4 );
            radioGroup.AddRadioButton( radioButton5 );

            this.Components.Add( checkbox1 );
            this.Components.Add( checkbox2 );
            this.Components.Add( radioGroup );
            this.Components.Add( button2 );
            this.Components.Add( button3 );

            /*psDialog dialog1 = new psDialog( null, new Rectangle( 16, 16, 400, 300 ) );
            this.Components.Add( dialog1 );*/
            #endregion

            loading = false;
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
            GraphicsDevice.Clear(Color.LightSkyBlue);
            spriteBatch.Begin();

            #region Affichage de l'UI
            //
            #endregion Affichage de l'UI

            
            // Add your drawing code here
            base.Draw(gameTime); // Garder en fin de procédure ?
            MouseHandler.DrawCursor();
            spriteBatch.End();
        }

        /// <summary>
        /// Cette méthode permet de mettre le jeu en pleine écran tout en conservant l'état de fenêtre (alt tab plus rapides :P);
        /// </summary>
        protected void SetWindowedFullScreen()
        {
            //On met a jour la résolution d'écran sur la résolution du bureau;
            this.manager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.manager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            this.manager.PreferredBackBufferFormat = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Format;
            this.manager.ApplyChanges();

            //on masque récupère la fenêtre.
            Form MyGameForm = (Form)Form.FromHandle(this.Window.Handle);
            MyGameForm.FormBorderStyle = FormBorderStyle.None; //Retrait de la bordure
            MyGameForm.Location = new System.Drawing.Point(0, 0); // on place la fenêtre correctement à l'origine de l'écran.
        }
    }
    
}
