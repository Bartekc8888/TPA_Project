using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class PropertySerializationModel : MemberAbstractSerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public MethodSerializationModel[] propertyMethods { get; set; }
       
        public PropertySerializationModel(PropertyModel model) : base(model)
        {
            propertyMethods = model.propertyMethods.Select(methodModel => new MethodSerializationModel(methodModel)).ToArray();
        }

        public PropertyModel ToModel()
        {
            PropertyModel propertyModel = new PropertyModel();
            FillModel(propertyModel);
            propertyModel.propertyMethods = propertyMethods.Select(model => model.ToModel()).ToArray();

            return propertyModel;
        }
        
        protected bool Equals(PropertySerializationModel other)
        {
            return base.Equals(other) && Equals(propertyMethods, other.propertyMethods);
        }

        public static PropertySerializationModel EmitUniqueType(PropertyModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new PropertySerializationModel(propertyModel));
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