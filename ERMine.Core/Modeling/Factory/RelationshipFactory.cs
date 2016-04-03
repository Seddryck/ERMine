using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling.Factory
{
    public class RelationshipFactory
    {
        public Relationship Create(string label, IEnumerable<Tuple<Entity, Cardinality>> members)
        {
            var relationship = new Relationship(members.Count());
            relationship.Label = label;
            foreach (var item in members)
                relationship.Add(item.Item1, item.Item2);
            return relationship;
        }

        public Relationship Create(string label, string firstEntity, Cardinality firstCardinality, string secondEntity, Cardinality secondCardinality)
        {
            var tuples = new List<Tuple<Entity, Cardinality>>();

            var first = new Entity(firstEntity);
            tuples.Add(new Tuple<Entity, Cardinality>(first, firstCardinality));

            var second = new Entity(secondEntity);
            tuples.Add(new Tuple<Entity, Cardinality>(second, secondCardinality));

            return Create(label, tuples);
        }
    }
}
