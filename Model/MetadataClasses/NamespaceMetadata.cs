using System.Collections.Generic;
using Model.MetadataClasses.Types;

namespace Model.MetadataClasses
{
    public class NamespaceMetadata
    {
        public string namespaceName;
        public IEnumerable<TypeMetadata> types;
    }
}