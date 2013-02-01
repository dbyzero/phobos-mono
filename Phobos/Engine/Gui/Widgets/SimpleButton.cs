using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Phobos.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Phobos.Engine.Content;

namespace Phobos.Gui.Widgets {
    class SimpleButton : AButton {

        #region Propriété publiques
        public delegate void ClickHandler( object sender, EventArgs e );
        public event ClickHandler Click;

        public ButtonState status;
        public enum ButtonState { ENABLED, DISABLED, DOWN, HOVER }
        #endregion

        #region Constructeurs
        public SimpleButton( AWidget parent, int x, int y, string text )
            : base( parent, x, y, 128, 32 ) {

            this.status = ButtonState.ENABLED;
            ContentHelper.Load<Texture2D>( @"gui\button" );
            ContentHelper.Load<Texture2D>( @"gui\button_down" );
            ContentHelper.Load<Texture2D>( @"gui\button_hover" );
            ContentHelper.Load<SpriteFont>( @"fonts\Corbel14Bold" );
            Vector2 textSize = ContentHelper.Get<SpriteFont>( @"fonts\Corbel14Bold" ).MeasureString( text );

            while( textSize.X > 128 ) {
                text = text.Substring( 0, text.Count() - 2 );
                textSize = ContentHelper.Get<SpriteFont>( @"fonts\Corbel14Bold" ).MeasureString( text );
            }

            ALabel label = 
                new SimpleTextLabel( this, location.X + 64 - (int) ( textSize.X / 2 ), location.Y + 16 - (int) ( textSize.Y / 2 ), text, @"fonts\Corbel14Bold", Color.White );

            children = new List<AWidget>();
            children.Add( label );


        }
        #endregion

        #region Méthodes

        protected void OnClick() {
            if( Click != null ) {
                Click( this, EventArgs.Empty );
            }
        }

        #endregion

        #region Implémentation de IDrawable

        public override void Draw( GameTime gameTime ) {

            switch( this.status ) {
                case ButtonState.ENABLED:
                    GameEngine.spriteBatch.Draw( ContentHelper.Get<Texture2D>( @"gui\button" ), this.location, Color.White );
                    break;
                case ButtonState.DOWN:
                    GameEngine.spriteBatch.Draw( ContentHelper.Get<Texture2D>( @"gui\button_down" ), this.location, Color.White );
                    break;
                case ButtonState.HOVER:
                    GameEngine.spriteBatch.Draw( ContentHelper.Get<Texture2D>( @"gui\button_hover" ), this.location, Color.White );
                    break;
                case ButtonState.DISABLED:
                    GameEngine.spriteBatch.Draw( ContentHelper.Get<Texture2D>( @"gui\button_down" ), this.location, Color.Gray );
                    break;
            }
            

            foreach( AWidget child in children ) {
                child.Draw( gameTime );
            }
        }

        #endregion

    }
}
