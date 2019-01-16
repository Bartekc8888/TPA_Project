namespace Model.MetadataClasses.Types.Members
{
    public abstract class MemberAbstract
    {
        public string Name { get; set; }
        public string TypeName { get; set; }

        public MemberAbstract(string name, string typeName)
        {
            Name = name;
            TypeName = typeName;
        }

        public MemberAbstract(string name)
        {
            Name = name;
            TypeName = null;
        }

        public MemberAbstract() { }

        protected bool Equals(MemberAbstract other)
        {
            return string.Equals(Name, other.Name) && string.Equals(TypeName, other.TypeName);
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
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
            }
        }
    }
}
