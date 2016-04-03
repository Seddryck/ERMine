using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Parsing
{
    class Grammar
    {
        public static readonly Parser<string> Textual = Parse.Letter.AtLeastOnce().Text().Token();
        public static readonly Parser<string> BracketTextual = Parse.CharExcept("[]").AtLeastOnce().Text().Contained(Parse.Char('['), Parse.Char(']')).Token();
        public static readonly Parser<string> CurlyBraceTextual = Parse.CharExcept("{}").AtLeastOnce().Text().Contained(Parse.Char('{'), Parse.Char('}')).Token();
        public static readonly Parser<string> QuotedTextual = Parse.CharExcept("'").AtLeastOnce().Text().Contained(Parse.Char('\''), Parse.Char('\'')).Token();
        public static readonly Parser<string> Record = Textual.Or(BracketTextual);
        public static readonly Parser<string> Line = Parse.AnyChar.Except(Parse.LineTerminator).AtLeastOnce().Text().Token();
        public static readonly Parser<IEnumerable<string>> Tabs = Parse.AnyChar.Except(Parse.Char('\t')).AtLeastOnce().Text().Token().Many();
        public static readonly Parser<IEnumerable<string>> RecordSequence = Record.DelimitedBy(Parse.Char(','));
        public static readonly Parser<IEnumerable<string>> QuotedRecordSequence = QuotedTextual.DelimitedBy(Parse.Char(','));
    }
}
