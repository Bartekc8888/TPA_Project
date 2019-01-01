using System.Runtime.Serialization;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public abstract class MemberAbstractSerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public TypeSerializationModel TypeMetadata { get; set; }

        public MemberAbstractSerializationModel(MemberAbstract member)
        {
            Name = member.Name;
            TypeMetadata = member.TypeMetadata == null ? null : TypeSerializationModel.EmitTypeSerializationModel(member.TypeMetadata);
        }

        public void FillModel(MemberAbstract constructedObject)
        {
            constructedObject.Name = Name;
            constructedObject.TypeMetadata = TypeMetadata == null ? null : TypeMetadata.ToModel();
        }

        protected bool Equals(MemberAbstractSerializationModel other)
        {
            return string.Equals(Name, other.Name) && Equals(TypeMetadata, other.TypeMetadata);
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
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (TypeMetadata != null ? TypeMetadata.GetHashCode() : 0);
            }
        }
    }
}
