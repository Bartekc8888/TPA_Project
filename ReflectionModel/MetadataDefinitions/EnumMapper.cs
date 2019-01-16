using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionModel.MetadataDefinitions
{
    public class EnumMapper
    {
        public static T1 ConvertEnum<T1, T2>(T2 enumToConvert)
        {
            T1 newEnum = (T1)Enum.Parse(typeof(T2), enumToConvert.ToString());
            return newEnum;
        }
    }
}
