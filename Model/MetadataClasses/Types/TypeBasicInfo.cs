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

        internal TypeBasicInfo(Type type)
        {
            m_typeName = type.Name;
            m_NamespaceName = type.Namespace;
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type.GetGenericArguments());
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
    }
}
