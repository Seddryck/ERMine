using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class UnionPartialRelationship : UnionRelationship
    {
        public UnionPartialRelationship(Entity super, Entity sub)
            : this(super, sub, string.Empty)
        {
        }

        public UnionPartialRelationship(Entity super, Entity sub, string label)
            : base(new[] { super }, sub, label)
        {
        }

        public UnionPartialRelationship(IEnumerable<Entity> supers, Entity sub, string label)
            : base(supers, sub, label)
        { }
    }
}
