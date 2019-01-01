namespace Model.MetadataClasses.Types.Members
{
    public abstract class MemberAbstract
    {
        public string Name { get; set; }
        public TypeMetadata TypeMetadata { get; set; }

        public MemberAbstract(string name, TypeMetadata typeInfo)
        {
            Name = name;
            TypeMetadata = typeInfo;
        }

        public MemberAbstract(string name)
        {
            Name = name;
            TypeMetadata = null;
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
            if (obj.GetType() != GetType()) return false;
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
