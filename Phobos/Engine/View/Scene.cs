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
using Phobos.Test;


namespace Phobos.Engine.View {
    /**
     * <summary> Une scene, utilisation de singleton </summary>
     */


    class Scene : DrawableGameComponent
    {
        public delegate void CalculsEntitiesHandler(Scene scene);

        //delegate utiliser pour recalculer toute les positions des entities, appeler en zoom et rotation de camera
        public CalculsEntitiesHandler CalculPositionsEntitiesHandler;

 	    private Camera camera;
 	    private Orientation orientation;
        private SpriteBatch spriteBatch;
        private MouseState prevMouseState;
        private Vector2 mouveMovement;
        private DrawableEntity centerEntity; //utiliser lors de la rotation de la camera

        public LightController LightController = new LightController();
        
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
        private Dictionary<string, Vector4> AmbiantColors { set; get; }  
        public SortedList<int, SortedList<int, ChunkProxy>> Chunks {
            set;
            get;
        }
        #endregion

        public Scene()
            : base( GameEngine.Instance ) {
            Chunks = new SortedList<int, SortedList<int, ChunkProxy>>();
            AmbiantColors = new Dictionary<string, Vector4>();
        }

        public DrawableEntity CenterEntity {
            get { return centerEntity; }
            set { centerEntity = value; }
        }

        public override void Initialize() {
            base.Initialize();
            spriteBatch = new SpriteBatch( GameEngine.Instance.GraphicsDevice );

            camera = new Camera(this);
            orientation = Orientation.SO;
            ShaderEffect = null ;
                        
            //calcul position and center
            if (CalculPositionsEntitiesHandler != null)
            {
                CalculPositionsEntitiesHandler(this);
            }

            CalculCenterEntity();
            configuration();
        }

        /// <summary>
        /// Todo : make it by a conf file or something else
        /// </summary>
        private void configuration()
        {
            ShaderEffect = GameEngine.Instance.Content.Load<Effect>(@"shaders\coloration");

            addAmbiantColor("sunrise", new Vector4(0.74f, 0.53f, 0.36f, 1.0f));
            addAmbiantColor("noon", new Vector4(1.0f, 1.0f, 0.95f, 1.0f));
            addAmbiantColor("night", new Vector4(0.2557f, 0.29f, 0.42f, 1.0f));
            addAmbiantColor("evening", new Vector4(0.75f, 0.49f, 0.36f, 1.0f));

            addAmbiantColor("current", getAmbiantColor("night")); //current color
            addAmbiantColor("converge", getAmbiantColor("night")); //current color
        }

        public Vector4 getAmbiantColor(string key)
        {
            Vector4 color;
            AmbiantColors.TryGetValue(key, out color);
            return color;
        }

        public void addAmbiantColor(string key, Vector4 color)
        {
            AmbiantColors.Add(key, color);
        }

        public void updateAmbiantColor(string key, Vector4 color)
        {
            AmbiantColors[key] = color ;
        }

        public override void Draw( GameTime gameTime ) {

            LightController.ApplyLights();

            int count_sprite = 0;

            if (ShaderEffect != null)
            {
                ShaderEffect.Parameters["AmbiantColor"].SetValue(getAmbiantColor("current"));
            }

            spriteBatch.Begin( 
                SpriteSortMode.Deferred, 
                BlendState.AlphaBlend, 
                SamplerState.PointWrap, 
                DepthStencilState.Default, 
                RasterizerState.CullNone, 
                ShaderEffect 
            );
            /** Browse layer throw camera depth **/
            switch( Orientation ) {
                //Calcul pour SE
                case Orientation.SE:
                    foreach( var chunks_row in Chunks.Values ) {
                        foreach( var chunk in chunks_row.Values ) {
                            count_sprite += chunk.Draw(spriteBatch, gameTime, this);
                        }
                    }
                    break;
                //Calcul pour SO
                case Orientation.SO:
                    foreach( var chunks_row in Chunks.Values.Reverse() ) {
                        foreach( var chunk in chunks_row.Values ) {
                            count_sprite += chunk.Draw(spriteBatch, gameTime, this);
                        }
                    }
                    break;
                //Calcul pour NE
                case Orientation.NE:
                    foreach( var chunks_row in Chunks.Values ) {
                        foreach( var chunk in chunks_row.Values.Reverse() ) {
                            count_sprite += chunk.Draw(spriteBatch, gameTime, this);
                        }
                    }
                    break;
                //Calcul pour NO
                case Orientation.NO:
                    foreach( var chunks_row in Chunks.Values.Reverse() ) {
                        foreach( var chunk in chunks_row.Values.Reverse() ) {
                            count_sprite += chunk.Draw(spriteBatch, gameTime, this);
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


            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                CalculCenterEntity();
            }

            if( Keyboard.GetState().IsKeyDown( Keys.F1 ) ) {
                Camera.turnCamera(Orientation.SE);
                CalculCenterEntity();
            }

            if( Keyboard.GetState().IsKeyDown( Keys.F2 ) ) {
                Camera.turnCamera(Orientation.SO);
                CalculCenterEntity();
            }

            if( Keyboard.GetState().IsKeyDown( Keys.F3 ) ) {
                Camera.turnCamera(Orientation.NO);
                CalculCenterEntity();
            }

            if( Keyboard.GetState().IsKeyDown( Keys.F4 ) ) {
                Camera.turnCamera(Orientation.NE);
                CalculCenterEntity();
            }

            if( Mouse.GetState().ScrollWheelValue != prevMouseState.ScrollWheelValue ) {
                //old cam
                int old_camera_width = this.Camera.Width;
                int old_camera_height = this.Camera.Height;

                //apply coeff
                float oldCoeff = Camera.Coefficient;
                Camera.Coefficient += (int)((Mouse.GetState().ScrollWheelValue - prevMouseState.ScrollWheelValue) / 120);
                if (oldCoeff != Camera.Coefficient)
                {
                    CalculPositionsEntitiesHandler(this);
                }

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
            convergeAmbiantColor();
            
        }

        private void convergeAmbiantColor()
        {
            if (getAmbiantColor("ambiant") != getAmbiantColor("converge"))
            {
                Vector4 shiftColor = Vector4.Multiply(getAmbiantColor("ambiant") - getAmbiantColor("converge"), 0.025f);
                updateAmbiantColor("ambiant", getAmbiantColor("converge") - shiftColor);
            }
        }

        public void CalculCenterEntity() {

            /** Browse layer throw camera depth **/
            switch( Orientation ) {
                //Calcul pour SE
                case Orientation.SE:
                    foreach( var chunks_row in Chunks.Values ) {
                        foreach( var chunk in chunks_row.Values ) {
                            chunk.CalculCenterEntity(this);
                        }
                    }
                    break;
                //Calcul pour SO
                case Orientation.SO:
                    foreach( var chunks_row in Chunks.Values.Reverse() ) {
                        foreach( var chunk in chunks_row.Values ) {
                            chunk.CalculCenterEntity(this);
                        }
                    }
                    break;
                //Calcul pour NE
                case Orientation.NE:
                    foreach( var chunks_row in Chunks.Values ) {
                        foreach( var chunk in chunks_row.Values.Reverse() ) {
                            chunk.CalculCenterEntity(this);
                        }
                    }
                    break;
                //Calcul pour NO
                case Orientation.NO:
                    foreach( var chunks_row in Chunks.Values.Reverse() ) {
                        foreach( var chunk in chunks_row.Values.Reverse() ) {
                            chunk.CalculCenterEntity(this);
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

            //if chunk exists core exists too
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