using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Gui.PWidgets.Events;
using Phobos.Engine.Gui.PWidgets.System;
using Phobos.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Models.Entities;
using Phobos.Engine.Gui.PWidgets;
using Phobos.Engine.View;
using System.Diagnostics;
using Phobos.Engine.Models.World;

namespace Phobos.Engine.GameStates.Game {
    class MainState : AGameState{

        PSButton returnButton;
        PSButton exitButton;
        Scene scene ;

        public MainState( GameStateManager manager )
            : base() {
            Status = GameStateStatus.Active;
        }

        public override void Initialize()
        {
            base.Initialize();
            scene = Scene.getInstance();
            scene.Initialize();

            /*#region Benchmark

            int size = 1000;
            Texture2D text = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            Dictionary<int, Dictionary<int, Core>> dictListCore = new Dictionary<int, Dictionary<int, Core>>();
            SortedDictionary<int, SortedDictionary<int, Core>> sDictListCore = new SortedDictionary<int, SortedDictionary<int, Core>>();
            Core[,] arrayListCore = new Core[size, size];
            for (int x = 0; x < size; x++)
            {
                Dictionary<int, Core> dictRow = new Dictionary<int, Core>();
                SortedDictionary<int, Core> sDictRow = new SortedDictionary<int, Core>();
                dictListCore.Add(x, dictRow);
                sDictListCore.Add(x, sDictRow);
                for (int y = 0; y < size; y++)
                {
                    Core core = new Core(new Vector3(x, y, 0), 32, 32, new Vector2(16, 8), text, new Rectangle(64, 32, 32, 32)) ;
                    dictRow.Add(y, core);
                    sDictRow.Add(y, core);
                    arrayListCore[x,y] = core ;
                }
            }

            Random random = new Random();
            var sw = new Stopwatch();
            sw.Start();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    int px = random.Next(999);
                    int py = random.Next(999);  
                    arrayListCore[px,py].calculateScreenRect();
                }
            }
            sw.Stop();
            Console.WriteLine("time for ARRAY lookups: {0} ms", sw.ElapsedMilliseconds);

            sw.Reset();

            sw.Start();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    int px = random.Next(999);
                    int py = random.Next(999);  
                    dictListCore[px][py].calculateScreenRect();
                }
            }
            sw.Stop();
            Console.WriteLine("time for DICTIONNARY index lookups: {0} ms", sw.ElapsedMilliseconds); 

            sw.Reset();

            sw.Start();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    int px = random.Next(999);
                    int py = random.Next(999);  
                    sDictListCore[px][py].calculateScreenRect();
                }
            }
            sw.Stop();
            Console.WriteLine("time for SORTED DICTIONNARY index lookups: {0} ms", sw.ElapsedMilliseconds); 
            #endregion*/
        }

        protected override void LoadContent() {

            returnButton = new PSButton(returnButton, 64, 64, "Retour");
            returnButton.Action += delegate( APButton  sender, ActionEvent e ) {
                ServicesManager.GetService<GameStateManager>().getGameState( GameStateList.MENU ).Status = GameStateStatus.Active;
                Status = GameStateStatus.Inactive;
            };
            exitButton = new PSButton(exitButton, 64, 128, "Exit");
            exitButton.Action += delegate(APButton sender, ActionEvent e)
            {
                GameEngine.Instance.Exit();
            };
            base.LoadContent();
        }

        #region IDrawable

        public override void Draw( GameTime gameTime ) {
            if (Status != GameStateStatus.Active) return;

            scene.Draw(gameTime);

            #region UI
            GameEngine.spriteBatch.Begin();
            returnButton.Draw(gameTime);
            exitButton.Draw(gameTime);
            GameEngine.spriteBatch.End(); 
            #endregion

            base.Draw(gameTime);

        }

        #endregion
        #region IUpdateable

        public override void Update( GameTime gameTime ) {
            if( Status != GameStateStatus.Active ) return;

            base.Update( gameTime );
            returnButton.Update(gameTime);
            exitButton.Update(gameTime);
            scene.Update(gameTime);
        }
        #endregion
    }
}
