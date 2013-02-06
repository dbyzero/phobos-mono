using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Phobos.Engine.Content {
    class ContentHelper{

        public static void Initialize() {
            bundles = new Dictionary<string, object>();
            bundles.Add( "Texture2D", new ContentBundle<Texture2D>() );
            bundles.Add( "SpriteFont", new ContentBundle<SpriteFont>() );
        }

        public static T Load<T>( string contentName ) {
            //if( !GameEngine.loading ) throw new ContentLoadException( "T ContentHelper.Load<T> ne peut être appelé en dehors du contexte de la méthode GameEngine.LoadContent()" );
            T ressource = GameEngine.Instance.Content.Load<T>( contentName );
            
            switch( ressource.GetType().Name ) {
                case "Texture2D":
                    ( ( ContentHelper.bundles[ "Texture2D" ] ) as ContentBundle<Texture2D> ).Add( contentName, ressource as Texture2D);
                    break;
                case "SpriteFont":

                    ( ( ContentHelper.bundles[ "SpriteFont" ] ) as ContentBundle<SpriteFont> ).Add( contentName, ressource as SpriteFont);
                    break;
            }
            return ressource;
        }

        public static T Get<T>( string contentName ) {
            foreach( KeyValuePair<string, object> bundle in bundles ) {
                if( bundle.Value is ContentBundle<T> ) {
                    return ( bundle.Value as ContentBundle<T> )[ contentName ];
                }
            }
            return default( T );
        }

        private static Dictionary<string, object> bundles; //TODO: pas possible de définir une bibliothèque contenant un type générique sans précisé le type ?

    }
}
