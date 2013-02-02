using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phobos.Engine.Gui.pWidgets;

namespace Phobos.Gui {

    /// <summary>
    /// Cette classe permet de définir les ressources utilisée par le template de l'ui afin d'alleger le code propre au widgets.
    /// </summary>
    class GuiTemplate {

        /// <summary>
        /// Cette méthode va faire une appel sur la methode statique LoadRessources de chaque widget défini.
        /// </summary>
        protected void LoadContent() {

            GuiTemplate.LoadSpecificRessources();
        }

        /// <summary>
        /// Cette méthode va appeler explicitement les ressources.
        /// </summary>
        public static void LoadSpecificRessources() {

        }
    }
}
