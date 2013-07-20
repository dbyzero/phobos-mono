using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.View.Proxies.World;
using Phobos.Engine.View.Proxies.Entities;
using Microsoft.Xna.Framework.Input;
using Phobos.Engine.Models.World;
using Phobos.Models.World.Generators;
using Phobos.Engine.Models.Light;
using Phobos.Engine.Controllers.Light;
using Phobos.Engine.View;
using Phobos.Engine;

namespace Phobos.Test
{
    class TestFillSceneLight : ITest
    {

        public TestFillSceneLight()
        {
        }

        private Texture2D text;
        private Texture2D text2;

        public void Initialize(Scene scene)
        {
            //Test Map creation
            scene.Camera.move(new Vector2(-200, -500));

            BasicGenerator generator = new BasicGenerator();
            Map map = generator.BuildSimpleMap();
            text = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\temp_sprite");
            text2 = GameEngine.Instance.Content.Load<Texture2D>(@"spriteSheets\test_rpg");

            initializeChunks(scene);
            initializeLights(scene);

        }

        private void initializeChunks(Scene scene)
        {

            #region Chunk 0,0
            ChunkProxy testChunk = new ChunkProxy(0, 0);
            int chunk_y = 0;
            Random rand = new Random();
            while (chunk_y < Chunk.CHUNKS_SIZE)
            {
                int chunk_x = 0;
                while (chunk_x < Chunk.CHUNKS_SIZE)
                {

                    CoreProxy core = new CoreProxy(new Vector3(chunk_x, chunk_y, 7), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 32, 32, 16), SpriteEffects.None);
                    core[Orientation.BR] = new SpriteArea(new Rectangle(96, 32, 32, 16), SpriteEffects.FlipHorizontally);
                    core[Orientation.TR] = new SpriteArea(new Rectangle(96, 32, 32, 16), SpriteEffects.None);
                    core[Orientation.TL] = new SpriteArea(new Rectangle(96, 32, 32, 16), SpriteEffects.FlipHorizontally);

                    testChunk[chunk_x, chunk_y] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;

                    //animated sprite
                    if ((chunk_x == Chunk.CHUNKS_SIZE - 1) && (chunk_y == Chunk.CHUNKS_SIZE - 1))
                    {
                        AnimatedEntity animated_entity = new AnimatedEntity(
                            new Vector3(chunk_x, chunk_y, 7),
                            32, 32, new Vector2(16, 27),
                            text, new Color(0, 0, 0, 1), Orientation.S);

                        animated_entity[Orientation.BR] = new Animation(text, SpriteEffects.FlipHorizontally);
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
                        scene.CalculPositionsEntitiesHandler += animated_entity.calculateScreenRect;
                    }

                    //fontain
                    if ((chunk_x == 0) && (chunk_y == 0))
                    {

                        DrawableEntity testFontain = new DrawableEntity(
                            new Vector3(chunk_x, chunk_y, 7), 27, 34,
                            new Vector2(13, 31), text2,
                            new Color(0, 0, 0, 1), Orientation.S
                        );
                        testFontain[Orientation.BL] = new SpriteArea(new Rectangle(344, 714, 27, 34), SpriteEffects.None);

                        core.addEntity(testFontain);
                        scene.CalculPositionsEntitiesHandler += testFontain.calculateScreenRect;

                        DrawableEntity testContainable = new DrawableEntity(
                            new Vector3(chunk_x, chunk_y, (float)9f), 32, 32,
                            new Vector2(16, 24), text2,
                            new Color(0, 0, 0, 1), Orientation.S
                        );
                        testContainable[Orientation.BL] = new SpriteArea(new Rectangle(205, 136, 32, 32), SpriteEffects.None);
                        core.addEntity(testContainable);
                        scene.CalculPositionsEntitiesHandler += testContainable.calculateScreenRect;
                    }

                    //dwarfs
                    else if (((chunk_x % 4) == 0) && ((chunk_y % 5) == 0))
                    {

                        /***
                            * color is hijack, it contain refill color (magenta and cyan) + light and light intensity
                            * offset colors are listed in shader (0-4 : hair, 5-14 : clothes, 15-255 free)
                            * */
                        Color hijack_color = new Color(
                            0,
                            0,
                            0,
                            (int)rand.Next(5, 13)
                        );

                        //body
                        DrawableEntity testGuysBody = new DrawableEntity(
                            new Vector3(chunk_x, chunk_y, 7), 32, 32,
                            new Vector2(16, 27), text,
                            hijack_color, Orientation.S
                        );

                        testGuysBody[Orientation.BL] = new SpriteArea(new Rectangle(128, 32, 32, 32), SpriteEffects.None);
                        testGuysBody[Orientation.BR] = new SpriteArea(new Rectangle(128, 32, 32, 32), SpriteEffects.FlipHorizontally);
                        core.addEntity(testGuysBody);
                        scene.CalculPositionsEntitiesHandler += testGuysBody.calculateScreenRect;

                        hijack_color = new Color(
                            0,
                            0,
                            0,
                            (int)rand.Next(0, 4)
                        );
                        DrawableEntity testGuysHead = new DrawableEntity(
                            new Vector3(chunk_x, chunk_y, 7), 32, 32,
                            new Vector2(16, (chunk_y % 2 == 0) ? 27 : 30), text,
                            hijack_color, Orientation.S
                        );

                        //head
                        testGuysHead[Orientation.BL] = new SpriteArea(new Rectangle((chunk_y % 2 == 0) ? 128 : 160, 64, 32, 32), SpriteEffects.None);
                        testGuysHead[Orientation.BR] = new SpriteArea(new Rectangle((chunk_y % 2 == 0) ? 128 : 160, 64, 32, 32), SpriteEffects.FlipHorizontally);
                        core.addEntity(testGuysHead);
                        scene.CalculPositionsEntitiesHandler += testGuysHead.calculateScreenRect;
                    }
                    chunk_x++;
                }
                chunk_y++;
            }
            scene.Chunks[0] = new SortedList<int, ChunkProxy>();
            scene.Chunks[0][0] = testChunk; 
            #endregion
            #region Chunk 0,1

