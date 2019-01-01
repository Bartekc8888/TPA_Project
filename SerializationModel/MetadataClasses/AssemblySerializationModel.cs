using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses;

namespace SerializationModel.MetadataClasses
{
    [DataContract(IsReference = true)]
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

        protected bool Equals(AssemblySerializationModel other)
        {
            return string.Equals(TypeName, other.TypeName) && string.Equals(Name, other.Name) && Equals(Namespaces, other.Namespaces);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AssemblySerializationModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (TypeName != null ? TypeName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Namespaces != null ? Namespaces.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}