using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Services {
    /// <summary>
    /// This class provide a simple way to access to all of the registered services.
    /// </summary>
    public static class ServicesManager {
        #region Fields & Properties

        static GameServiceContainer services;

        static GameServiceContainer Instance {
            get {
                if( services == null ) {
                    services = new GameServiceContainer();
                }
                return services;
            }
        }

        #endregion
        #region Methods

        public static T GetService<T>() {
            return (T) Instance.GetService( typeof( T ) );
        }

        public static void AddService<T>( T service ) {
            Instance.AddService( typeof( T ), service );
        }

        public static void RemoveService<T>() {
            Instance.RemoveService( typeof( T ) );
        }

        #endregion
    }
}
