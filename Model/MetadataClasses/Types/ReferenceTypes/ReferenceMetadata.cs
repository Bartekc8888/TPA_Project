using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.MetadataClasses.Types.ReferenceTypes
{
    public class ReferenceMetadata : TypeMetadata
    {
        private TypeBasicInfo m_BaseType;
        private IEnumerable<TypeBasicInfo> m_ImplementedInterfaces;

        internal ReferenceMetadata(Type type) : base(type)
        {
            m_BaseType = EmitExtends(type.BaseType);
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
        }

        private static TypeBasicInfo EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;

            return TypeBasicInfo.EmitReference(baseType);
        }

        private IEnumerable<TypeBasicInfo> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select TypeBasicInfo.EmitReference(currentInterface);
        }
    }
}
