using System;

namespace SerializationModel.MetadataDefinitions
{
    public class EnumMapper
    {
        public static T1 ConvertEnum<T1, T2>(T2 enumToConvert)
        {
            T1 newEnum = (T1) Enum.Parse(typeof(T1), enumToConvert.ToString());
            return newEnum;
        }
    }
}