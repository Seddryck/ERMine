using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public abstract class IsaUnionRelationship: IEntityRelationship
    {
        private readonly bool isPartial;
        

        public IsaUnionRelationship(string label)
        {
            Label = label;           
        }
        
        public string Label { get; protected set; }

        public virtual CompletenessType Completeness
        {
            get { return CompletenessType.Partial; }
        }
    }
}
