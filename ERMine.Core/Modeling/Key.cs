using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public abstract class Key
    {
        public IEnumerable<Attribute> Attributes { get; set; }

        public Key(IEnumerable<Attribute> attributes)
        {
            Attributes = attributes;
        }
    }
}
