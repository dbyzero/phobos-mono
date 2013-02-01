using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine {
    enum EngineStatus {
        INITIALIZATION, CONTENTLOADING, CONTENTUNDLOADING, UPDATING, DRAWING //oulà pas bon, pas utiliser (kaboum si updating et drawing sont appelé de manière asyn)
    }
}
