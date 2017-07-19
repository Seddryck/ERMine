﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class UnionTotalRelationship: UnionRelationship
    {
        public UnionTotalRelationship(Entity super, Entity sub)
            : base (new[] {super},  sub, string.Empty)
        {
        }

        public UnionTotalRelationship(Entity super, Entity sub, string label)
            : base(new[] { super }, sub, label)
        {
        }

        public UnionTotalRelationship(IEnumerable<Entity> supers, Entity sub, string label)
            : base(supers, sub, label)
        { }

        public override CompletenessType Completeness
        {
            get { return CompletenessType.Total; }
        }
    }
}
