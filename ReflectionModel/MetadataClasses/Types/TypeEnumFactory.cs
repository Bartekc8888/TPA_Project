using System;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types
{
    public static class TypeEnumFactory
    {
        public static TypeTypesEnumMetadata CreateTypeMetadataClass(Type type)
        {
            return type == null ? TypeTypesEnumMetadata.Unknown :
                    type.IsEnum ? TypeTypesEnumMetadata.Enum :
                    type.IsPrimitive ? TypeTypesEnumMetadata.Primitive :
                    type.IsValueType ? TypeTypesEnumMetadata.Structure :
                    type.IsArray ? TypeTypesEnumMetadata.Array :
                    type.IsInterface ? TypeTypesEnumMetadata.Interface :
                    (type.IsSubclassOf(typeof(Delegate)) || type == typeof(Delegate)) ? TypeTypesEnumMetadata.Delegate :
                    type.IsClass ? TypeTypesEnumMetadata.Class : TypeTypesEnumMetadata.Unknown;
        }
    }
}
