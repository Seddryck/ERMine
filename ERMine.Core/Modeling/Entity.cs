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
        public Key Key { get; protected set; }
        public IList<Attribute> Attributes { get; private set; }

        internal Entity(string label)
        {
            Label = label;
            Attributes = new List<Attribute>();
        }

        internal Entity(string label, IEnumerable<Attribute> attributes)
        {
            Label = label;
            Attributes = attributes.ToList();
            BuildKey(attributes);
        }

        protected virtual void BuildKey(IEnumerable<Attribute> attributes)
        {
            Key = new PrimaryKey(attributes.Where(a => a.IsPartOfPrimaryKey));
        }

        public virtual bool IsWeak
        {
            get { return false; }
        }

        public void Define(IEnumerable<Attribute> attributes)
        {
            if (Attributes == null || Attributes.Count==0)
                Attributes = attributes.ToList();
            else
                throw new InvalidOperationException();
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
