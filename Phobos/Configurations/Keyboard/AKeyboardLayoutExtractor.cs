using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Phobos.Configurations.Keyboard
{

    /// <summary>
    /// Cette interface défini les méthodes nécessaire à obtenir le format du clavié utilisé.
    /// </summary>
    abstract class AKeyboardLayoutExtractor
    {

        public const string DEFAULT_LAYOUT = "US"; //C'est le nom du clavier us sous windows

        /// <summary>
        /// Cette méthode retourn le nom système du layout utilisé.
        /// </summary>
        public abstract string getCurrentSystemLayout();
    }
}
