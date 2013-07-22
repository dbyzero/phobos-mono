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
using Phobos.Engine.Models.World.Generators;
using Phobos.Engine.Models.Light;
using Phobos.Engine.Controllers.Light;
using Phobos.Engine.View;
using Phobos.Engine;

namespace Phobos.Test
{
    class TestRayCasting : ITest
    {

        private Texture2D text;
        private Texture2D text2;

        public void Initialize(Scene scene)
        {
            //Test Map creation
            scene.Camera.move(new Vector2(-200, -300));

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
                    int chunk_z = 0;
                    if (rand.Next(100) > 97)
                    {
                        chunk_z = 1;
                    }
                    if (rand.Next(100) > 99)
                    {
                        chunk_z = 4;
                    }
                    CoreProxy core = new CoreProxy(new Vector3(chunk_x, chunk_y, chunk_z), 32, 16,
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
                            new Vector3(chunk_x, chunk_y, chunk_z),
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
                            new Vector3(chunk_x, chunk_y, chunk_z), 27, 34,
                            new Vector2(13, 31), text2,
                            new Color(0, 0, 0, 1), Orientation.S
                        );
                        testFontain[Orientation.BL] = new SpriteArea(new Rectangle(344, 714, 27, 34), SpriteEffects.None);

                        core.addEntity(testFontain);
                        scene.CalculPositionsEntitiesHandler += testFontain.calculateScreenRect;

                        DrawableEntity testContainable = new DrawableEntity(
                            new Vector3(chunk_x, chunk_y, chunk_z + (float)2f), 32, 32,
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
                            new Vector3(chunk_x, chunk_y, chunk_z), 32, 32,
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
                            new Vector3(chunk_x, chunk_y, chunk_z), 32, 32,
                            new Vector2(16, (chunk_y % 2 == 0) ? 27 : 30), text,
                            hijack_color, Orientation.S
                        );

                        //head
                        testGuysHead[Orientation.BL] = new SpriteArea(new Rectangle((chunk_y % 2 == 0) ? 128 : 160, 64, 32, 32), SpriteEffects.None);
                        testGuysHead[Orientation.BR] = new SpriteArea(new Rectangle((chunk_y % 2 == 0) ? 128 : 160, 64, 32, 32), SpriteEffects.FlipHorizontally);
                        core.addEntity(testGuysHead);
                        scene.CalculPositionsEntitiesHandler += testGuysHead.calculateScreenRect;

                        chunk_z = 0;
                    }
                    chunk_x++;
                }
                chunk_y++;
            }
            scene.Chunks[0] = new SortedList<int, ChunkProxy>();
            scene.Chunks[0][0] = testChunk; 
            #endregion

            //calcul position and center
            scene.CalculPositionsEntitiesHandler(scene);
            scene.CalculCenterEntity();

            //calcul du cliff
            scene.Chunks[0][0].calculCliffs(scene);
        }

        private void initializeLights(Scene scene) 
        {
            //light
            FixedLight test_light4 = new FixedLight(20, new Vector3(20, 20, 0), Color.Green);
            FixedLight test_light5 = new FixedLight(10, new Vector3(25, 25, 30), Color.Yellow);
            FixedLight test_light6 = new FixedLight(10, new Vector3(15, 15, 30), Color.Blue);
            scene.AddLight(test_light4);
            //scene.AddLight(test_light5);
            //scene.AddLight(test_light6);
        }

        public void Update(Scene scene)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ALight light = scene.GetARadomLight();
                light.Position += new Vector3(-1,-1,0);
                light.RegisterCoreInTheLight(scene);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ALight light = scene.GetARadomLight();
                light.Position += new Vector3(1, 1, 0);
                light.RegisterCoreInTheLight(scene);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                ALight light = scene.GetARadomLight();
                light.Position += new Vector3(1, -1, 0);
                light.RegisterCoreInTheLight(scene);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                ALight light = scene.GetARadomLight();
                light.Position += new Vector3(-1, 1, 0);
                light.RegisterCoreInTheLight(scene);
            }
        }

        public void Draw(Scene scene)
        {
        }
    }
}
