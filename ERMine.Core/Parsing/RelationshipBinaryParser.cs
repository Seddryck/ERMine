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
        

        public readonly static Parser<Relationship> Relationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from firstCardinality in Keyword.Cardinality
            from firstSeparator in Parse.Char('-')
            from label in Grammar.Textual.Optional()
            from secondSeparator in Parse.Char('-')
            from secondCardinality in Keyword.Cardinality
            from space2 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select new RelationshipFactory().Create(label.GetOrDefault(), firstEntity, firstCardinality, secondEntity, secondCardinality)
        );
        

    }
}
