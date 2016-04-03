using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class Model
    {
        public IList<Entity> Entities { get; internal set; }
        public IList<Relationship> Relationships { get; internal set; }

        public Model()
        {
            Entities = new List<Entity>();
            Relationships = new List<Relationship>();
        }
    }
}
