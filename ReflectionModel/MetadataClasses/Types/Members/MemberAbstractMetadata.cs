namespace Model.MetadataClasses.Types.Members
{
    public abstract class MemberAbstractMetadata
    {
        public string Name { get; set; }
        public string TypeName { get; set; }

        public MemberAbstractMetadata(string name, string typeName)
        {
            Name = name;
            TypeName = typeName;
        }

        public MemberAbstractMetadata(string name)
        {
            Name = name;
            TypeName = null;
        }

        public MemberAbstractMetadata() { }

        public MemberAbstractMetadata(MemberAbstract member)
        {
            Name = member.Name;
            TypeName = member.TypeName;
        }

        public void FillModel(MemberAbstract constructedObject)
        {
            constructedObject.Name = Name;
            constructedObject.TypeName = TypeName;
        }

        protected bool Equals(MemberAbstractMetadata other)
        {
            return string.Equals(Name, other.Name) && string.Equals(TypeName, other.TypeName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MemberAbstractMetadata) obj);
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
