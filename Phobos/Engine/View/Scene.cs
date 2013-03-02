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
        Camera camera = new Camera(-656,-251);
        Orientation orientation = Orientation.SE;
        SpriteBatch spriteBatch;
        MouseState prevMouseState;
        Vector2 mouveMovement;
        DrawableEntity centerEntity;

        public Orientation Orientation {
            get { return orientation ; }
            set { orientation = value ; }
        }

        public Camera Camera { 
            get { return camera; } 
            set { camera = value; } 
        }

        public SortedList<int, SortedList<int, Chunk>> Chunks
        {
            set;
            get;
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
            Chunks = new SortedList<int, SortedList<int, Chunk>>();
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

                    Core core = new Core(new Vector3(i, j, (float)(i/2f + j/3f)), 32, 16,
                               new Vector2(16, 8), text,
                               new Color(new Vector4(
                                   0.8f, 0.8f, 0.8f, 1.0f
                                   )
                               )
                    );
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 32, 32, 16),SpriteEffects.None);
                    core[Orientation.BR] = new Sprite(new Rectangle(96, 32, 32, 16), SpriteEffects.FlipHorizontally);
                    core[Orientation.TR] = new Sprite(new Rectangle(96, 32, 32, 16), SpriteEffects.None);
                    core[Orientation.TL] = new Sprite(new Rectangle(96, 32, 32, 16), SpriteEffects.FlipHorizontally);

                    testChunk.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    
                    if ((i == Chunk.Chunk_Size - 1) && (j == Chunk.Chunk_Size - 1)) {
                        AnimatedEntity animated_entity = new AnimatedEntity(
                            new Vector3(i, j, (float)(i / 2f + j / 3f)),
                            32, 32, new Vector2(16, 27),
                            text, Color.White, Orientation.S);

                        animated_entity[Orientation.BR] = new Animation(text,SpriteEffects.FlipHorizontally);
                        animated_entity[Orientation.BR].addFrame(new Rectangle(32, 0, 32, 32), 500);
                        animated_entity[Orientation.BR].addFrame(new Rectangle(96, 0, 32, 32), 500);
                        animated_entity[Orientation.BR].addFrame(new Rectangle(32, 0, 32, 32), 500);
                        animated_entity[Orientation.BR].addFrame(new Rectangle(64, 0, 32, 32), 500);

                        animated_entity[Orientation.BL] = new Animation(text);
                        animated_entity[Orientation.BL].addFrame(new Rectangle(32, 0, 32, 32), 500);
                        animated_entity[Orientation.BL].addFrame(new Rectangle(96, 0, 32, 32), 500);
                        animated_entity[Orientation.BL].addFrame(new Rectangle(32, 0, 32, 32), 500);
                        animated_entity[Orientation.BL].addFrame(new Rectangle(64, 0, 32, 32), 500)
                            ;
                        animated_entity[Orientation.TR] = new Animation(text);
                        animated_entity[Orientation.TR].addFrame(new Rectangle(128, 0, 32, 32), 500);
                        animated_entity[Orientation.TR].addFrame(new Rectangle(192, 0, 32, 32), 500);
                        animated_entity[Orientation.TR].addFrame(new Rectangle(128, 0, 32, 32), 500);
                        animated_entity[Orientation.TR].addFrame(new Rectangle(160, 0, 32, 32), 500);

                        animated_entity[Orientation.TL] = new Animation(text, SpriteEffects.FlipHorizontally);
                        animated_entity[Orientation.TL].addFrame(new Rectangle(128, 0, 32, 32), 500);
                        animated_entity[Orientation.TL].addFrame(new Rectangle(192, 0, 32, 32), 500);
                        animated_entity[Orientation.TL].addFrame(new Rectangle(128, 0, 32, 32), 500);
                        animated_entity[Orientation.TL].addFrame(new Rectangle(160, 0, 32, 32), 500);

                        core.addEntity(animated_entity);
                        calculPositionsEntitiesHandler += animated_entity.calculateScreenRect;
                    }

                    if ((i == 0) && (j == 0))
                    {

                        DrawableEntity testFontain = new DrawableEntity(
                            new Vector3(i, j, (float)(i / 2f + j / 3f)), 27, 34,
                            new Vector2(13, 31), text2,
                            Color.White, Orientation.S
                        );
                        testFontain[Orientation.BL] = new Sprite(new Rectangle(344, 714, 27, 34), SpriteEffects.None);
                    
                        core.addEntity(testFontain);
                        calculPositionsEntitiesHandler += testFontain.calculateScreenRect;

                        DrawableEntity testContainable = new DrawableEntity(
                            new Vector3(i, j, (float)(i / 2f + j / 3f + 2.2f)), 32, 32,
                            new Vector2(16, 24), text2, 
                            Color.White, Orientation.S
                        );
                        testContainable[Orientation.BL] = new Sprite(new Rectangle(205, 136, 32, 32),SpriteEffects.None);
                        core.addEntity(testContainable);
                        calculPositionsEntitiesHandler += testContainable.calculateScreenRect;
                    }
                    i++;
                }
                j++;
            }
            Chunks[0] = new SortedList<int,Chunk>() ;
            Chunks[0][0] = testChunk ;

            //eau 0,1
            j = 0;
            Chunk testChunk2 = new Chunk(0, 1);
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {
                    Core core = new Core(new Vector3(i, j + Chunk.Chunk_Size, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(new Vector4(
                                    0.8f, 0.8f, 0.8f, 1.0f
                                    )
                                )
                    );
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk2.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[0][1] = testChunk2;


            //eau 1,0
            j = 0;
            Chunk testChunk3 = new Chunk(1, 0);
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {
                    Core core = new Core(new Vector3(i + Chunk.Chunk_Size, j, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(new Vector4(
                                    0.8f, 0.8f, 0.8f, 1.0f
                                    )
                                )
                    );
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk3.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[1] = new SortedList<int, Chunk>();
            Chunks[1][0] = testChunk3;

            //calcul position and center
            calculPositionsEntitiesHandler() ;
            CalculCenterEntity();

            //eau 0,-1
            j = 0;
            Chunk testChunk4 = new Chunk(0, -1);
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {
                    Core core = new Core(new Vector3(i, j - Chunk.Chunk_Size, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(new Vector4(
                                    0.8f, 0.8f, 0.8f, 1.0f
                                    )
                                )
                    );
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk4.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[0][-1] = testChunk4;

            //eau -1, 0
            j = 0;
            Chunk testChunk5 = new Chunk(-1, 0);
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {
                    Core core = new Core(new Vector3(i - Chunk.Chunk_Size, j, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(new Vector4(
                                    0.8f, 0.8f, 0.8f, 1.0f
                                    )
                                )
                    );
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk5.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[-1] = new SortedList<int, Chunk>();
           Chunks[-1][0] = testChunk5;


            //eau -1, -1
            j = 0;
            Chunk testChunk6 = new Chunk(-1, -1);
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {
                    Core core = new Core(new Vector3(i - Chunk.Chunk_Size, j - Chunk.Chunk_Size, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(new Vector4(
                                    0.8f, 0.8f, 0.8f, 1.0f
                                    )
                                )
                    );
                    if (((i % 4) == 0) && ((j % 5) == 0))
                    {

                        DrawableEntity water = new DrawableEntity(
                            new Vector3(i - Chunk.Chunk_Size, j - Chunk.Chunk_Size, 0), 34, 21,
                            new Vector2(17, 20), text2,
                            Color.White, Orientation.S
                        );
                        water[Orientation.BL] = new Sprite(new Rectangle(307, 726, 34, 21), SpriteEffects.None);
                        water[Orientation.BR] = new Sprite(new Rectangle(307, 726, 34, 21), SpriteEffects.FlipHorizontally);
                        water[Orientation.TL] = new Sprite(new Rectangle(307, 726, 34, 21), SpriteEffects.None);
                        water[Orientation.TR] = new Sprite(new Rectangle(307, 726, 34, 21), SpriteEffects.FlipHorizontally);
                        core.addEntity(water);
                        calculPositionsEntitiesHandler += water.calculateScreenRect;
                    }
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk6.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
           Chunks[-1][-1] = testChunk6;


            //eau 1,-1
            j = 0;
            Chunk testChunk7 = new Chunk(1, -1);
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {
                    Core core = new Core(new Vector3(i + Chunk.Chunk_Size, j - Chunk.Chunk_Size, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(new Vector4(
                                    0.8f, 0.8f, 0.8f, 1.0f
                                    )
                                )
                    );
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 64, 32, 16),SpriteEffects.None);
                    testChunk7.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
           Chunks[1][-1] = testChunk7;


            //eau -1, 1
            j = 0;
            Chunk testChunk8 = new Chunk(-1, 1);
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {
                    Core core = new Core(new Vector3(i - Chunk.Chunk_Size, j + Chunk.Chunk_Size, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(new Vector4(
                                    0.8f, 0.8f, 0.8f, 1.0f
                                    )
                                )
                    );
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk8.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
          Chunks[-1][1] = testChunk8;


            //eau 1, 1
             j = 0;
            Chunk testChunk9 = new Chunk(1, 0);
            while (j < Chunk.Chunk_Size)
            {
                int i = 0;
                while (i < Chunk.Chunk_Size)
                {
                    Core core = new Core(new Vector3(i + Chunk.Chunk_Size, j+ Chunk.Chunk_Size, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(new Vector4(
                                    0.8f, 0.8f, 0.8f, 1.0f
                                    )
                                )
                    );
                    core[Orientation.BL] = new Sprite(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk9.addCore(i, j, core);
                    calculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[1][1] = testChunk9;

            Chunks[0][0].calculCliffs();
            #endregion

            //calcul position and center
            calculPositionsEntitiesHandler() ;
            CalculCenterEntity();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointWrap,DepthStencilState.Default,RasterizerState.CullNone);
            /** Browse layer throw camera depth **/
            switch (Scene.getInstance().Orientation)
            {
                //Calcul pour SE
                case Orientation.SE:
                    foreach (var chunks_row in Chunks.Values)
                    {
                        foreach (var chunk in chunks_row.Values)
                        {
                            chunk.Draw(spriteBatch, gameTime);
                        }
                    }
                    break;
                //Calcul pour SO
                case Orientation.SO:
                    foreach (var chunks_row in Chunks.Values.Reverse())
                    {
                        foreach (var chunk in chunks_row.Values)
                        {
                            chunk.Draw(spriteBatch, gameTime);
                        }
                    }
                    break;
                //Calcul pour NE
                case Orientation.NE:
                    foreach (var chunks_row in Chunks.Values)
                    {
                        foreach (var chunk in chunks_row.Values.Reverse())
                        {
                            chunk.Draw(spriteBatch, gameTime);
                        }
                    }
                    break;
                //Calcul pour NO
                case Orientation.NO:
                    foreach (var chunks_row in Chunks.Values.Reverse())
                    {
                        foreach (var chunk in chunks_row.Values.Reverse())
                        {
                            chunk.Draw(spriteBatch, gameTime);
                        }
                    }
                    break;
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

            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                Scene.getInstance().Camera.turnCamera(Orientation.SE);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                Scene.getInstance().Camera.turnCamera(Orientation.SO);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                Scene.getInstance().Camera.turnCamera(Orientation.NO);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F4))
            {
                Scene.getInstance().Camera.turnCamera(Orientation.NE);
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
                                0.5f * (old_camera_height - new_camera_height)
                    )
                );
            }

            prevMouseState = Mouse.GetState();


            foreach (var chunks_row in Chunks.Values)
            {
                foreach (var chunk in chunks_row.Values)
                {
                    chunk.Update(gameTime);
                }
            }
        }
        
        public void CalculCenterEntity() {

            /** Browse layer throw camera depth **/
            switch (Scene.getInstance().Orientation)
            {
                //Calcul pour SE
                case Orientation.SE:
                    foreach (var chunks_row in Chunks.Values)
                    {
                        foreach (var chunk in chunks_row.Values)
                        {
                            chunk.CalculCenterEntity();
                        }
                    }
                    break;
                //Calcul pour SO
                case Orientation.SO:
                    foreach (var chunks_row in Chunks.Values.Reverse())
                    {
                        foreach (var chunk in chunks_row.Values)
                        {
                            chunk.CalculCenterEntity();
                        }
                    }
                    break;
                //Calcul pour NE
                case Orientation.NE:
                    foreach (var chunks_row in Chunks.Values)
                    {
                        foreach (var chunk in chunks_row.Values.Reverse())
                        {
                            chunk.CalculCenterEntity();
                        }
                    }
                    break;
                //Calcul pour NO
                case Orientation.NO:
                    foreach (var chunks_row in Chunks.Values.Reverse())
                    {
                        foreach (var chunk in chunks_row.Values.Reverse())
                        {
                            chunk.CalculCenterEntity();
                        }
                    }
                    break;
            }
            
        }

        public bool IsLoadedCore(int x, int y) {

            Chunk chunk;
            int chunk_x = x / Chunk.Chunk_Size;
            int chunk_y = y / Chunk.Chunk_Size; 
            
            //if negatif, need to substract 1 to get the good one
            if (x < 0) chunk_x--;
            if (y < 0) chunk_y--;

            SortedList<int, Chunk> chunks_x;
            if (!Chunks.TryGetValue(chunk_x, out chunks_x))
            {
                return false;
            }
            if (!chunks_x.TryGetValue(chunk_y, out chunk))
            {
                return false;
            }

            //if chunk exists core exisist too
            //int core_x = x % Chunk.Chunk_Size;
            //int core_y = y % Chunk.Chunk_Size;

            return true ;
        }

        public Core getCore(int x, int y) {

            int chunk_x = x / Chunk.Chunk_Size;
            int chunk_y = y / Chunk.Chunk_Size;

            //if negatif, need to substract 1 to get the good one
            if (x < 0) chunk_x--;
            if (y < 0) chunk_y--;

            Chunk chunk = Chunks[chunk_x][chunk_y];
            
            int core_x = x % Chunk.Chunk_Size;
            int core_y = y % Chunk.Chunk_Size;

            //if negatif, coords are inversed
            if (core_x < 0) core_x = Chunk.Chunk_Size + core_x;
            if (core_y < 0) core_y = Chunk.Chunk_Size + core_y;

            return chunk.getCore(core_x, core_y);
        }

    }
}