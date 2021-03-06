﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;

namespace ERMine.Core.Parsing
{
    static class EntityParser
    {
        public readonly static Parser<string> Entity =
        (
            from label in Grammar.AsmToken(Grammar.BracketTextual)
            select label
        );

        public readonly static Parser<Attribute> Attribute =
        (
            from keyType in Keyword.IsPartOfKey.Optional()
            from label in Grammar.AsmToken(Grammar.Textual)
            from dataType in DataType.Optional()
            from isSparse in Keyword.IsSparse.Optional()
            from isNullable in Keyword.IsNullable.Optional()
            from isImmutable in Keyword.IsImmutable.Optional()
            from isMultiValued in Keyword.IsMultiValued.Optional()
            from isDerived in Keyword.IsDerived.Optional()
            from formulaDerived in Formula.Derived.Optional()
            from isDefault in Keyword.IsDefault.Optional()
            from formulaDefault in Formula.Default.Optional()
            from endDeclaration in Grammar.Terminator
            select new Attribute()
            {
                Label = label
                , DataType = dataType.GetOrDefault()
                , IsNullable = isNullable.IsDefined
                , IsSparse = isSparse.IsDefined
                , Key = keyType.IsDefined ? keyType.Get() : KeyType.None
                , IsImmutable = isImmutable.IsDefined
                , IsMultiValued = isMultiValued.IsDefined
                , IsDerived = isDerived.IsDefined || formulaDerived.IsDefined
                , DerivedFormula = formulaDerived.GetOrElse(string.Empty).Trim()
                , IsDefault = isDefault.IsDefined || formulaDefault.IsDefined
                , DefaultFormula = formulaDefault.GetOrElse(string.Empty).Trim()
            }
        );

        readonly static Parser<string> DataType =
        (
            from type in Grammar.Textual
            from additional in DataTypeLength.Or(DataTypePrecision).Optional()
            select type + additional.GetOrDefault()
        );

        readonly static Parser<string> DataTypeLength =
        (
            from open in Parse.Char('(')
            from length in Parse.Number
            from close in Parse.Char(')')
            select string.Format("({0})", length)
        );

        readonly static Parser<string> DataTypePrecision =
        (
            from open in Parse.Char('(')
            from length in Parse.Number
            from separator in Parse.Char(',')
            from precision in Parse.Number
            from close in Parse.Char(')')
            select string.Format("({0},{1})", length, precision)
        );
    }
}
