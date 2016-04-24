using System.Collections.Generic;
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
        public readonly static Parser<Entity> Entity =
        (
            from label in Grammar.AsmToken(Grammar.BracketTextual)
            select new EntityFactory().Create(label)
        );
    }
}
