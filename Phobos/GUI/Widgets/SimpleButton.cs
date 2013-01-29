using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Phobos.Engine;

namespace Phobos.GUI.Widgets
{
    class SimpleButton : AButton
    {

        #region Propriété publiques

        #endregion

        #region Constructeurs
        public SimpleButton(AWidget parent, int x, int y, string label) : base(parent, x, y , 128, 32)
        {
            #region Ajout du Label

            Label = new SimpleTextLabel(this, new Point(x, y), label);
            Childrend = new List<AWidget>();
            Childrend.Add(Label);
            
            #endregion
        }
        #endregion

        #region Methodes

        #endregion

        #region Implémentation de IDrawable

        public override void Draw(GameTime gameTime)
        {
            GameEngine.spriteBatch.Draw(GameEngine.ressources[@"gui\button"],this.Location,Color.White);
            /*
             * TODO: Draw sur le label;
            foreach (AWidget child in Childrend)
            {
                
            }
             */
        }

        #endregion
        
    }   
}