            int j;
            //eau 0,1
            j = 0;
            ChunkProxy testChunk2 = new ChunkProxy(0, 1);
            while (j < Chunk.CHUNKS_SIZE)
            {
                int i = 0;
                while (i < Chunk.CHUNKS_SIZE)
                {
                    CoreProxy core = new CoreProxy(new Vector3(i, j + Chunk.CHUNKS_SIZE, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk2[i, j] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            scene.Chunks[0][1] = testChunk2; 
            #endregion
            #region Chunk 1,0
            //eau 1,0
            j = 0;
            ChunkProxy testChunk3 = new ChunkProxy(1, 0);
            while (j < Chunk.CHUNKS_SIZE)
            {
                int i = 0;
                while (i < Chunk.CHUNKS_SIZE)
                {
                    CoreProxy core = new CoreProxy(new Vector3(i + Chunk.CHUNKS_SIZE, j, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk3[i, j] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            scene.Chunks[1] = new SortedList<int, ChunkProxy>();
            scene.Chunks[1][0] = testChunk3;

            //calcul position and center
            scene.CalculPositionsEntitiesHandler();
            scene.CalculCenterEntity();
            
            #endregion
            #region Chunk 0,-1
            //eau 0,-1
            j = 0;
            ChunkProxy testChunk4 = new ChunkProxy(0, -1);
            while (j < Chunk.CHUNKS_SIZE)
            {
                int i = 0;
                while (i < Chunk.CHUNKS_SIZE)
                {
                    CoreProxy core = new CoreProxy(new Vector3(i, j - Chunk.CHUNKS_SIZE, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk4[i, j] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            scene.Chunks[0][-1] = testChunk4; 
            #endregion
            #region Chunk -1,0

            //eau -1, 0
            j = 0;
            ChunkProxy testChunk5 = new ChunkProxy(-1, 0);
            while (j < Chunk.CHUNKS_SIZE)
            {
                int i = 0;
                while (i < Chunk.CHUNKS_SIZE)
                {
                    CoreProxy core = new CoreProxy(new Vector3(i - Chunk.CHUNKS_SIZE, j, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk5[i, j] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            scene.Chunks[-1] = new SortedList<int, ChunkProxy>();
            scene.Chunks[-1][0] = testChunk5;

            
            #endregion
            #region Chunk -1,-1
            //eau -1, -1
            j = 0;
            ChunkProxy testChunk6 = new ChunkProxy(-1, -1);
            while (j < Chunk.CHUNKS_SIZE)
            {
                int i = 0;
                while (i < Chunk.CHUNKS_SIZE)
                {
                    CoreProxy core = new CoreProxy(new Vector3(i - Chunk.CHUNKS_SIZE, j - Chunk.CHUNKS_SIZE, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    if (((i % 2) == 0) && ((j % 2) == 0))
                    {

                        DrawableEntity water = new DrawableEntity(
                            new Vector3(i - Chunk.CHUNKS_SIZE, j - Chunk.CHUNKS_SIZE, 0), 34, 21,
                            new Vector2(17, 20), text2,
                            new Color(0, 0, 0, 1), Orientation.S
                        );
                        water[Orientation.BL] = new SpriteArea(new Rectangle(307, 726, 34, 21), SpriteEffects.None);
                        water[Orientation.BR] = new SpriteArea(new Rectangle(307, 726, 34, 21), SpriteEffects.FlipHorizontally);
                        water[Orientation.TL] = new SpriteArea(new Rectangle(307, 726, 34, 21), SpriteEffects.None);
                        water[Orientation.TR] = new SpriteArea(new Rectangle(307, 726, 34, 21), SpriteEffects.FlipHorizontally);
                        core.addEntity(water);
                        scene.CalculPositionsEntitiesHandler += water.calculateScreenRect;
                    }
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk6[i, j] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            scene.Chunks[-1][-1] = testChunk6;
            
            #endregion
            #region Chunk 1,-1

            //eau 1,-1
            j = 0;
            ChunkProxy testChunk7 = new ChunkProxy(1, -1);
            while (j < Chunk.CHUNKS_SIZE)
            {
                int i = 0;
                while (i < Chunk.CHUNKS_SIZE)
                {
                    CoreProxy core = new CoreProxy(new Vector3(i + Chunk.CHUNKS_SIZE, j - Chunk.CHUNKS_SIZE, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk7[i, j] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            scene.Chunks[1][-1] = testChunk7;

            
            #endregion
            #region Chunk -1,1
            //eau -1, 1
            j = 0;
            ChunkProxy testChunk8 = new ChunkProxy(-1, 1);
            while (j < Chunk.CHUNKS_SIZE)
            {
                int i = 0;
                while (i < Chunk.CHUNKS_SIZE)
                {
                    CoreProxy core = new CoreProxy(new Vector3(i - Chunk.CHUNKS_SIZE, j + Chunk.CHUNKS_SIZE, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk8[i, j] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            scene.Chunks[-1][1] = testChunk8;

            
            #endregion
            #region Chunk 1,1
            //eau 1, 1
            j = 0;
            ChunkProxy testChunk9 = new ChunkProxy(1, 0);
            while (j < Chunk.CHUNKS_SIZE)
            {
                int i = 0;
                while (i < Chunk.CHUNKS_SIZE)
                {
                    CoreProxy core = new CoreProxy(new Vector3(i + Chunk.CHUNKS_SIZE, j + Chunk.CHUNKS_SIZE, 0), 32, 16,
                                new Vector2(16, 8), text,
                                new Color(0, 0, 0, 1)
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk9[i, j] = core;
                    scene.CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            scene.Chunks[1][1] = testChunk9;
            
            #endregion

            scene.Chunks[0][0].calculCliffs();
        }

        private void initializeLights(Scene scene) 
        {
            //light
            FixedLight test_light4 = new FixedLight(14, new Vector3(20, 20, 30), Color.Red);
            scene.LightController.AddLight(test_light4);
            FixedLight test_light2 = new FixedLight(14, new Vector3(20, 10, 30), Color.Blue);
            scene.LightController.AddLight(test_light2);
            FixedLight test_light3 = new FixedLight(14, new Vector3(10, 20, 30), Color.Green);
            scene.LightController.AddLight(test_light3);
        }

        public void Update(Scene scene)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ALight light = scene.LightController.GetALight();
                light.Position += new Vector3(-1,-1,0);
                light.RegisterCoreInTheLight();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ALight light = scene.LightController.GetALight();
                light.Position += new Vector3(1, 1, 0);
                light.RegisterCoreInTheLight();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                ALight light = scene.LightController.GetALight();
                light.Position += new Vector3(1, -1, 0);
                light.RegisterCoreInTheLight();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                ALight light = scene.LightController.GetALight();
                light.Position += new Vector3(-1, 1, 0);
                light.RegisterCoreInTheLight();
            }
        }

        public void Draw(Scene scene)
        {
        }
    }
}
