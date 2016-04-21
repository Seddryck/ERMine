using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;

namespace ERMine.Core.Parsing
{
    static class RelationshipBinaryParser
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
            from firstEntity in Grammar.BracketTextual
            from firstCardinality in Cardinality
            from firstSeparator in Parse.Char('-')
            from label in Grammar.Textual.Optional()
            from secondSeparator in Parse.Char('-')
            from secondCardinality in Cardinality
            from secondEntity in Grammar.BracketTextual
            select new RelationshipFactory().Create(label.GetOrDefault(), firstEntity, firstCardinality, secondEntity, secondCardinality)
        );

        public readonly static Parser<IEnumerable<Relationship>> Relationships =
        (
            Relationship.Many()
        );


    }
}
