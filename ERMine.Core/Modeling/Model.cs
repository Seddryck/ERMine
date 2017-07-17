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
        public IList<IsaRelationship> IsaRelationships { get; internal set; }
        public IList<UnionRelationship> UnionRelationships { get; internal set; }
        public IList<Domain> Domains { get; internal set; }

        public Model()
        {
            Entities = new List<Entity>();
            Relationships = new List<Relationship>();
            IsaRelationships = new List<IsaRelationship>();
            UnionRelationships = new List<UnionRelationship>();
            Domains = new List<Domain>();
        }
    }
}
