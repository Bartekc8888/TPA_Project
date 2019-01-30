using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionModel.MetadataExtensions;

namespace Model.MetadataClasses.Types.Members
{
    public class ConstructorMetadata : MethodMetadata
    {
        public ConstructorMetadata(MethodBase method) : base(method)
        {
        }
        
        public ConstructorMetadata(MethodMetadata method) : base(method)
        {
        }

        public ConstructorMetadata() : base() { }

        internal static IEnumerable<ConstructorMetadata> EmitConstructors(IEnumerable<MethodBase> methods)
        {
            return from MethodBase currentMethod in methods
                   select new ConstructorMetadata(currentMethod);
        }

        public ConstructorMetadata(ConstructorModel model) : base(model)
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

        public static ConstructorMetadata EmitUniqueType(ConstructorModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ConstructorMetadata(propertyModel));
        }
    }
}
