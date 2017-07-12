using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;

namespace ERMine.Core.Parsing
{
    static class ModelParser
    {
        public readonly static Parser<IEntityRelationship> Relationship =
        (
            from relationship in RelationshipBinaryParser.Relationship.Or(RelationshipNAryParser.Relationship)
            select relationship
        );

        public readonly static Parser<IEntityRelationship> EntityAttributes =
        (
            from entity in EntityParser.Entity
            from lineTerminator in Parse.LineTerminator.XMany()
            from attributes in EntityParser.Attribute.XMany()
            select new EntityFactory().Create(entity, attributes)
        );

        public readonly static Parser<IEntityRelationship> DomainValues =
        (
            from label in DomainParser.Domain
            from lineTerminator in Parse.LineTerminator.XMany()
            from values in DomainParser.DomainValue.XMany()
            select new DomainFactory().Create(label, values)
        );

        public readonly static Parser<IEntityRelationship> EntityRelationship =
        (
            from entityRelationship in Relationship.Or(EntityAttributes).Or(DomainValues)
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
