using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phobos
{
    struct Layer
    {
        public float layerDepth;
        public string layerName;

        public Layer(float _layerDepth, string _layerName = "")
        {
            this.layerDepth = _layerDepth;
            this.layerName = _layerName;
        }
    }
}
