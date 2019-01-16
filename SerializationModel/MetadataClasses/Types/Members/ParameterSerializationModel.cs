using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class ParameterSerializationModel : MemberAbstractSerializationModel
    {
        public ParameterSerializationModel(ParameterModel model) : base(model)
        {
        }

        public ParameterModel ToModel()
        {
            ParameterModel parameterModel = new ParameterModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static ParameterSerializationModel EmitUniqueType(ParameterModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ParameterSerializationModel(propertyModel));
        }
    }
}