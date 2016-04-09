using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class WeakEntity : Entity
    {
        internal WeakEntity(string label)
            : base(label) { }

        internal WeakEntity(string label, IEnumerable<Attribute> attributes)
            : base(label, attributes) { }

        protected override void BuildKey(IEnumerable<Attribute> attributes)
        {
            Key = new PartialKey(attributes.Where(a => a.IsPartOfPartialKey));
        }

        public override bool IsWeak
        {
            get { return true; }
        }
    }
}
