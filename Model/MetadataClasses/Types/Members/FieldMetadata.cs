﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [XmlRoot]
    public class FieldMetadata : MemberAbstract
    {
        internal static IEnumerable<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fieldsInfo)
        {
            return from info in fieldsInfo
                   select new FieldMetadata(info.Name, info.FieldType);
        }

        private FieldMetadata(string propertyName, Type type) : base(propertyName, TypeBasicInfo.EmitReference(type))
        {
        }

        public FieldMetadata() :base(){ }
    }
}
