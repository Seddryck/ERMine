using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class Attribute
    {
        public string Label { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPartOfKey { get; set; }
    }
}
