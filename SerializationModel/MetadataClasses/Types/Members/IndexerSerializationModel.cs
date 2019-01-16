using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class IndexerSerializationModel : MemberAbstractSerializationModel
    {
        public IndexerSerializationModel(IndexerModel model) : base(model)
        {
        }
        
        public IndexerModel ToModel()
        {
            IndexerModel parameterModel = new IndexerModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static IndexerSerializationModel EmitUniqueType(IndexerModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new IndexerSerializationModel(propertyModel));
        }
    }
}
