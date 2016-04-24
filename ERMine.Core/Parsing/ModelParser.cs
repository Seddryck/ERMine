using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;

namespace ERMine.Core.Parsing
{
    static class ModelParser
    {
        public readonly static Parser<IEntityRelationship> Relationship =
        (
            from relationship in RelationshipBinaryParser.Relationship.Or(RelationshipNAryParser.Relationship)
            select relationship
        );

        public readonly static Parser<IEntityRelationship> EntityRelationship =
        (
            from entityRelationship in Grammar.AsmToken(Relationship).Or(EntityParser.Entity).Or(AttributeParser.Attribute)
            from lineTerminator in Parse.LineTerminator.XMany()
            select entityRelationship
        );

        public readonly static Parser<IEnumerable<IEntityRelationship>> EntityRelationships =
        (
            from members in EntityRelationship.XMany().End()
            select members
        );

        
    }
}
