using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;

namespace ERMine.Core.Parsing
{
    static class IsaUnionRelationshipParser
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

        private readonly static Parser<IsaMarkerStruct> IsaMarker =
        (
            from firstSeparator in Parse.Char('(')
            from type in Parse.Char('d').Or(Parse.Char('o'))
            from space2 in Parse.WhiteSpace.Many()
            from groupName in Identifier.Optional()
            from secondSeparator in Parse.Char(')')
            select new IsaMarkerStruct(){ Type = type, Name = groupName.GetOrElse(string.Empty) }
        );

        private readonly static Parser<IsaUnionRelationship> LeftRightIsaRelationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from completeness in Parse.Char('-').Or(Parse.Char('='))
            from marker in IsaMarker.Optional()
            from secondSeparator in Parse.Char('-')
            from way in Parse.String("|>")
            from thirdSeparator in Parse.Char('-')
            from space2 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select 
                marker.IsDefined ? 
                new IsaUnionRelationshipFactory().Create(
                    firstEntity, secondEntity, completeness == '-', marker.Get().Type, marker.Get().Name) :
                new IsaUnionRelationshipFactory().Create(
                    firstEntity, secondEntity, completeness == '-')
        );

        private readonly static Parser<IsaUnionRelationship> RightLeftIsaRelationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from firstSeparator in Parse.Char('-')
            from way in Parse.String("<|")
            from secondSeparator in Parse.Char('-')
            from marker in IsaMarker.Optional()
            from completeness in Parse.Char('-').Or(Parse.Char('='))
            from space2 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select
                marker.IsDefined ?
                new IsaUnionRelationshipFactory().Create(
                     secondEntity, firstEntity, completeness == '-', marker.Get().Type, marker.Get().Name) :
                new IsaUnionRelationshipFactory().Create(
                    secondEntity, firstEntity, completeness == '-')
        );

        private readonly static Parser<string> IsaMember =
        (
            from endLine in Grammar.NewLine.Optional()
            from space1 in Parse.WhiteSpace.Many().Optional()
            from way in Parse.String("|>")
            from thirdSeparator in Parse.Char('-')
            from space2 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select secondEntity
        );

        private readonly static Parser<IsaUnionRelationship> MultipleIsaRelationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from completeness in Parse.Char('-').Or(Parse.Char('='))
            from marker in IsaMarker
            from secondSeparator in Parse.Char('-')
            from isaMembers in IsaMember.AtLeastOnce()
            select
                new IsaUnionRelationshipFactory().Create(
                    firstEntity, isaMembers.ToArray(), completeness == '-', marker.Type, marker.Name) 
        );

        private readonly static Parser<string> UnionMember =
        (
            from endLine in Grammar.NewLine.Optional()
            from space1 in Parse.WhiteSpace.Many().Optional()
            from thirdSeparator in Parse.Char('-')
            from space2 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select secondEntity
        );

        private readonly static Parser<IsaUnionRelationship> MultipleUnionRelationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from completeness in Parse.Char('-').Or(Parse.Char('='))
            from way in Parse.String("<|-(u)")
            from unionMembers in UnionMember.AtLeastOnce()
            select
                new IsaUnionRelationshipFactory().Create(
                    firstEntity, unionMembers.ToArray(), completeness == '-', 'u', string.Empty)
        );

        private readonly static Parser<IsaUnionRelationship> LeftRightUnionRelationship =
        (
            from firstEntity in Grammar.BracketTextual
            from space1 in Parse.WhiteSpace.Many()
            from completeness in Parse.Char('-').Or(Parse.Char('='))
            from unionSymbol in Parse.String("(u")
            from space2 in Parse.WhiteSpace.Many()
            from identifier in Identifier
            from way in Parse.String(")-|>-")
            from space3 in Parse.WhiteSpace.Many()
            from secondEntity in Grammar.BracketTextual
            select
                new IsaUnionRelationshipFactory().Create(
                    secondEntity, firstEntity, completeness == '-', 'u', identifier)
        );

        public readonly static Parser<IsaUnionRelationship> IsaRelationship =
        (
            from isaRelationship in MultipleIsaRelationship.Or(MultipleUnionRelationship).Or(LeftRightIsaRelationship).Or(RightLeftIsaRelationship).Or(LeftRightUnionRelationship)
            select isaRelationship
        );

        


    }
}
