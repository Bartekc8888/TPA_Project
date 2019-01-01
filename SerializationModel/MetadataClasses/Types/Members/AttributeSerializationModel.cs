using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class AttributeSerializationModel : MemberAbstractSerializationModel
    {
        public AttributeSerializationModel(AttributeMetadata metadata) : base(metadata)
        {
        }
        
        public AttributeMetadata ToModel()
        {
            AttributeMetadata parameterMetadata = new AttributeMetadata();
            FillModel(parameterMetadata);
            return parameterMetadata;
        }

        public static AttributeSerializationModel EmitUniqueType(AttributeMetadata metadata)
        {
            return UniqueEmitter.EmitType(metadata, propertyMetadata => new AttributeSerializationModel(propertyMetadata));
        }
    }
}
