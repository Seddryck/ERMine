using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class IsaRelationship: IEntityRelationship
    {
        public IsaRelationship(Entity super, Entity sub)
            : this (super, new[] { sub }, string.Empty)
        {
        }

        public IsaRelationship(Entity super, Entity sub, string label)
            : this(super, new[] { sub }, label)
        {
        }

        public IsaRelationship(Entity super, IEnumerable<Entity> sub, string label)
        {
            SuperClass = super;
            SubClasses = new List<Entity>(sub);
            Label = label;
        }

        public Entity SuperClass { get; internal set; }

        public IList<Entity> SubClasses { get; internal set; }

        public string Label { get; protected set; }

        public override bool Equals(object obj)
        {
            var item = obj as IsaRelationship;

            if (item == null)
                return false;

            return
                this.SuperClass.Equals(item.SuperClass)
                && !string.IsNullOrEmpty(this.Label)
                && !string.IsNullOrEmpty(item.Label)
                && this.Label.Equals(item.Label);
        }

        public override int GetHashCode()
        {
            return Label.GetHashCode()^ SuperClass.GetHashCode();
        }

        public static bool operator ==(IsaRelationship isa1, IsaRelationship isa2)
        {
            if (((object)isa1) == null || ((object)isa2) == null)
                return Object.Equals(isa1, isa2);

            return isa1.Equals(isa2);
        }

        public static bool operator !=(IsaRelationship isa1, IsaRelationship isa2)
        {
            if (((object)isa1) == null || ((object)isa2) == null)
                return !Object.Equals(isa1, isa2);

            return !(isa1.Equals(isa2));
        }

        public virtual string Kind
        {
            get { return "Undefined"; }
        }
    }
}
