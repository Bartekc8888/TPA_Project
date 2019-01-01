﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.MetadataClasses.Types;

namespace Model.MetadataClasses
{
    public class NamespaceMetadata
    {
        public string NamespaceName { get; set; }
        public IEnumerable<TypeMetadata> Types { get; set; }

        public NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            NamespaceName = name;
            Types = from type in types orderby type.Name select new TypeMetadata(type);
        }

        public NamespaceMetadata() { }
    }
}