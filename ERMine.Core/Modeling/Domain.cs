using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class Domain : IEquatable<Domain>, IEntityRelationship
    {
        public string Label { get; private set; }
        public IReadOnlyList<string> Values { get; protected set; }
        
        internal Domain(string label)
        {
            Label = label;
            Values = new List<string>();
        }

        internal Domain(string label, IEnumerable<string> values)
        {
            Label = label;
            Values = new List<string>(values);
        }
                
        #region IEquatable

        public bool Equals(Domain other)
        {
            if (other == null)
                return false;

            return this.Label == other.Label;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var entityObj = obj as Domain;
            if (entityObj == null)
                return false;
            else
                return Equals(entityObj);
        }

        public override int GetHashCode()
        {
            return this.Label.GetHashCode();
        }

        public static bool operator ==(Domain domain1, Domain domain2)
        {
            if (((object)domain1) == null || ((object)domain2) == null)
                return Object.Equals(domain1, domain2);

            return domain1.Equals(domain2);
        }

        public static bool operator !=(Domain domain1, Domain domain2)
        {
            if (((object)domain1) == null || ((object)domain2) == null)
                return !Object.Equals(domain1, domain2);

            return !(domain1.Equals(domain2));
        }

        #endregion
    }
}
