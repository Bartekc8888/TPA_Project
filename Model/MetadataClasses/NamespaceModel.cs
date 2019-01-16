using System.Collections.Generic;
using Model.MetadataClasses.Types;

namespace Model.MetadataClasses
{
    public class NamespaceModel
    {
        public string NamespaceName { get; set; }
        public IEnumerable<TypeModel> Types { get; set; }
    }
}