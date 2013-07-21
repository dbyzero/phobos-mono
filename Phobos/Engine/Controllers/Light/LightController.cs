using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Models.Light;
using Phobos.Engine.View;

namespace Phobos.Engine.Controllers.Light
{
    class LightController
    {
        private List<ALight> lights = new List<ALight>();

        public void AddLight(ALight light, Scene scene) {
            lights.Add(light);
            light.RegisterCoreInTheLight(scene);
        }

        public void RemoveLight(ALight light)
        {
            lights.Remove(light);
        }

        public void PurgeLights()
        {
            lights.Clear();
        }

        public void ApplyLights()
        {
            foreach(ALight light in lights) {
                light.ApplyLight();
            }
        }

        //mainly for test purpose
        public ALight GetALight()
        {
            return lights.First();
        }
    }
}
