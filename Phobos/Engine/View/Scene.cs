using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Models.World;
using Phobos.Engine.Models.Entities;
using Microsoft.Xna.Framework.Input;

public delegate void CalculEntitiesHandler() ;
namespace Phobos.Engine.View
{
    class Scene : DrawableGameComponent
    {
        public CalculEntitiesHandler calculPositionsEntitiesHandler;
        static Scene scene = null;
        Camera camera = new Camera(-710,-60);
        Orientation orientation = Orientation.SE;
        SpriteBatch spriteBatch;
        MouseState prevMouseState;
        Vector2 mouveMovement;
        DrawableEntity centerEntity;

        private List<Chunk> chunks = new List<Chunk>();

        public Orientation Orientation {
            get { return orientation ; }
            set { orientation = value ; }
        }

        public Camera Camera { 
            get { return camera; } 
            set { camera = value; } 
        }

        static public Scene getInstance()
        {
            if (scene == null)
            {
                scene = new Scene();
            }
            return scene;
        }

        private Scene()
            : base(GameEngine.Instance)
        {
        }

        public DrawableEntity CenterEntity
        {
            get { return centerEntity; }
            set { centerEntity = value; }
        }


        public override void Initialize()
        {
            base.Initialize();

            spriteBatch = new SpriteBatch(GameEngine.Instance.GraphicsDevice);

            #region Test
            /* TEST */
            Chunk testChunk = new Chunk(0, 0);
            Texture2D text = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            Texture2D text2 = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\test_rpg");
            int j = 0;
            Random rand = new Random();
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {

                    Core core = new Core(new Vector3(i, j, (float)(-2 * i + j) / 10), 32, 16,
                               new Vector2(16, 8), text, new Rectangle(96, 32, 32, 16),
                               new Color(new Vector4(
                                   0.5f, 0.5f, 0.5f, 1.0f
                                   )
                               )
                    );

                    testChunk.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;

                    if ((i == Chunk.Chunk_Size - 1) && (j == Chunk.Chunk_Size - 1))
                    {

                        DrawableEntity testFontain = new DrawableEntity(
                            new Vector3(i, j, (float)(-2 * i + j) / 10), 27, 34,
                            new Vector2(13, 31), text2, new Rectangle(344, 714, 27, 34),
                            Color.White
                        );
                        core.addEntity(testFontain);
                        calculPositionsEntitiesHandler += testFontain.calculateScreenRect;

                        DrawableEntity testContainable = new DrawableEntity(
                            new Vector3(i, j, (float)(-2 * i + j) / 10 + 2.2f), 32, 32,
                            new Vector2(16, 24), text2, new Rectangle(205, 136, 32, 32),
                            Color.White
                        );
                        core.addEntity(testContainable);
                        calculPositionsEntitiesHandler += testContainable.calculateScreenRect;
                    }
                    i++;
                }
                j++;
            }
            chunks.Add(testChunk);
            #endregion

            calculPositionsEntitiesHandler() ;
            CalculCenterEntity();
        }

       
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointWrap,DepthStencilState.Default,RasterizerState.CullNone);
            foreach (Chunk chunk in chunks)
            {
                chunk.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                mouveMovement = Vector2.Zero;

                float deltaX = prevMouseState.X - Mouse.GetState().X;
                if (deltaX != 0)
                {
                    mouveMovement.X = deltaX/Camera.Coefficient;
                }

                float deltaY = prevMouseState.Y - Mouse.GetState().Y;
                if (deltaY != 0)
                {
                    mouveMovement.Y = deltaY/Camera.Coefficient;
                }
                Camera.move(mouveMovement);
            }

            if (Mouse.GetState().ScrollWheelValue != prevMouseState.ScrollWheelValue)
            {
                //old cam
                int old_camera_width = this.Camera.Width;
                int old_camera_height = this.Camera.Height;
                
                //apply coeff
                this.Camera.Coefficient += (int)((Mouse.GetState().ScrollWheelValue - prevMouseState.ScrollWheelValue)/120) ;

                //new cam
                int new_camera_width = this.Camera.Width;
                int new_camera_height = this.Camera.Height;
                Camera.move(
                    new Vector2(0.5f * (old_camera_width - new_camera_width),
                                0.5f * (old_camera_height - new_camera_height))
                    );
            }

            prevMouseState = Mouse.GetState();

            foreach (Chunk chunk in chunks)
            {
                chunk.Update(gameTime);
            }
        }

        public void CalculCenterEntity() {
            foreach (Chunk chunk in chunks)
            {
                chunk.CalculCenterEntity();
            }
        }

    }
}