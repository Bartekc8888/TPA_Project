
using Model.MetadataExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses
{
    public class AssemblyMetadata
    {
        private string name;
        private IEnumerable<NamespaceMetadata> namespaces;

        internal AssemblyMetadata(Assembly assembly)
        {
            name = assembly.ManifestModule.Name;
            namespaces = from Type _type in assembly.GetTypes()
                         where _type.GetVisible()
                         group _type by _type.GetNamespace() into _group
                         orderby _group.Key
                         select new NamespaceMetadata(_group.Key, _group);
        }
    }
}