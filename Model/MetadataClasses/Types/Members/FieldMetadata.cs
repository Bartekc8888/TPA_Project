using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses.Types.Members
{
    public class FieldMetadata : MemberAbstract
    {
        internal static IEnumerable<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fieldsInfo)
        {
            return from info in fieldsInfo
                   select new FieldMetadata(info.Name, info.FieldType);
        }

        private FieldMetadata(string propertyName, Type type) : base(propertyName, TypeMetadata.EmitReference(type))
        {
        }

        public FieldMetadata() :base(){ }
    }
}
