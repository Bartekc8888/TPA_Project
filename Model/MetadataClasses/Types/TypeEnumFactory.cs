using System;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types
{
    public static class TypeEnumFactory
    {
        public static TypeTypesEnum CreateTypeMetadataClass(Type type)
        {
            return type == null ? TypeTypesEnum.Unknown :
                    type.IsEnum ? TypeTypesEnum.Enum :
                    type.IsPrimitive ? TypeTypesEnum.Primitive :
                    type.IsValueType ? TypeTypesEnum.Structure :
                    type.IsArray ? TypeTypesEnum.Array :
                    type.IsInterface ? TypeTypesEnum.Interface :
                    (type.IsSubclassOf(typeof(Delegate)) || type == typeof(Delegate)) ? TypeTypesEnum.Delegate :
                    type.IsClass ? TypeTypesEnum.Class : TypeTypesEnum.Unknown;
        }
    }
}
