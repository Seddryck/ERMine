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
    static class RelationshipTernaryParser
    {
        readonly static Parser<Cardinality> Cardinality =
        (
            from cardinality in Parse.Char('*').Return(Modeling.Cardinality.ZeroOrMore)
                                .Or(Parse.Char('?').Return(Modeling.Cardinality.ZeroOrOne)
                                .Or(Parse.Char('1').Return(Modeling.Cardinality.ExactyOne)
                                .Or(Parse.Char('+').Return(Modeling.Cardinality.OneOrMore)
                                )))
            select cardinality
        );

        public readonly static Parser<Relationship> Relationship =
        (
            
            from firstSeparator in Parse.Char('-')
            from label in Grammar.Textual.Optional()
            from secondSeparator in Parse.Char('-')
            from firstEntity in Grammar.BracketTextual
            from firstCardinality in Cardinality
            from secondEntity in Grammar.BracketTextual
            from secondCardinality in Cardinality
            from thirdEntity in Grammar.BracketTextual
            from thirdCardinality in Cardinality
            select new RelationshipFactory().Create(    label.GetOrDefault()
                                                        , new List<Tuple<string, Cardinality>>()
                                                        {
                                                            new Tuple<string, Modeling.Cardinality> (firstEntity, firstCardinality)
                                                            , new Tuple<string, Modeling.Cardinality> (secondEntity, secondCardinality)
                                                            , new Tuple<string, Modeling.Cardinality> (thirdEntity, thirdCardinality)
                                                        }
                                                    )
        );

        public readonly static Parser<IEnumerable<Relationship>> Relationships =
        (
            Relationship.Many()
        );


    }
}
