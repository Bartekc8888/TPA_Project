using System;

namespace Model.MetadataClasses.Types.ReferenceTypes
{
    internal class ReferenceMetadata : TypeMetadata
    {
        private TypeMetadata m_BaseType;

        internal ReferenceMetadata(Type type) : base(type)
        {
            m_BaseType = EmitExtends(type.BaseType);
        }

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;

            return EmitReference(baseType);
        }
    }
}
