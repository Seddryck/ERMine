using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class PartialKey : Key
    {
        public PartialKey(IEnumerable<Attribute> attributes)
            : base(attributes) { }
    }
}
