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
        protected IEntityRelationship CurrentObject = null;

        public Model Get()
        {
            return model;
        }

        public void MergeEntity(Entity entity)
        {
            if (!model.Entities.Contains(entity))
                model.Entities.Add(entity);
            else
            {
                var existingEntity = model.Entities.Single(e => e.Label == entity.Label);
                if (existingEntity.Attributes.Count==0)
                {
                    model.Entities.Remove(existingEntity);
                    model.Entities.Add(entity);
                }
            }
        }

        public void MergeRelationship(Relationship relationship)
        {
            CurrentObject = null;

            var newRelationship = new Relationship(relationship.Entities.Count());
            newRelationship.Label = relationship.Label;
            foreach (var member in relationship.Members)
            {
                MergeEntity(member.Item1);
                newRelationship.Add(member.Item1, member.Item2);
            }

            model.Relationships.Add(newRelationship);
        }


        public void MergeDomain(Domain domain)
        {
            if (!model.Domains.Contains(domain))
                model.Domains.Add(domain);
            else
            {
                var existingDomain = model.Domains.Single(d => d.Label == domain.Label);
                if (existingDomain.Values.Count == 0)
                {
                    model.Domains.Remove(existingDomain);
                    model.Domains.Add(domain);
                }
            }
        }

        public void Merge(IEntityRelationship entityRelationship)
        {
            if (entityRelationship is Entity)
                MergeEntity(entityRelationship as Entity);
            else if (entityRelationship is Relationship)
                MergeRelationship(entityRelationship as Relationship);
            else if (entityRelationship is Domain)
                MergeDomain(entityRelationship as Domain);
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
