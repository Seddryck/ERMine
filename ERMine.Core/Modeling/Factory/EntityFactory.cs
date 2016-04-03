using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling.Factory
{
    public class EntityFactory
    {
        public Entity Create(string label, IEnumerable<Attribute> attributes)
        {
            return new Entity(label, attributes);
        }
    }
}
