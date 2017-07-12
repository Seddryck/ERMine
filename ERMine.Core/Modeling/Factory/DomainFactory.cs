using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling.Factory
{
    public class DomainFactory
    {
        public Domain Create(string label, IEnumerable<string> values)
        {
            return new Domain(label, values);
        }

        public Domain Create(string label, DomainValue value)
        {
            return new Domain(label, Enumerable.Repeat(value.Label, 1));
        }

        public Domain Create(string label, IEnumerable<DomainValue> values)
        {
            return new Domain(label, values.Select(v => v.Label));
        }

        public Domain Create(string label)
        {
            return new Domain(label, new string[0]);
        }
    }
}
