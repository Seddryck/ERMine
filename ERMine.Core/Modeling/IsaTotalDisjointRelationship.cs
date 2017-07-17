﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class IsaTotalDisjointRelationship: IsaRelationship
    {
        public IsaTotalDisjointRelationship(Entity super, Entity sub)
            : base(super, sub)
        {
            
        }

        public IsaTotalDisjointRelationship(Entity super, Entity sub, string label)
            : this(super, new[] { sub }, label)
        {
        }
        public IsaTotalDisjointRelationship(Entity super, IEnumerable<Entity> subs, string label)
            : base(super, subs, label)
        { }

        public override DisjointnessType Disjointness
        {
            get { return DisjointnessType.Disjoint; }
        }

        public override CompletenessType Completeness
        {
            get { return CompletenessType.Total; }
        }
    }
}
