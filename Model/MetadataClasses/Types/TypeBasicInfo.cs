using Model.MetadataDefinitions;
using Model.MetadataExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.MetadataClasses.Types
{
    internal class TypeBasicInfo
    {
        private string m_typeName;
        private string m_NamespaceName;
        private IEnumerable<TypeBasicInfo> m_GenericArguments;
        private Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> m_Modifiers;
        private IEnumerable<Attribute> m_Attributes;

        internal TypeBasicInfo(Type type)
        {
            m_typeName = type.Name;
            m_NamespaceName = type.Namespace;
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type.GetGenericArguments());
            m_Modifiers = EmitModifiers(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();
        }

        private TypeBasicInfo(string typeName, string namespaceName)
        {
            m_typeName = typeName;
            m_NamespaceName = namespaceName;
        }

        private TypeBasicInfo(string typeName, string namespaceName, IEnumerable<TypeBasicInfo> genericArguments) : this(typeName, namespaceName)
        {
            m_GenericArguments = genericArguments;
        }

        internal static TypeBasicInfo EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeBasicInfo(type.Name, type.GetNamespace());
            else
                return new TypeBasicInfo(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
        }

        internal static IEnumerable<TypeBasicInfo> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }

        internal static TypeBasicInfo EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;

            return EmitReference(declaringType);
        }

        internal static Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevelEnum _access = AccessLevelEnum.Private;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            SealedEnum _sealed = SealedEnum.NotSealed;

            // check if not default 
            if (type.IsPublic || type.IsNestedPublic)
                _access = AccessLevelEnum.Public;
            else if (type.IsNotPublic || type.IsNestedPrivate)
                _access = AccessLevelEnum.Private;
            else if (type.IsNestedFamily)
                _access = AccessLevelEnum.Protected;
            else if (type.IsNestedAssembly)
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
