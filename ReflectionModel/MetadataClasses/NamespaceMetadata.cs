using System;
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

        protected bool Equals(NamespaceMetadata other)
        {
            return string.Equals(NamespaceName, other.NamespaceName) && Equals(Types, other.Types);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NamespaceMetadata) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((NamespaceName != null ? NamespaceName.GetHashCode() : 0) * 397) ^ (Types != null ? Types.GetHashCode() : 0);
            }
        }

        public NamespaceMetadata(NamespaceModel model)
        {
            NamespaceName = model.NamespaceName;
            Types = model.Types.Select(TypeMetadata.EmitTypeMetadata);
        }

        public NamespaceModel ToModel()
        {
            NamespaceModel namespaceModel = new NamespaceModel();
            namespaceModel.NamespaceName = NamespaceName;
            namespaceModel.Types = Types.Select(model => model.ToModel());
            return namespaceModel;
        }
    }
}