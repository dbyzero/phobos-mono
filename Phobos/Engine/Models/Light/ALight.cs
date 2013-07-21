using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework ;
using Phobos.Engine.View;
using Phobos.Engine.View.Proxies.World;
using Phobos.Engine.View.Proxies.Entities;
using Phobos.Tools;
using Phobos.Cache;

namespace Phobos.Engine.Models.Light
{
    abstract class ALight
    {
        #region mutators and ascessors

        public Vector3 Position
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public List<DrawableEntity> listCoreInTheLight
        {
            get;
            private set;
        }

        public int Radius
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        } 

        #endregion

        #region constructors

        private ALight() { }

        public ALight(int radius, Vector3 position, Color color)
        {
            listCoreInTheLight = new List<DrawableEntity>();
            Active = true;
            Radius = radius;
            Color = color;
            Position = position;


        }

        #endregion

        public List<DrawableEntity> RegisterCoreInTheLight(Scene scene)
        {
            listCoreInTheLight.Clear();
            for (float i = Position.X - Radius; i < Position.X + Radius;i++ )
            {
                for (float j = Position.Y - Radius; j < Position.Y + Radius; j++)
                {
                    if (scene.IsLoadedCore((int)Math.Ceiling(i), (int)Math.Ceiling(j)))
                    {
                        CoreProxy core = scene.GetCore((int)Math.Ceiling(i), (int)Math.Ceiling(j));
                        float distance_to_core = Vector2.Subtract(new Vector2(core.X, core.Y), new Vector2(Position.X, Position.Y)).Length();
                        if (distance_to_core <= Radius)
                        {
                            listCoreInTheLight.Add(core);
                        }
                    }
                }
            }
            return listCoreInTheLight;
        }
        
        public void ApplyLight(Scene scene)
        {
            if (Active)
            {
                Dictionary<Vector2, SortedList<float, Vector2>> collisionMatrix = LightCache.getCacheForARadius(Radius);
                foreach (DrawableEntity ent in listCoreInTheLight)
                {
                    Vector2 relativePositionTarget = new Vector2(ent.X - Position.X, ent.Y - Position.Y);
                    SortedList<float, Vector2> collisionTileMatrix ;
                    collisionMatrix.TryGetValue(relativePositionTarget,out collisionTileMatrix) ;
                    if (collisionTileMatrix != null)
                    {
                        bool show = true;
                        foreach (KeyValuePair<float, Vector2> kvp in collisionTileMatrix)
                        {
                            if (relativePositionTarget.Length() < kvp.Value.Length()) break;
                            if (scene.IsLoadedCore((int)Position.X - (int)kvp.Value.X, (int)Position.Y - (int)kvp.Value.Y))
                            {
                                if (scene.GetCore((int)Position.X - (int)kvp.Value.X, (int)Position.Y - (int)kvp.Value.Y).Z > Position.Z)
                                {
                                    show = false;
                                }
                            }
                        }
                        if (show)
                        {
                            float distance_to_core = Vector2.Subtract(new Vector2(ent.X, ent.Y), new Vector2(Position.X, Position.Y)).Length();
                            ent.applyLight(Color.Multiply(Color, 1f - distance_to_core / Radius));
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Position:"+Position.ToString()+" Radius:"+Radius+" Color:"+Color;
        }
    }
}
