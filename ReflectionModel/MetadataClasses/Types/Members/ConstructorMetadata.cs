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

        public static ConstructorMetadata EmitUniqueType(ConstructorModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ConstructorMetadata(propertyModel));
        }
    }
}
