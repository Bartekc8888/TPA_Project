using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    public abstract class MemberAbstractDbModel
    {
        public string Name { get; set; }
        public string TypeName { get; set; }

        public MemberAbstractDbModel()
        {
            
        }
        
        public MemberAbstractDbModel(MemberAbstract member)
        {
            Name = member.Name;
            TypeName = member.TypeName;
        }

        public void FillModel(MemberAbstract constructedObject)
        {
            constructedObject.Name = Name;
            constructedObject.TypeName = TypeName;
        }

        protected bool Equals(MemberAbstractDbModel other)
        {
            return string.Equals(Name, other.Name) && Equals(TypeName, other.TypeName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MemberAbstractDbModel) obj);
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
