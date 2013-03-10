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

public delegate void CalculsEntitiesHandler();

namespace Phobos.Engine.View {
    /**
     * <summary> Une scene, utilisation de singleton </summary>
     */
    class Scene : DrawableGameComponent {
        //delegate utiliser pour recalculer toute les positions des entities, appeler en zoom et rotation de camera
        public CalculsEntitiesHandler CalculPositionsEntitiesHandler;

        //la scene static, utilisation du simgleton
        static Scene scene = null;
        static public Scene GetInstance() {
            if( scene == null ) {
                scene = new Scene();
            }
            return scene;
        }

        
 	    private Camera camera = new Camera(-200,-500);
 	    private Orientation orientation = Orientation.SO;
        private SpriteBatch spriteBatch;
        private MouseState prevMouseState;
        private Vector2 mouveMovement;
        private DrawableEntity centerEntity; //utiliser lors de la rotation de la camera

        //FOR TEST PURPOSE
        FixedLight test_light;

        #region mutator et gettor
        public Effect ShaderEffect { set; get; }
        public Orientation Orientation {
            get { return orientation; }
            set { orientation = value; }
        }

        public Camera Camera {
            get { return camera; }
            set { camera = value; }
        }
        public Vector4 AmbiantColor { set; get; } //actual ambiant color
 	    public Vector4 ConvergeColor { set; get; } //color to converge to for ambiant color
 	    public Vector4 SunriseColor { set; get; } //matin
        public Vector4 NoonColor { set; get; } //midi
        public Vector4 EveningColor { set; get; } //soir
 	    public Vector4 NightColor { set; get; } //nuit

        public SortedList<int, SortedList<int, ChunkProxy>> Chunks {
            set;
            get;
        }
        #endregion

        private Scene()
            : base( GameEngine.Instance ) {
            Chunks = new SortedList<int, SortedList<int, ChunkProxy>>();
        }

        public DrawableEntity CenterEntity {
            get { return centerEntity; }
            set { centerEntity = value; }
        }

        public override void Initialize() {
            base.Initialize();

            spriteBatch = new SpriteBatch( GameEngine.Instance.GraphicsDevice );
            Texture2D text = GameEngine.Instance.Content.Load<Texture2D>( @"spriteSheets\temp_sprite" );
            Texture2D text2 = GameEngine.Instance.Content.Load<Texture2D>( @"spriteSheets\test_rpg" );
            ShaderEffect = GameEngine.Instance.Content.Load<Effect>(@"shaders\coloration");
            
            SunriseColor = new Vector4(0.74f, 0.53f, 0.36f, 1.0f);
            NoonColor = new Vector4(1.0f, 1.0f, 0.95f, 1.0f);
            NightColor = new Vector4(0.2557f, 0.29f, 0.42f, 1.0f);
            EveningColor = new Vector4(0.75f, 0.49f, 0.36f, 1.0f);

            ConvergeColor = AmbiantColor = NoonColor;

            #region Test
            //Test Map creation
            BasicGenerator generator = new BasicGenerator();
            Map map = generator.BuildSimpleMap();
            
            //Proxies creation
            //for( int i = 0 ; i < map.Size ; i++ ) {
            //    Chunks[i] = new SortedList<int,ChunkProxy>();
            //    for( int j = 0 ; j < map.Size ; j++ ) {
            //        Chunks[i].Add(j, map[i,j].BuildProxy());
            //    }
            //}
            //End Test Map creation

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
                               new Color(0,0,0,1)  
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 32, 32, 16), SpriteEffects.None);
                    core[Orientation.BR] = new SpriteArea(new Rectangle(96, 32, 32, 16), SpriteEffects.FlipHorizontally);
                    core[Orientation.TR] = new SpriteArea(new Rectangle(96, 32, 32, 16), SpriteEffects.None);
                    core[Orientation.TL] = new SpriteArea(new Rectangle(96, 32, 32, 16), SpriteEffects.FlipHorizontally);

