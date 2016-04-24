using ERMine.Core.Modeling.Factory;
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
        protected Entity CurrentEntity = null;

        public Model Get()
        {
            return model;
        }

        public Entity MergeEntity(Entity entity)
        {
            if (!model.Entities.Contains(entity))
            {
                model.Entities.Add(entity);
                CurrentEntity = entity;
                return entity;
            }
                

            var existingEntity = model.Entities.FirstOrDefault(e => e.Label == entity.Label);
            if (existingEntity==null)
                throw new InvalidOperationException();

            CurrentEntity = existingEntity;
            return existingEntity;
        }

        public Relationship MergeRelationship(Relationship relationship)
        {
            CurrentEntity = null;

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

        public Entity MergeAttribute(Attribute attribute)   
        {
            if (CurrentEntity == null)
                throw new InvalidOperationException(String.Format("Can't determine the entity that the attribute '{0}' belongs to.", attribute.Label));

            var factory = new EntityFactory();

            var newEntity = factory.Create(CurrentEntity, attribute);
            CurrentEntity = newEntity;

            var existingEntity = model.Entities.Single(e => e.Label == CurrentEntity.Label);
            model.Entities.Remove(existingEntity);
            model.Entities.Add(CurrentEntity);

            return CurrentEntity;
        }

        public void Merge(IEntityRelationship entityRelationship)
        {
            if (entityRelationship is Entity)
                MergeEntity(entityRelationship as Entity);
            else if (entityRelationship is Relationship)
                MergeRelationship(entityRelationship as Relationship);
            else if (entityRelationship is Attribute)
                MergeAttribute(entityRelationship as Attribute);
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
