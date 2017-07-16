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
    }
}
