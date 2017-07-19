using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling.Factory
{
    public class IsaUnionRelationshipFactory
    {
        public IsaUnionRelationship Create(string firstEntity, string secondEntity, bool isPartial)
        {
            return this.Create(firstEntity, secondEntity, isPartial, 'd', string.Empty);
        }

        public IsaUnionRelationship Create(string firstEntity, string secondEntity, bool isPartial, char type, string groupName)
        {
            return this.Create(firstEntity, new[] { secondEntity }, isPartial, type, groupName);
        }


        public IsaUnionRelationship Create(string firstEntity, string[] secondEntities, bool isPartial, char type, string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                groupName = (Guid.NewGuid()).ToString();

            var super = new Entity(firstEntity);

            var listEntities = new List<Entity>();
            foreach (var secondEntity in secondEntities)
                listEntities.Add(new Entity(secondEntity));

            switch (type)
            {
                case 'd':
                    return
                        isPartial ?
                        (IsaUnionRelationship)new IsaPartialDisjointRelationship(super, listEntities, groupName) :
                        (IsaUnionRelationship)new IsaTotalDisjointRelationship(super, listEntities, groupName);
                case 'o':
                    return
                        isPartial ?
                        (IsaUnionRelationship)new IsaPartialOverlappingRelationship(super, listEntities, groupName) :
                        (IsaUnionRelationship)new IsaTotalOverlappingRelationship(super, listEntities, groupName);
                case 'u':
                    return
                        isPartial ?
                        (IsaUnionRelationship)new UnionPartialRelationship(listEntities,super , groupName) :
                        (IsaUnionRelationship)new UnionTotalRelationship(listEntities, super , groupName);
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }
    }
}
