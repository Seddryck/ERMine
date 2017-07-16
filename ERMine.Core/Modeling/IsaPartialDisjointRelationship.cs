using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class IsaPartialDisjointRelationship: IsaRelationship
    {
        public IsaPartialDisjointRelationship(Entity super, Entity sub)
            : base(super, sub)
        {
            
        }

        public IsaPartialDisjointRelationship(Entity super, Entity sub, string label)
            : this(super, sub)
        {
            Label = label;
        }

        public override DisjointnessType Disjointness
        {
            get { return DisjointnessType.Disjoint; }
        }
    }
}
