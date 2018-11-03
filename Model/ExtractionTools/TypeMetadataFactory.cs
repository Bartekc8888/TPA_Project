using Model.MetadataClasses;
using Model.MetadataClasses.Types.ReferenceTypes;
using Model.MetadataClasses.Types.ValueTypes;
using System;

namespace Model.ExtractionTools
{
    public static class TypeMetadataFactory
    {
        public static TypeMetadata CreateTypeMetadataClass(Type type)
        {
            return type.IsEnum ? new EnumMetadata(type) :
                    type.IsPrimitive ? new PrimitiveMetadata(type) :
                    type.IsValueType ? new StructureMetadata(type) :
                    type.IsArray ? new ArrayMetadata(type) :
                    type.IsInterface ? new InterfaceMetadata(type) :
                    (type.IsSubclassOf(typeof(Delegate)) || type == typeof(Delegate)) ? new DelegateMetadata(type) :
                    type.IsClass ? new ClassMetadata(type) :
                    new TypeMetadata(type);
        }
    }
}
