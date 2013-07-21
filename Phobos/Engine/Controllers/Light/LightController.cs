using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Models.Light;
using Phobos.Engine.View.Proxies.Entities;
using Phobos.Engine.View;
using Phobos.Cache;
using Microsoft.Xna.Framework;
using Phobos.Tools;

namespace Phobos.Engine.Controllers.Light
{
    class LightController
    {
        private List<ALight> lights = new List<ALight>();

        public void AddLight(ALight light, Scene scene) {
            lights.Add(light);
            light.RegisterCoreInTheLight(scene);
            LightCache.setCache(light.Radius);
        }

        public void RemoveLight(ALight light)
        {
            lights.Remove(light);
        }

        public void PurgeLights()
        {
            lights.Clear();
        }

        public void ApplyLights(Scene scene)
        {
            foreach(ALight light in lights) {
                light.ApplyLight(scene);
            }
        }

        //mainly for test purpose
        public ALight GetARadomLight()
        {
            return lights.First();
        }
    }
}
