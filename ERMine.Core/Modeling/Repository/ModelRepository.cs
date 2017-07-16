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

        public Entity MergeEntity(Entity entity)
        {
            if (!model.Entities.Contains(entity))
                model.Entities.Add(entity);
            else
            {
                var existingEntity = model.Entities.Single(e => e.Equals(entity));
                var missingAttributes = entity.SpecificAttributes.Except(existingEntity.SpecificAttributes);
                foreach (var missingAttribute in missingAttributes)
                    existingEntity.SpecificAttributes.Add(missingAttribute);
            }

            foreach (var attr in entity.SpecificAttributes)
                attr.Domain = model.Domains.SingleOrDefault(d => d.Label == attr.DataType);

            return model.Entities.Single(e => e.Equals(entity));
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

            var attrs = model.Entities.SelectMany(e => e.SpecificAttributes).Where(a => a.DataType == domain.Label);
            foreach (var attr in attrs)
                attr.Domain = domain;
        }


        public void MergeIsaRelationship(IsaRelationship isaRelationship)
        {
            var superClass = MergeEntity(isaRelationship.SuperClass);
            isaRelationship.SuperClass = superClass;

            for (int i = 0; i < isaRelationship.SubClasses.Count; i++)
            {
                var subClass = MergeEntity(isaRelationship.SubClasses[i]);
                isaRelationship.SubClasses[i] = subClass;
            }   

            if (model.IsaRelationships.Contains(isaRelationship))
            {
                var existing = model.IsaRelationships.Single(i => i.Equals(isaRelationship));
                foreach (var subClass in isaRelationship.SubClasses)
                    if (!existing.SubClasses.Contains(subClass))
                        existing.SubClasses.Add(subClass);
                        
            }
            else
                model.IsaRelationships.Add(isaRelationship);

            var isa = model.IsaRelationships.SingleOrDefault(i => i.Equals(isaRelationship));
            isa = isa ?? isaRelationship; //It happens when the relationship has no name
            foreach (var subClass in isa.SubClasses)
                subClass.IsA.Add(isa);
        }

        public void Merge(IEntityRelationship entityRelationship)
        {
            if (entityRelationship is Entity)
                MergeEntity(entityRelationship as Entity);
            else if (entityRelationship is Relationship)
                MergeRelationship(entityRelationship as Relationship);
            else if (entityRelationship is Domain)
                MergeDomain(entityRelationship as Domain);
            else if (entityRelationship is IsaRelationship)
                MergeIsaRelationship(entityRelationship as IsaRelationship);
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
