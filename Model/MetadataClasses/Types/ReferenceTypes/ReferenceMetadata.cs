using System;
using System.Collections.Generic;

namespace Model.MetadataClasses.Types.ReferenceTypes
{
    internal class ReferenceMetadata : TypeMetadata
    {
        private TypeMetadata m_BaseType;
        private IEnumerable<TypeMetadata> m_ImplementedInterfaces;

        internal ReferenceMetadata(Type type) : base(type)
        {
            m_BaseType = EmitExtends(type.BaseType);
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
        }

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;

            return EmitReference(baseType);
        }

        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
    }
}
