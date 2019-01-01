using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

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

        public static IndexerSerializationModel EmitUniqueType(IndexerMetadata metadata)
        {
            return UniqueEmitter.EmitType(metadata, propertyMetadata => new IndexerSerializationModel(propertyMetadata));
        }
    }
}
