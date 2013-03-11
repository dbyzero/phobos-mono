using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Models.Light ;

namespace Phobos.Engine.Controllers.Light
{
    class LightController
    {
        private List<ALight> lights = new List<ALight>();

        public void AddLight(ALight light) {
            lights.Add(light);
            light.RegisterCoreInTheLight();
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
