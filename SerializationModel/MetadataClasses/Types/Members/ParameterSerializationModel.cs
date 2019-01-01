using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class ParameterSerializationModel : MemberAbstractSerializationModel
    {
        public ParameterSerializationModel(ParameterMetadata metadata) : base(metadata)
        {
        }

        public ParameterMetadata ToModel()
        {
            ParameterMetadata parameterMetadata = new ParameterMetadata();
            FillModel(parameterMetadata);
            return parameterMetadata;
        }
    }
}