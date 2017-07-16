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
        public struct IsaMarkerStruct
        {
            internal char Type;
            internal string Name;
        }

        public readonly static Parser<string> Identifier =
        (
            from groupSeparator in Parse.Char('#')
            from groupName in Grammar.Textual.Or(Grammar.Number)
            select groupName
        );

        public readonly static Parser<IsaMarkerStruct> IsaMarker =
        (
            from firstSeparator in Parse.Char('(')
            from type in Parse.Char('d').Or(Parse.Char('o'))
            from space2 in Parse.WhiteSpace.Many()
            from groupName in Identifier.Optional()
            from secondSeparator in Parse.Char(')')
            select new IsaMarkerStruct(){ Type = type, Name = groupName.GetOrElse(string.Empty) }
        );

        private readonly static Parser<IsaRelationship> OneWayIsaRelationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from firstSeparator in Parse.Char('-')
            from marker in IsaMarker.Optional()
            from secondSeparator in Parse.Char('-')
            from way in Parse.Char(')')
            from thirdSeparator in Parse.Char('-')
            from space2 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select 
                marker.IsDefined ? 
                new IsaRelationshipFactory().Create(
                    firstEntity, secondEntity, marker.Get().Type, marker.Get().Name) :
                new IsaRelationshipFactory().Create(
                    firstEntity, secondEntity)
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

        


    }
}
