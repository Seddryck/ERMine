﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;

namespace ERMine.Core.Parsing
{
    static class AttributeParser
    {
        public readonly static Parser<Attribute> Attribute =
        (
            from keyType in Keyword.IsPartOfKey.Optional()
            from label in Grammar.Textual
            from dataType in DataType.Optional()
            from isNullable in Keyword.IsNullable.Optional()
            from isMultiValued in Keyword.IsMultiValued.Optional()
            from isDerivated in Keyword.IsDerived.Optional()
            select new Attribute() { Label = label
                , DataType=dataType.GetOrDefault()
                , IsNullable=isNullable.IsDefined
                , Key = keyType.IsDefined ? keyType.Get() : KeyType.None
                , IsMultiValued = isMultiValued.IsDefined
                , IsDerived = isDerivated.IsDefined
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

        public readonly static Parser<IEnumerable<Attribute>> Attributes =
        (
            Attribute.Many()
        );
    }
}