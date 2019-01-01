using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;

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
    }
}
