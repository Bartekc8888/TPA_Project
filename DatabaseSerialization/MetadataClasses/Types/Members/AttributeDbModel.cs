using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    public class AttributeDbModel : MemberAbstractDbModel
    {
        public AttributeDbModel(AttributeModel model) : base(model)
        {
        }
        
        public AttributeModel ToModel()
        {
            AttributeModel parameterModel = new AttributeModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static AttributeDbModel EmitUniqueType(AttributeModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new AttributeDbModel(propertyModel));
        }
    }
}
