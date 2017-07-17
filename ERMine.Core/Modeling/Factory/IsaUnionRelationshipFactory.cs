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
            var super = new Entity(firstEntity);
            var sub = new Entity(secondEntity);

            return new IsaRelationship(super, sub, (new Guid()).ToString(), isPartial);
        }


        public IsaUnionRelationship Create(string firstEntity, string secondEntity, bool isPartial, char type, string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                groupName = (new Guid()).ToString();

            var super = new Entity(firstEntity);
            var sub = new Entity(secondEntity);
            switch (type)
            {
                case 'd':
                    return
                        isPartial ?
                        (IsaUnionRelationship) new IsaPartialDisjointRelationship(super, sub, groupName) :
                        (IsaUnionRelationship) new IsaTotalDisjointRelationship(super, sub, groupName);
                case 'o':
                    return
                        isPartial ?
                        (IsaUnionRelationship)new IsaPartialOverlappingRelationship(super, sub, groupName) :
                        (IsaUnionRelationship)new IsaTotalOverlappingRelationship(super, sub, groupName);
                case 'u':
                    return
                        isPartial ?
                        (IsaUnionRelationship)new UnionPartialRelationship(super, sub, groupName) :
                        (IsaUnionRelationship)new UnionTotalRelationship(super, sub, groupName);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
