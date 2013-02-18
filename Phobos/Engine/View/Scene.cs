using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Models.World;
using Phobos.Engine.Models.Entities;
using Microsoft.Xna.Framework.Input;

public delegate void CalculRenderEntitiesHandler() ;
namespace Phobos.Engine.View
{
    class Scene : DrawableGameComponent
    {
        public CalculRenderEntitiesHandler calculRenderEntitiesHandler;
        static private Scene scene = null;
        private Camera cameraNO;
        private Camera cameraNE;
        private Camera cameraSO;
        private Camera cameraSE;    
        Orientation orientation;
        SpriteBatch spriteBatch;
        MouseState prevMouseState;
        Vector2 mouveMovement;

        private List<Chunk> chunks = new List<Chunk>();

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
       
        public Camera currentCamera()
        {
            switch (orientation)
            {
                case Orientation.SE:
                    return cameraSE;

                case Orientation.SO:
                    return cameraSO;
                        
                case Orientation.NE:
                    return cameraNE;

                case Orientation.NO:
                    return cameraNO;
            }
            throw new Exception("Cannot find camera");
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
                    mouveMovement.X = deltaX/currentCamera().Coefficient;
                }

                float deltaY = prevMouseState.Y - Mouse.GetState().Y;
                if (deltaY != 0)
                {
                    mouveMovement.Y = deltaY/currentCamera().Coefficient;
                }
            }
            currentCamera().Position += mouveMovement;

            if (Mouse.GetState().ScrollWheelValue != prevMouseState.ScrollWheelValue)
            {
                //old cam
                float old_coeff = this.currentCamera().Coefficient;
                int old_camera_width = this.currentCamera().Width;
                int old_camera_height = this.currentCamera().Height;
                
                //apply coeff
                this.currentCamera().Coefficient += (int)((Mouse.GetState().ScrollWheelValue - prevMouseState.ScrollWheelValue)/120) ;

                //new cam
                float new_coeff = this.currentCamera().Coefficient;
                int new_camera_width = this.currentCamera().Width;
                int new_camera_height = this.currentCamera().Height;

                currentCamera().Position += new Vector2(0.5f * (old_camera_width - new_camera_width),
                                                        0.5f * (old_camera_height - new_camera_height));
            }

            prevMouseState = Mouse.GetState();
        }

        public override void Initialize()
        {
            base.Initialize();
            cameraNO = new Camera();
            cameraSE = new Camera();
            cameraNE = new Camera();
            cameraSO = new Camera();

            orientation = Orientation.SE;

            spriteBatch = new SpriteBatch(GameEngine.Instance.GraphicsDevice);

            #region Test
            /* TEST */
            Chunk testChunk = new Chunk(0, 0);
            Texture2D text = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            Texture2D text2 = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\test_rpg");
            int j = -20;
            while (j < 20)
            {
                int i = 20;
                while (i < 60)
                {
                    Core core = new Core(new Vector3(i, j, 0), 32, 32, new Vector2(16, 8), text, new Rectangle(64, 32, 32, 32));
                    testChunk.addCore(core);
                    calculRenderEntitiesHandler += core.calculateScreenRect;

                    if (i % 4 == 0 && j % 4 == 0)
                    {

                        DrawableEntity testFontain = new DrawableEntity(new Vector3(i, j, 0), 27, 34, new Vector2(13, 31), text2, new Rectangle(344, 714, 27, 34));
                        core.addEntity(testFontain);
                        calculRenderEntitiesHandler += testFontain.calculateScreenRect;

                        DrawableEntity testContainable = new DrawableEntity(new Vector3(i, j, 2.2f), 32, 32, new Vector2(16, 24), text2, new Rectangle(205, 136, 32, 32));
                        core.addEntity(testContainable);
                        calculRenderEntitiesHandler += testContainable.calculateScreenRect;

                    }
                    i++;
                }
                j++;
            }

            Chunk testChunk2 = new Chunk(0, 1);
            j = 20;
            while (j < 60)
            {
                int i = 20;
                while (i < 60)
                {
                    Core core = new Core(new Vector3(i, j, 0), 32, 32, new Vector2(16, 8), text, new Rectangle(128, 32, 32, 32));
                    testChunk2.addCore(core);
                    calculRenderEntitiesHandler += core.calculateScreenRect;
                    if (i == 40 && j == 40)
                    {

                        DrawableEntity testContainable = new DrawableEntity(new Vector3(i, j, 0), 32, 32, new Vector2(16, 28), text, new Rectangle(32, 96, 32, 32));
                        core.addEntity(testContainable);
                        calculRenderEntitiesHandler += testContainable.calculateScreenRect;
                    }
                    i++;
                }
                j++;
            }

            Chunk testChunk3 = new Chunk(0, 1);
            j = -20;
            while (j < 20)
            {
                int i = -20;
                while (i < 20)
                {
                    Core core = new Core(new Vector3(i, j, 0), 32, 32, new Vector2(16, 8), text, new Rectangle(160, 32, 32, 32));

                    if (i == 0 && j == 0)
                    {

                        DrawableEntity testContainable = new DrawableEntity(new Vector3(i, j, 0), 32, 32, new Vector2(16, 28), text, new Rectangle(32, 0, 32, 32));
                        core.addEntity(testContainable);
                        calculRenderEntitiesHandler += testContainable.calculateScreenRect;
                    }

                    testChunk3.addCore(core);
                    calculRenderEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunk testChunk4 = new Chunk(0, 1);
            j = 20;
            while (j < 60)
            {
                int i = -20;
                while (i < 20)
                {
                    Core core = new Core(new Vector3(i, j, 0), 32, 32, new Vector2(16, 8), text, new Rectangle(0, 0, 32, 32));

                    if (i == 0 && j == 40)
                    {

                        DrawableEntity testContainable = new DrawableEntity(new Vector3(i, j, 0), 32, 32, new Vector2(16, 28), text, new Rectangle(32, 0, 32, 32));
                        core.addEntity(testContainable);
                        calculRenderEntitiesHandler += testContainable.calculateScreenRect;
                    }

                    testChunk4.addCore(core);
                    calculRenderEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            chunks.Add(testChunk3);
            chunks.Add(testChunk4);
            chunks.Add(testChunk);
            chunks.Add(testChunk2);
            #endregion
        }

    }
}