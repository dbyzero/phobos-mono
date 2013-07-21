using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework ;
using Phobos.Engine.View;
using Phobos.Engine.View.Proxies.World;
using Phobos.Engine.View.Proxies.Entities;

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

        private  List<DrawableEntity> listCoreInTheLight
        {
            get;
            set;
        }

        public float Radius
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

        public void RegisterCoreInTheLight(Scene scene) {
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
        }
        
        public void ApplyLight()
        {
            if (Active)
            {
                foreach (DrawableEntity ent in listCoreInTheLight)
                {
                    float distance_to_core = Vector2.Subtract(new Vector2(ent.X, ent.Y), new Vector2(Position.X, Position.Y)).Length();
                    ent.applyLight(Color.Multiply(Color, 1f - distance_to_core/Radius));
                }
            }
        }
    }
}
