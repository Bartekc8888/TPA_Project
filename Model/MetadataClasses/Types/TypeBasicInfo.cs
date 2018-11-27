using System;
using System.Collections.Generic;
using System.Linq;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types
{
    public class TypeBasicInfo
    {
        public string TypeName { get; private set; }
        public string NamespaceName { get; private set; }
        public IEnumerable<TypeBasicInfo> GenericArguments { get; private set; }
        public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> Modifiers { get; private set; }
        public IEnumerable<Attribute> Attributes { get; private set; }
        public Type InfoType { get; private set; }

        public TypeBasicInfo(Type type)
        {
            TypeName = type.Name;
            NamespaceName = type.Namespace;
            GenericArguments = !type.IsGenericTypeDefinition && !type.IsConstructedGenericType ? null : EmitGenericArguments(type.GetGenericArguments());
            Modifiers = EmitModifiers(type);
            Attributes = type.GetCustomAttributes(false).Cast<Attribute>();

            InfoType = type;
        }

        internal static TypeBasicInfo EmitReference(Type type)
        {
            return new TypeBasicInfo(type);
        }

        internal static IEnumerable<TypeBasicInfo> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }

        public static TypeBasicInfo EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;

            return EmitReference(declaringType);
        }

        public static Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevelEnum _access = AccessLevelEnum.Private;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            SealedEnum _sealed = SealedEnum.NotSealed;

            // check if not default 
            if (type.IsPublic || type.IsNestedPublic)
                _access = AccessLevelEnum.Public;
            else if (type.IsNestedPrivate)
                _access = AccessLevelEnum.Private;
            else if (type.IsNestedFamily)
                _access = AccessLevelEnum.Protected;
            else if (type.IsNotPublic || type.IsNestedAssembly)
                _access = AccessLevelEnum.Internal;
            else if (type.IsNestedFamORAssem || type.IsNestedFamORAssem)
                _access = AccessLevelEnum.ProtectedInternal;

            if (type.IsSealed)
                _sealed = SealedEnum.Sealed;

            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;

            return new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }
    }
}
