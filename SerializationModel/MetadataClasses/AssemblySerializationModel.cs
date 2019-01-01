using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses;

namespace SerializationModel.MetadataClasses
{
    [DataContract]
    public class AssemblySerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public string TypeName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<NamespaceSerializationModel> Namespaces { get; set; }

        public AssemblySerializationModel(AssemblyMetadata metadata)
        {
            TypeName = metadata.TypeName;
            Name = metadata.Name;
            Namespaces = metadata.Namespaces.Select(namespaceMetadata => new NamespaceSerializationModel(namespaceMetadata));
        }

        public AssemblyMetadata ToModel()
        {
            AssemblyMetadata assemblyMetadata = new AssemblyMetadata();
            assemblyMetadata.TypeName = TypeName;
            assemblyMetadata.Name = Name;
            assemblyMetadata.Namespaces = Namespaces.Select(model => model.ToModel());

            return assemblyMetadata;
        }
    }
}