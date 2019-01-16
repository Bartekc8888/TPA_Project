using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class FieldSerializationModel : MemberAbstractSerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public TypeSerializationModel TypeModel { get; set; }
        
        public FieldSerializationModel(FieldModel model) : base(model)
        {
            TypeModel = TypeSerializationModel.EmitTypeSerializationModel(model.TypeModel);
        }
        
        public FieldModel ToModel()
        {
            FieldModel parameterModel = new FieldModel();
            parameterModel.TypeModel = TypeModel.ToModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static FieldSerializationModel EmitUniqueType(FieldModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new FieldSerializationModel(propertyModel));
        }
    }
}
