using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class Relationship: IEntityRelationship
    {
        protected List<Entity> entities;
        protected List<Cardinality> cardinalities;
        public string Label { get; internal set; }

        internal Relationship(int count)
        {
            entities = new List<Entity>(count);
            cardinalities = new List<Cardinality>(count);
        }

        internal void Add(Entity entity, Cardinality cardinality)
        {
            entities.Add(entity);
            cardinalities.Add(cardinality);
        }

        public IEnumerable<Entity> Entities
        {
            get { return entities.Skip(0); }
        }

        public IEnumerable<Cardinality> Cardinality
        {
            get { return cardinalities.Skip(0); }
        }

        public IEnumerable<Tuple<Entity, Cardinality>> Members
        {
            get
            {
                return entities.Zip(cardinalities, (e, c) => Tuple.Create(e, c));
            }
        }

        
    }
}
