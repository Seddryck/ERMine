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
            from label in Grammar.BracketTextual
            from attributes in AttributeParser.Attributes.Optional()
            select new EntityFactory().Create(label, attributes.Get())
        );

        public readonly static Parser<IEnumerable<Entity>> Entities =
        (
            Entity.Many()
        );
    }
}
