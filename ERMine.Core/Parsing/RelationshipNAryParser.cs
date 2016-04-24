using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;
using System;

namespace ERMine.Core.Parsing
{
    static class RelationshipNAryParser
    {
        
        public readonly static Parser<Tuple<string, Modeling.Cardinality>> EntityCardinality =
        (
            from space1 in Parse.WhiteSpace.Many()
            from entity in Grammar.BracketTextual
            from cardinality in Keyword.Cardinality
            select new Tuple<string, Modeling.Cardinality> (entity, cardinality)
        );


        public readonly static Parser<Relationship> Relationship =
        (
            from firstSeparator in Parse.Char('-')
            from label in Grammar.Textual.Optional()
            from secondSeparator in Parse.Char('-')
            from first in EntityCardinality
            from others in EntityCardinality.AtLeastOnce()
            select new RelationshipFactory().Create
                            (    
                                label.GetOrDefault()
                                , Enumerable.Repeat(first, 1).Union(others)
                            )
        );
        
    }
}
