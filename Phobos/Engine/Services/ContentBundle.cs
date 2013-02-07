using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Content {

    /// <summary>
    /// Cette classe représente un 'package' d'un type de ressource.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ContentBundle<T> {
        #region Fields and Propreties

        //Indexer
        public T this[ string name ] {
            get {
                return content[ name ];
            }
            set {
                this.Add( name, value );
            }
        }

        #endregion

        #region Constructors
        public ContentBundle() {
            this.content = new Dictionary<string, T>();
        }
        #endregion

        #region Methodes
        public void Add( string name, T content ) {
            if( !this.content.ContainsKey( name ) ) {
                Console.WriteLine( "CONTENT: " + content.GetType().Name + "::['" + name + "'] a été ajouté au ContentBundle['" + content.GetType().Name + "']" );

                this.content.Add( name, content );

                Console.WriteLine( "CONTENT: " + content.GetType().Name + "::['" + name + "'] existait déjà au ContentBundle['" + content.GetType().Name + "']" );
            }
        }
        #endregion

        #region Privates
        private Dictionary<string, T> content;
        #endregion
    }
}
