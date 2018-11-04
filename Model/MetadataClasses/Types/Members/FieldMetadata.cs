using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses.Types.Members
{
    public class FieldMetadata
    {
        public string Name { get; private set; }
        public TypeBasicInfo TypeMetadata { get; private set; }

        internal static IEnumerable<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fieldsInfo)
        {
            return from info in fieldsInfo
                   select new FieldMetadata(info.Name, info.FieldType);
        }

        private FieldMetadata(string propertyName, Type type)
        {
            Name = propertyName;
            TypeMetadata = TypeBasicInfo.EmitReference(type);
        }
    }
}
