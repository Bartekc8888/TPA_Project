using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses;
using SerializationModel.MetadataClasses.Types;

namespace SerializationModel.MetadataClasses
{
    [DataContract(IsReference = true)]
    public class NamespaceSerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public string NamespaceName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeSerializationModel> Types { get; set; }

        public NamespaceSerializationModel(NamespaceMetadata metadata)
        {
            NamespaceName = metadata.NamespaceName;
            Types = metadata.Types.Select(typeMetadata => new TypeSerializationModel(typeMetadata));
        }
        
        public NamespaceMetadata ToModel()
        {
            NamespaceMetadata namespaceMetadata = new NamespaceMetadata();
            namespaceMetadata.NamespaceName = NamespaceName;
            namespaceMetadata.Types = Types.Select(model => model.ToModel());
            return null;
        }
    }
}