                    testChunk[chunk_x, chunk_y] = core ;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;

                    //animated sprite
                    if ((chunk_x == Chunk.CHUNKS_SIZE - 1) && (chunk_y == Chunk.CHUNKS_SIZE - 1))
                    {
                        AnimatedEntity animated_entity = new AnimatedEntity(
                            new Vector3(chunk_x, chunk_y, 7),
                            32, 32, new Vector2(16, 27),
                            text, new Color(0,0,0,1) , Orientation.S);

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
                        CalculPositionsEntitiesHandler += animated_entity.calculateScreenRect;
                    }

                    //fontain
                    if ((chunk_x == 0) && (chunk_y == 0))
                    {

                        DrawableEntity testFontain = new DrawableEntity(
                            new Vector3(chunk_x, chunk_y, 7), 27, 34,
                            new Vector2(13, 31), text2,
                            new Color(0,0,0,1) , Orientation.S
                        );
                        testFontain[Orientation.BL] = new SpriteArea(new Rectangle(344, 714, 27, 34), SpriteEffects.None);

                        core.addEntity(testFontain);
                        CalculPositionsEntitiesHandler += testFontain.calculateScreenRect;

                        DrawableEntity testContainable = new DrawableEntity(
                            new Vector3(chunk_x, chunk_y, (float)9f), 32, 32,
                            new Vector2(16, 24), text2,
                            new Color(0,0,0,1) , Orientation.S
                        );
                        testContainable[Orientation.BL] = new SpriteArea(new Rectangle(205, 136, 32, 32), SpriteEffects.None);
                        core.addEntity(testContainable);
                        CalculPositionsEntitiesHandler += testContainable.calculateScreenRect;
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
                        CalculPositionsEntitiesHandler += testGuysBody.calculateScreenRect;

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
                        CalculPositionsEntitiesHandler += testGuysHead.calculateScreenRect;
                    }
                    chunk_x++;
                }
                chunk_y++;
            }
            Chunks[0] = new SortedList<int, ChunkProxy>();
            Chunks[0][0] = testChunk;
            
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
                                new Color(0,0,0,1) 
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk2[i, j] = core;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[0][1] = testChunk2;


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
                                new Color(0,0,0,1) 
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk3[i, j] = core;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[1] = new SortedList<int, ChunkProxy>();
            Chunks[1][0] = testChunk3;

            //calcul position and center
            CalculPositionsEntitiesHandler();
            CalculCenterEntity();

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
                                new Color(0,0,0,1) 
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk4[i, j] = core ;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[0][-1] = testChunk4;

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
                                new Color(0,0,0,1) 
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk5[i, j] =  core;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[-1] = new SortedList<int, ChunkProxy>();
            Chunks[-1][0] = testChunk5;


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
                                new Color(0,0,0,1) 
                    );
                    if (((i % 2) == 0) && ((j % 2) == 0))
                    {

                        DrawableEntity water = new DrawableEntity(
                            new Vector3(i - Chunk.CHUNKS_SIZE, j - Chunk.CHUNKS_SIZE, 0), 34, 21,
                            new Vector2(17, 20), text2,
                            new Color(0,0,0,1) , Orientation.S
                        );
                        water[Orientation.BL] = new SpriteArea(new Rectangle(307, 726, 34, 21), SpriteEffects.None);
                        water[Orientation.BR] = new SpriteArea(new Rectangle(307, 726, 34, 21), SpriteEffects.FlipHorizontally);
                        water[Orientation.TL] = new SpriteArea(new Rectangle(307, 726, 34, 21), SpriteEffects.None);
                        water[Orientation.TR] = new SpriteArea(new Rectangle(307, 726, 34, 21), SpriteEffects.FlipHorizontally);
                        core.addEntity(water);
                        CalculPositionsEntitiesHandler += water.calculateScreenRect;
                    }
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk6[i, j] = core;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[-1][-1] = testChunk6;


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
                                new Color(0,0,0,1) 
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk7[i, j] = core;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[1][-1] = testChunk7;


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
                                new Color(0,0,0,1) 
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk8[i, j] =  core;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[-1][1] = testChunk8;


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
                                new Color(0,0,0,1) 
                    );
                    core[Orientation.BL] = new SpriteArea(new Rectangle(96, 64, 32, 16), SpriteEffects.None);
                    testChunk9[i, j] =  core;
                    CalculPositionsEntitiesHandler += core.calculateScreenRect;
                    i++;
                }
                j++;
            }
            Chunks[1][1] = testChunk9;

            Chunks[0][0].calculCliffs();
            #endregion

            //calcul position and center
            CalculPositionsEntitiesHandler() ;
            CalculCenterEntity();

            //light
            test_light = new FixedLight(14, new Vector3(15, 15, 7), new Color(0.5f,0.5f,0.5f,1f));
            test_light.RegisterCoreInTheLight();

        }

        public override void Draw( GameTime gameTime ) {
            int count_sprite = 0;

            ShaderEffect.Parameters["AmbiantColor"].SetValue(AmbiantColor);
            spriteBatch.Begin( 
                SpriteSortMode.Deferred, 
                BlendState.AlphaBlend, 
                SamplerState.PointWrap, 
                DepthStencilState.Default, 
                RasterizerState.CullNone, 
                ShaderEffect 
            );
            /** Browse layer throw camera depth **/
            switch( Scene.GetInstance().Orientation ) {
                //Calcul pour SE
                case Orientation.SE:
                    foreach( var chunks_row in Chunks.Values ) {
                        foreach( var chunk in chunks_row.Values ) {
                            count_sprite += chunk.Draw( spriteBatch, gameTime );
                        }
                    }
                    break;
                //Calcul pour SO
                case Orientation.SO:
                    foreach( var chunks_row in Chunks.Values.Reverse() ) {
                        foreach( var chunk in chunks_row.Values ) {
                            count_sprite += chunk.Draw( spriteBatch, gameTime );
                        }
                    }
                    break;
                //Calcul pour NE
                case Orientation.NE:
                    foreach( var chunks_row in Chunks.Values ) {
                        foreach( var chunk in chunks_row.Values.Reverse() ) {
                            count_sprite += chunk.Draw( spriteBatch, gameTime );
                        }
                    }
                    break;
                //Calcul pour NO
                case Orientation.NO:
                    foreach( var chunks_row in Chunks.Values.Reverse() ) {
                        foreach( var chunk in chunks_row.Values.Reverse() ) {
                            count_sprite += chunk.Draw( spriteBatch, gameTime );
                        }
                    }
                    break;
            }

            spriteBatch.End();
            //return count_sprite;
        }

        public override void Update( GameTime gameTime ) {
            base.Update( gameTime );

            if( Mouse.GetState().LeftButton == ButtonState.Pressed ) {
                mouveMovement = Vector2.Zero;

                float deltaX = prevMouseState.X - Mouse.GetState().X;
                if( deltaX != 0 ) {
                    mouveMovement.X = deltaX / Camera.Coefficient;
                }

                float deltaY = prevMouseState.Y - Mouse.GetState().Y;
                if( deltaY != 0 ) {
                    mouveMovement.Y = deltaY / Camera.Coefficient;
                }
                Camera.move( mouveMovement );
            }

            if( Keyboard.GetState().IsKeyDown( Keys.F1 ) ) {
                Scene.GetInstance().Camera.turnCamera( Orientation.SE );
            }

            if( Keyboard.GetState().IsKeyDown( Keys.F2 ) ) {
                Scene.GetInstance().Camera.turnCamera( Orientation.SO );
            }

            if( Keyboard.GetState().IsKeyDown( Keys.F3 ) ) {
                Scene.GetInstance().Camera.turnCamera( Orientation.NO );
            }

            if( Keyboard.GetState().IsKeyDown( Keys.F4 ) ) {
                Scene.GetInstance().Camera.turnCamera( Orientation.NE );
            }

            if( Mouse.GetState().ScrollWheelValue != prevMouseState.ScrollWheelValue ) {
                //old cam
                int old_camera_width = this.Camera.Width;
                int old_camera_height = this.Camera.Height;

                //apply coeff
                this.Camera.Coefficient += (int) ( ( Mouse.GetState().ScrollWheelValue - prevMouseState.ScrollWheelValue ) / 120 );

                //new cam
                int new_camera_width = this.Camera.Width;
                int new_camera_height = this.Camera.Height;
                Camera.move(
                    new Vector2( 0.5f * ( old_camera_width - new_camera_width ),
                                0.5f * ( old_camera_height - new_camera_height )
                    )
                );
            }

            prevMouseState = Mouse.GetState();

            foreach( var chunks_row in Chunks.Values ) {
                foreach( var chunk in chunks_row.Values ) {
                    chunk.Update( gameTime );
                }
            }

            //Converge color to target
            if (AmbiantColor != ConvergeColor)
            {
                Vector4 shiftColor = Vector4.Multiply(AmbiantColor - ConvergeColor, 0.025f);
                AmbiantColor -= shiftColor;
            }
        }

        public void CalculCenterEntity() {

            /** Browse layer throw camera depth **/
            switch( Scene.GetInstance().Orientation ) {
                //Calcul pour SE
                case Orientation.SE:
                    foreach( var chunks_row in Chunks.Values ) {
                        foreach( var chunk in chunks_row.Values ) {
                            chunk.CalculCenterEntity();
                        }
                    }
                    break;
                //Calcul pour SO
                case Orientation.SO:
                    foreach( var chunks_row in Chunks.Values.Reverse() ) {
                        foreach( var chunk in chunks_row.Values ) {
                            chunk.CalculCenterEntity();
                        }
                    }
                    break;
                //Calcul pour NE
                case Orientation.NE:
                    foreach( var chunks_row in Chunks.Values ) {
                        foreach( var chunk in chunks_row.Values.Reverse() ) {
                            chunk.CalculCenterEntity();
                        }
                    }
                    break;
                //Calcul pour NO
                case Orientation.NO:
                    foreach( var chunks_row in Chunks.Values.Reverse() ) {
                        foreach( var chunk in chunks_row.Values.Reverse() ) {
                            chunk.CalculCenterEntity();
                        }
                    }
                    break;
            }

        }

        public bool IsLoadedCore( int x, int y ) {

            ChunkProxy chunk;
            int chunk_x = x / Chunk.CHUNKS_SIZE;
            int chunk_y = y / Chunk.CHUNKS_SIZE;

            //if negatif, need to substract 1 to get the good one
            if( x < 0 ) chunk_x--;
            if( y < 0 ) chunk_y--;

            SortedList<int, ChunkProxy> chunks_x;
            if( !Chunks.TryGetValue( chunk_x, out chunks_x ) ) {
                return false;
            }
            if( !chunks_x.TryGetValue( chunk_y, out chunk ) ) {
                return false;
            }

            //if chunk exists core exisist too
            //int core_x = x % Chunk.Chunk_Size;
            //int core_y = y % Chunk.Chunk_Size;

            return true;
        }

        public CoreProxy GetCore( int x, int y ) {

            int chunk_x = x / Chunk.CHUNKS_SIZE;
            int chunk_y = y / Chunk.CHUNKS_SIZE;

            //if negatif, need to substract 1 to get the good one
            if( x < 0 ) chunk_x--;
            if( y < 0 ) chunk_y--;

            ChunkProxy chunk = Chunks[ chunk_x ][ chunk_y ];

            int core_x = x % Chunk.CHUNKS_SIZE;
            int core_y = y % Chunk.CHUNKS_SIZE;

            //if negatif, coords are inversed
            if (core_x < 0) core_x = Chunk.CHUNKS_SIZE + core_x;
            if (core_y < 0) core_y = Chunk.CHUNKS_SIZE + core_y;

            return chunk[ core_x, core_y ];
        }

    }
}