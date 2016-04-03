using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling.Repository
{
    public class ModelRepository
    {
        private readonly Model model = new Model();

        public Model Get()
        {
            return model;
        }

        public Entity MergeEntity(Entity entity)
        {
            if (!model.Entities.Contains(entity))
            {
                model.Entities.Add(entity);
                return entity;
            }
                

            var existingEntity = model.Entities.FirstOrDefault(e => e.Label == entity.Label);
            if (existingEntity.Attributes == null || existingEntity.Attributes.Count==0)
            {
                existingEntity.Define(entity.Attributes);
                return existingEntity;
            }

            if (entity.Attributes == null || entity.Attributes.Count == 0)
                return existingEntity;

            throw new InvalidOperationException();
        }

        public Relationship MergeRelationship(Relationship relationship)
        {
            var newRelationship = new Relationship(relationship.Entities.Count());
            newRelationship.Label = relationship.Label;
            foreach (var member in relationship.Members)
            {
                var entity = MergeEntity(member.Item1);
                newRelationship.Add(entity, member.Item2);
            }

            model.Relationships.Add(newRelationship);
            return newRelationship;
        }

        public void Merge(IEntityRelationship entityRelationship)
        {
            if (entityRelationship is Entity)
                MergeEntity(entityRelationship as Entity);
            else if (entityRelationship is Relationship)
                MergeRelationship(entityRelationship as Relationship);
            else
                throw new ArgumentOutOfRangeException();
        }

        public void Merge(IEnumerable<IEntityRelationship> entityRelationships)
        {
            foreach (var item in entityRelationships)
                Merge(item);
        }
    }
}
