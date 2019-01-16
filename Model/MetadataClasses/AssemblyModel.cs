using System.Collections.Generic;

namespace Model.MetadataClasses
{
    public class AssemblyModel
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public IEnumerable<NamespaceModel> Namespaces { get; set; }
    }
}