using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;

namespace ERMine.Core.Parsing
{
    static class DomainParser
    {
        public readonly static Parser<string> Domain =
        (
            from label in Grammar.AsmToken(Grammar.AngleBracketTextual)
            select label
        );

        public readonly static Parser<DomainValue> DomainValue =
        (
            from label in Grammar.AsmToken(Grammar.Textual.Or(Grammar.QuotedTextual))
            from endDeclarartion in Grammar.Terminator
            select new DomainValue()
            {
                Label = label
            }
        );
    }
}
