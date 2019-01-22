using System.ComponentModel.DataAnnotations.Schema;
using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    [Table("Constructor")]
    public class ConstructorDbModel : MethodDbModel
    {
        public ConstructorDbModel(ConstructorModel model) : base(model)
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

        public static ConstructorDbModel EmitUniqueType(ConstructorModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ConstructorDbModel(propertyModel));
        }
    }
}
