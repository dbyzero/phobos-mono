using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Phobos.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Phobos.Gui.Widgets
{
    class SimpleButton : AButton
    {

        #region Propriété publiques

        #endregion

        #region Constructeurs
        public SimpleButton(AWidget parent, int x, int y, string text)
            : base(parent, x, y, 128, 32)
        {
            #region Ajout du Label
            
            //la référence est gardée afin d'éviter d'en recréer une à chaque appel de draw.
            Vector2 textSize = GameEngine.fonts[ @"fonts\Corbel14Bold" ].MeasureString( text );
           
            while( textSize.X > 128 ) {
                text = text.Substring( 0, text.Count() - 2 );
                textSize = GameEngine.fonts[ @"fonts\Corbel14Bold" ].MeasureString( text );
            }

            ALabel label = 
                new SimpleTextLabel( this, location.X + 64 - (int) ( textSize.X / 2 ), location.Y + 16 - (int) ( textSize.Y / 2 ), text, @"fonts\Corbel14Bold", Color.White );

            children = new List<AWidget>();
            children.Add(label);
            
            #endregion
        }
        #endregion

        #region Méthodes surchargée

        public static void LoadContent() {
            GameEngine.LoadRessource<Texture2D>( @"gui\button" );
            GameEngine.LoadRessource<SpriteFont>( @"fonts\Corbel14Bold" );
        }

        #endregion

        #region Implémentation de IDrawable

        public override void Draw(GameTime gameTime)
        {
            
            GameEngine.spriteBatch.Draw(GameEngine.textures[@"gui\button"],this.location,Color.White);

            foreach (AWidget child in children)
            {
                child.Draw(gameTime);
            }
             
        }

        #endregion
        
    }   
}
