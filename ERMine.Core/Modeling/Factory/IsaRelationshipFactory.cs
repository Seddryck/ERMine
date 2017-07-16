using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling.Factory
{
    public class IsaRelationshipFactory
    {
        
        public IsaRelationship Create(string firstEntity, string secondEntity)
        {
            var super = new Entity(firstEntity);
            var sub = new Entity(secondEntity);
            
            return new IsaRelationship(super, sub);
        }

        public IsaRelationship Create(string firstEntity, string secondEntity, bool isPartial)
        {
            var super = new Entity(firstEntity);
            var sub = new Entity(secondEntity);

            return new IsaRelationship(super, sub, string.Empty, isPartial);
        }


        public IsaRelationship Create(string firstEntity, string secondEntity, bool isPartial, char type, string groupName)
        {
            var super = new Entity(firstEntity);
            var sub = new Entity(secondEntity);
            switch (type)
            {
                case 'd':
                    return
                        isPartial ?
                        (IsaRelationship) new IsaPartialDisjointRelationship(super, sub, groupName) :
                        (IsaRelationship) new IsaTotalDisjointRelationship(super, sub, groupName);
                case 'o':
                    return
                        isPartial ?
                        (IsaRelationship)new IsaPartialOverlappingRelationship(super, sub, groupName) :
                        (IsaRelationship)new IsaTotalOverlappingRelationship(super, sub, groupName);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
