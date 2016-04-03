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
        readonly static Parser<IEnumerable<IEntityRelationship>> EntityRelationship =
        (
            from relationships in RelationshipParser.Relationship.Optional()
            from entities in EntityParser.Entity.Optional()
            select entities.IsDefined && relationships.IsDefined  
                    ? Enumerable.Repeat(entities.GetOrDefault() as IEntityRelationship, 1)
                        .Union(Enumerable.Repeat(relationships.GetOrDefault() as IEntityRelationship, 1))
                    : entities.IsDefined ? Enumerable.Repeat(entities.GetOrDefault() as IEntityRelationship, 1)
                                        : Enumerable.Repeat(relationships.GetOrDefault() as IEntityRelationship, 1)
        );

        public readonly static Parser<IEnumerable<IEntityRelationship>> EntityRelationships =
        (
            from members in EntityRelationship.Many().End()
            select members.SelectMany(x => x)
        );
    }
}
