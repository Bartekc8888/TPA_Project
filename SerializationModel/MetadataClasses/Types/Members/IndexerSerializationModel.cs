using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class IndexerSerializationModel : MemberAbstractSerializationModel
    {
        public IndexerSerializationModel(IndexerMetadata metadata) : base(metadata)
        {
        }
        
        public IndexerMetadata ToModel()
        {
            IndexerMetadata parameterMetadata = new IndexerMetadata();
            FillModel(parameterMetadata);
            return parameterMetadata;
        }
    }
}
