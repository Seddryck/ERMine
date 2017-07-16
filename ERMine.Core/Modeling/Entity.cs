using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class Entity : IEquatable<Entity>, IEntityRelationship
    {
        public string Label { get; private set; }
        public virtual Key Key
        {
            get
            {
                return new PrimaryKey(Attributes.Where(a => a.IsPartOfPrimaryKey));
            }
        }
        public IList<Attribute> SpecificAttributes { get; private set; }

        public IList<IsaRelationship> IsA { get; private set; }

        internal Entity(string label)
        {
            Label = label;
            SpecificAttributes = new List<Attribute>();
            IsA = new List<IsaRelationship>();
        }

        internal Entity(string label, IEnumerable<Attribute> attributes)
        {
            Label = label;
            SpecificAttributes = attributes.ToList();
            IsA = new List<IsaRelationship>();
        }

        public virtual bool IsWeak
        {
            get { return false; }
        }

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                return SpecificAttributes.Union
                    (
                        IsA.SelectMany(i => i.SuperClass.Attributes)
                    );
            }
        }
        
        #region IEquatable

        public bool Equals(Entity other)
        {
            if (other == null)
                return false;

            return this.Label == other.Label;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Entity entityObj = obj as Entity;
            if (entityObj == null)
                return false;
            else
                return Equals(entityObj);
        }

        public override int GetHashCode()
        {
            return this.Label.GetHashCode();
        }

        public static bool operator ==(Entity entity1, Entity entity2)
        {
            if (((object)entity1) == null || ((object)entity2) == null)
                return Object.Equals(entity1, entity2);

            return entity1.Equals(entity2);
        }

        public static bool operator !=(Entity entity1, Entity entity2)
        {
            if (((object)entity1) == null || ((object)entity2) == null)
                return !Object.Equals(entity1, entity2);

            return !(entity1.Equals(entity2));
        }

        #endregion
    }
}
