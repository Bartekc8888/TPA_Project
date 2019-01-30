using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using Serialization.MetadataExtensions;

namespace Serialization.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class ConstructorSerializationModel : MethodSerializationModel
    {
        public ConstructorSerializationModel(ConstructorModel model) : base(model)
        {
        }
        
        public new ConstructorModel ToModel()
        {
            MethodModel methodModel = base.ToModel();
            ConstructorModel constructorModel = new ConstructorModel
            {
                Extension = methodModel.Extension,
                GenericArguments = methodModel.GenericArguments,
                Modifiers = methodModel.Modifiers,
                Name = methodModel.Name,
                Parameters = methodModel.Parameters,
                ReturnType = methodModel.ReturnType
            };
            return constructorModel;
        }

        public static ConstructorSerializationModel EmitUniqueType(ConstructorModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ConstructorSerializationModel(propertyModel));
        }
    }
}
