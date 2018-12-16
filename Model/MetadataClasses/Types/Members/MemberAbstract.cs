using System.Runtime.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public abstract class MemberAbstract
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public TypeMetadata TypeMetadata { get; set; }

        public MemberAbstract(string name, TypeMetadata typeInfo)
        {
            this.Name = name;
            this.TypeMetadata = typeInfo;
        }

        public MemberAbstract(string name)
        {
            this.Name = name;
            this.TypeMetadata = null;
        }

        public MemberAbstract() { }

        protected bool Equals(MemberAbstract other)
        {
            return string.Equals(Name, other.Name) && Equals(TypeMetadata, other.TypeMetadata);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MemberAbstract) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (TypeMetadata != null ? TypeMetadata.GetHashCode() : 0);
            }
        }
    }
}
