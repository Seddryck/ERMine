using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class IsaRelationship: IEntityRelationship
    {
        public IsaRelationship(Entity super, Entity sub)
        {
            SuperClass = super;
            SubClass = sub;
        }

        public Entity SuperClass { get; private set; }
        public Entity SubClass { get; private set; }

        public string Label { get; protected set; }
    }
}
