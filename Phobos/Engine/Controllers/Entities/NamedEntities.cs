using Phobos.Engine.Models.Entities;
using Phobos.Engine.Models.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phobos.Engine.Controllers.Entities {
    /// <summary>
    /// This Controller simply help to access to registred Entities.
    /// </summary>
    static class NamedEntities {
        public static Dictionary<string, Dictionary<string, AEntity>> dictionaries;

        static NamedEntities() {
            dictionaries = new Dictionary<string, Dictionary<string, AEntity>>();
        }

        public static AEntity Get( string dictionary, string entityName ) {
            return NamedEntities.dictionaries[ dictionary ][ entityName ];
        }

        public static bool Set( string dictionary, string entityName , AEntity entity) {
            try {
                if( NamedEntities.dictionaries.ContainsKey( dictionary ) ) {
                    if( NamedEntities.dictionaries[ dictionary ].ContainsKey( entityName ) ) {
                        NamedEntities.dictionaries[ dictionary ][ entityName ] = entity;
                    } else {
                        NamedEntities.dictionaries[ dictionary ].Add( entityName, entity );
                    }
                } else {
                    Dictionary<string, AEntity> temp = new Dictionary<string, AEntity>();
                    temp.Add( entityName, entity );
                    NamedEntities.dictionaries.Add( dictionary, temp );
                }
            } catch {
                return false;
            }
            return true;
        }
    }
}
