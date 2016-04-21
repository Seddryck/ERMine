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
        public readonly static Parser<IEnumerable<IEntityRelationship>> Relationships =
        (
            from binaryRelationships in RelationshipBinaryParser.Relationship.Many()
            from ternaryRelationships in RelationshipTernaryParser.Relationship.Many()
            select binaryRelationships
                        .Union(ternaryRelationships)
        );

        public readonly static Parser<IEnumerable<IEntityRelationship>> Entities =
        (
            from entities in EntityParser.Entity.Many()
            select entities
        );

        readonly static Parser<IEnumerable<IEntityRelationship>> EntityRelationship =
        (
            from relationships in Relationships.Many()
            from entity in EntityParser.Entity.Optional()
            select entity.IsDefined ? relationships.SelectMany(r => r).Union(Enumerable.Repeat((IEntityRelationship) entity.GetOrDefault(), 1)) : relationships.SelectMany(r => r)
        );

        public readonly static Parser<IEnumerable<IEntityRelationship>> EntityRelationships =
        (
            from members in EntityRelationship.Many().End()
            select members.SelectMany(x => x)
        );
    }
}
