using ERMine.Core.Modeling;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Parsing
{
    class Keyword
    {
        public static readonly Parser<KeyType> IsPartOfKey = Parse.String("*").Text().Or(Parse.String("*").Text().Token().Or(Parse.String("PK").Text())).Return(KeyType.Primary)
                                                                        .Or(Parse.String("~").Text().Or(Parse.String("~").Text().Token().Or(Parse.String("PPK").Text())).Return(KeyType.Partial));
        public static readonly Parser<string> IsNullable = Parse.String("?").Text().Or(Parse.String("?").Text().Token().Or(Parse.String("NULL").Text().Token()));
        public static readonly Parser<string> IsSparse = Parse.String("??").Text().Or(Parse.String("??").Text().Token().Or(Parse.String("SPARSE").Text().Token()));
        public static readonly Parser<string> IsMultiValued = Parse.String("#").Text().Or(Parse.String("#").Text().Token().Or(Parse.String("MV").Text()));
        public static readonly Parser<string> IsDerived = Parse.String("%").Text().Or(Parse.String("%").Text().Token().Or(Parse.String("CALC").Text()));
        public static readonly Parser<string> IsImmutable = Parse.String("^").Text().Or(Parse.String("^").Text().Token().Or(Parse.String("IMMUTABLE").Text()));

        public readonly static Parser<Cardinality> Cardinality =
        (
            from cardinality in Parse.Char('*').Return(Modeling.Cardinality.ZeroOrMore)
                                .Or(Parse.Char('?').Return(Modeling.Cardinality.ZeroOrOne)
                                .Or(Parse.Char('1').Return(Modeling.Cardinality.ExactyOne)
                                .Or(Parse.Char('+').Return(Modeling.Cardinality.OneOrMore)
                                )))
            select cardinality
        );
    }
}
