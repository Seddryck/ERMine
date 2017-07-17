using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class UnionRelationship: IsaUnionRelationship
    {
        public UnionRelationship(Entity super, Entity sub)
            : this (new[] {super},  sub, string.Empty)
        {
        }
        
        public UnionRelationship(IEnumerable<Entity> super, Entity sub, string label)
            : base(label)
        {
            SuperClasses = new List<Entity>(super);
            SubClass = sub;
        }

        public IList<Entity> SuperClasses { get; internal set; }

        public Entity SubClass { get; internal set; }

        public override bool Equals(object obj)
        {
            var item = obj as UnionRelationship;

            if (item == null)
                return false;

            return
                this.SubClass.Equals(item.SubClass)
                && !string.IsNullOrEmpty(this.Label)
                && !string.IsNullOrEmpty(item.Label)
                && this.Label.Equals(item.Label);
        }

        public override int GetHashCode()
        {
            return Label.GetHashCode()^ SubClass.GetHashCode();
        }

        public static bool operator ==(UnionRelationship union1, UnionRelationship union2)
        {
            if (((object)union1) == null || ((object)union2) == null)
                return Object.Equals(union1, union2);

            return union1.Equals(union2);
        }

        public static bool operator !=(UnionRelationship union1, UnionRelationship union2)
        {
            if (((object)union1) == null || ((object)union2) == null)
                return !Object.Equals(union1, union2);

            return !(union1.Equals(union2));
        }
    }
}
