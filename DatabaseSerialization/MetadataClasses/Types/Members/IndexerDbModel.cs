using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    public class IndexerDbModel : MemberAbstractDbModel
    {
        public IndexerDbModel(IndexerModel model) : base(model)
        {
        }
        
        public IndexerModel ToModel()
        {
            IndexerModel parameterModel = new IndexerModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static IndexerDbModel EmitUniqueType(IndexerModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new IndexerDbModel(propertyModel));
        }
    }
}
