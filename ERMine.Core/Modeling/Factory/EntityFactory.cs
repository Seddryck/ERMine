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
            if (attributes.Any(a => a.IsPartOfPrimaryKey))
                return new Entity(label, attributes);
            else if (attributes.Any(a => a.IsPartOfPartialKey))
                return new WeakEntity(label, attributes);
            else
                return new Entity(label, attributes);
        }

        public Entity Create(Entity entity, Attribute attribute)
        {
            var attributes = new List<Attribute>(entity.SpecificAttributes);
            attributes.Add(attribute);

            if (attributes.Any(a => a.IsPartOfPrimaryKey))
                return new Entity(entity.Label, attributes);
            else if (attributes.Any(a => a.IsPartOfPartialKey))
                return new WeakEntity(entity.Label, attributes);
            else
                return new Entity(entity.Label, attributes);
        }

        public Entity Create(string label)
        {
            return new Entity(label);
        }
    }
}
