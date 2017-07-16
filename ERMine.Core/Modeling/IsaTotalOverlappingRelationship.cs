using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class IsaTotalOverlappingRelationship : IsaRelationship
    {
        public IsaTotalOverlappingRelationship(Entity super, Entity sub)
            : base(super, sub)
        {
            
        }
        public IsaTotalOverlappingRelationship(Entity super, Entity sub, string label)
            : this(super, sub)
        {
            Label = label;
        }

        public override DisjointnessType Disjointness
        {
            get { return DisjointnessType.Overlapping; }
        }

        public override CompletenessType Completeness
        {
            get { return CompletenessType.Total; }
        }
    }
}
