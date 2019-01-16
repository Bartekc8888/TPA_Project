using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class AttributeSerializationModel : MemberAbstractSerializationModel
    {
        public AttributeSerializationModel(AttributeModel model) : base(model)
        {
        }
        
        public AttributeModel ToModel()
        {
            AttributeModel parameterModel = new AttributeModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static AttributeSerializationModel EmitUniqueType(AttributeModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new AttributeSerializationModel(propertyModel));
        }
    }
}
