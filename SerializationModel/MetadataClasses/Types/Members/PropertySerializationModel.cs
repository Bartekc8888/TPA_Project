using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class PropertySerializationModel : MemberAbstractSerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public MethodSerializationModel[] propertyMethods { get; set; }
       
        public PropertySerializationModel(PropertyMetadata metadata) : base(metadata)
        {
            propertyMethods = metadata.propertyMethods.Select(methodMetadata => new MethodSerializationModel(methodMetadata)).ToArray();
        }

        public PropertyMetadata ToModel()
        {
            PropertyMetadata propertyMetadata = new PropertyMetadata();
            FillModel(propertyMetadata);
            propertyMetadata.propertyMethods = propertyMethods.Select(model => model.ToModel()).ToArray();

            return propertyMetadata;
        }

        protected bool Equals(PropertySerializationModel other)
        {
            return base.Equals(other) && Equals(propertyMethods, other.propertyMethods);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertySerializationModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (propertyMethods != null ? propertyMethods.GetHashCode() : 0);
            }
        }
    }
}