using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses.Types.Members
{
    internal class FieldMetadata
    {
        private string m_Name;
        private TypeBasicInfo m_TypeMetadata;

        internal static IEnumerable<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fieldsInfo)
        {
            return from info in fieldsInfo
                   select new FieldMetadata(info.Name, info.FieldType);
        }

        private FieldMetadata(string propertyName, Type type)
        {
            m_Name = propertyName;
            m_TypeMetadata = TypeBasicInfo.EmitReference(type);
        }
    }
}
