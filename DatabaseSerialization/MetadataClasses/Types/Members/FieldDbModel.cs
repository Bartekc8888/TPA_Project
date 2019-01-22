using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    public class FieldDbModel : MemberAbstractDbModel
    {
        public TypeDbModel TypeModel { get; set; }
        
        public FieldDbModel(FieldModel model) : base(model)
        {
            TypeModel = TypeDbModel.EmitTypeDbModel(model.TypeModel);
        }
        
        public FieldModel ToModel()
        {
            FieldModel parameterModel = new FieldModel();
            parameterModel.TypeModel = TypeModel.ToModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static FieldDbModel EmitUniqueType(FieldModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new FieldDbModel(propertyModel));
        }
    }
}
