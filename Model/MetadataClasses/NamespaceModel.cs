using System.Collections.Generic;
using Model.MetadataClasses.Types;

namespace Model.MetadataClasses
{
    public class NamespaceModel
    {
        public string NamespaceName { get; set; }
        public IEnumerable<TypeModel> Types { get; set; }

        protected bool Equals(NamespaceModel other)
        {
            return string.Equals(NamespaceName, other.NamespaceName) && Equals(Types, other.Types);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NamespaceModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((NamespaceName != null ? NamespaceName.GetHashCode() : 0) * 397) ^ (Types != null ? Types.GetHashCode() : 0);
            }
        }
    }
}