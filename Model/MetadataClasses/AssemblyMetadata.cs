using System.Collections.Generic;

namespace Model.MetadataClasses
{
    public class AssemblyMetadata
    {
        public string typeName;
        public string name;
        public IEnumerable<NamespaceMetadata> namespaces;
    }
}