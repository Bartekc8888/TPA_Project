using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public abstract class MemberAbstractSerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string TypeName { get; set; }

        public MemberAbstractSerializationModel(MemberAbstract member)
        {
            Name = member.Name;
            TypeName = member.TypeName;
        }

        public void FillModel(MemberAbstract constructedObject)
        {
            constructedObject.Name = Name;
            constructedObject.TypeName = TypeName;
        }

        protected bool Equals(MemberAbstractSerializationModel other)
        {
            return string.Equals(Name, other.Name) && Equals(TypeName, other.TypeName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MemberAbstractSerializationModel) obj);
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
