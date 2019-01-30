using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses;
using Serialization.MetadataClasses.Types;

namespace Serialization.MetadataClasses
{
    [DataContract(IsReference = true)]
    public class NamespaceSerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public string NamespaceName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeSerializationModel> Types { get; set; }

        public NamespaceSerializationModel(NamespaceModel model)
        {
            NamespaceName = model.NamespaceName;
            Types = model.Types.Select(TypeSerializationModel.EmitTypeSerializationModel);
        }
        
        public NamespaceModel ToModel()
        {
            NamespaceModel namespaceModel = new NamespaceModel();
            namespaceModel.NamespaceName = NamespaceName;
            namespaceModel.Types = Types.Select(model => model.ToModel());
            return namespaceModel;
        }

        protected bool Equals(NamespaceSerializationModel other)
        {
            return string.Equals(NamespaceName, other.NamespaceName) && Equals(Types, other.Types);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NamespaceSerializationModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((NamespaceName != null ? NamespaceName.GetHashCode() : 0) * 397) ^ (Types != null ? Types.GetHashCode() : 0);
            }
        }
    }
}