using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;

namespace ERMine.Core.Parsing
{
    static class IsaRelationshipParser
    {

        private readonly static Parser<IsaRelationship> OneWayIsaRelationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from firstSeparator in Parse.Char('-')
            from secondSeparator in Parse.Char('-')
            from way in Parse.Char(')')
            from thirdSeparator in Parse.Char('-')
            from space2 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select new IsaRelationshipFactory().Create(firstEntity, secondEntity)
        );

        private readonly static Parser<IsaRelationship> ReverseIsaRelationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from firstSeparator in Parse.Char('-')
            from way in Parse.Char('(')
            from secondSeparator in Parse.Char('-')
            from thirdSeparator in Parse.Char('-')
            from space2 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select new IsaRelationshipFactory().Create(secondEntity, firstEntity)
        );

        public readonly static Parser<IsaRelationship> IsaRelationship =
        (
            from isaRelationship in OneWayIsaRelationship.Or(ReverseIsaRelationship)
            select isaRelationship
        );

        //public readonly static Parser<IsaMarker> IsaMarker =
        //(
        //    from firstSeparator in Parse.Char('(')
        //    from label in Parse.Char('d')
        //    from group in Parse.Char('d')
        //    from secondSeparator in Parse.Char(')')
        //    from way in Parse.Char(')')
        //    select new IsaMarkerFactory().Create(label, group)
        //);


    }
}
