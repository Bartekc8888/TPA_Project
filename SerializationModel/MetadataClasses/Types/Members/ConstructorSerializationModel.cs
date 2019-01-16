using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class ConstructorSerializationModel : MethodSerializationModel
    {
        public ConstructorSerializationModel(ConstructorModel model) : base(model)
        {
        }
        
        public ConstructorModel ToModel()
        {
            MethodModel methodModel = base.ToModel();
            ConstructorModel constructorModel = new ConstructorModel();
            constructorModel.Extension = methodModel.Extension;
            constructorModel.GenericArguments = methodModel.GenericArguments;
            constructorModel.Modifiers = methodModel.Modifiers;
            constructorModel.Name = methodModel.Name;
            constructorModel.Parameters = methodModel.Parameters;
            constructorModel.ReturnType = methodModel.ReturnType;
            return constructorModel;
        }

        public static ConstructorSerializationModel EmitUniqueType(ConstructorModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ConstructorSerializationModel(propertyModel));
        }
    }
}
