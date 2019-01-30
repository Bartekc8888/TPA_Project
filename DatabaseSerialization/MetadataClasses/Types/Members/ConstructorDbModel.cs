using System.ComponentModel.DataAnnotations.Schema;
using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    [Table("Constructor")]
    public class ConstructorDbModel : MethodDbModel
    {
        public new int Id { get; set; }
        
        public ConstructorDbModel()
        {
            
        }
        
        public ConstructorDbModel(ConstructorModel model) : base(model)
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

        public static ConstructorDbModel EmitUniqueType(ConstructorModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ConstructorDbModel(propertyModel));
        }
    }
}
