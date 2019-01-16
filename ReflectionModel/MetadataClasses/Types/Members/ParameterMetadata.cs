using ReflectionModel.MetadataExtensions;

namespace Model.MetadataClasses.Types.Members
{
    public class ParameterMetadata : MemberAbstractMetadata
    {

        public ParameterMetadata(string name, TypeMetadata typeMetadata) : base(name, typeMetadata.TypeName)
        {
        }

        public ParameterMetadata() : base() { }

        public ParameterMetadata(ParameterModel model) : base(model)
        {
        }

        public ParameterModel ToModel()
        {
            ParameterModel parameterModel = new ParameterModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static ParameterMetadata EmitUniqueType(ParameterModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ParameterMetadata(propertyModel));
        }
    }
}