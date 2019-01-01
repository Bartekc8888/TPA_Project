using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class FieldSerializationModel : MemberAbstractSerializationModel
    {
        public FieldSerializationModel(FieldMetadata metadata) : base(metadata)
        {
        }
        
        public FieldMetadata ToModel()
        {
            FieldMetadata parameterMetadata = new FieldMetadata();
            FillModel(parameterMetadata);
            return parameterMetadata;
        }
    }
}
