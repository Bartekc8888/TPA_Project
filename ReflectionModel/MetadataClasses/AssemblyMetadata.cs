﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.MetadataExtensions;

namespace Model.MetadataClasses
{
    public class AssemblyMetadata
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public IEnumerable<NamespaceMetadata> Namespaces { get; set; }
       
        public AssemblyMetadata(Assembly assembly)
        {
            TypeName = assembly.GetType().Name;
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                         group _type by _type.GetNamespace() into _group
                         orderby _group.Key
                         select new NamespaceMetadata(_group.Key, _group);
        }

        public AssemblyMetadata() { }

        protected bool Equals(AssemblyMetadata other)
        {
            return string.Equals(TypeName, other.TypeName) && string.Equals(Name, other.Name) && Equals(Namespaces, other.Namespaces);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AssemblyMetadata) obj);
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

        public AssemblyMetadata(AssemblyModel model)
        {
            TypeName = model.TypeName;
            Name = model.Name;
            Namespaces = model.Namespaces.Select(namespaceModel => new NamespaceMetadata(namespaceModel));
        }

        public AssemblyModel ToModel()
        {
            AssemblyModel assemblyModel = new AssemblyModel();
            assemblyModel.TypeName = TypeName;
            assemblyModel.Name = Name;
            assemblyModel.Namespaces = Namespaces.Select(model => model.ToModel());

            return assemblyModel;
        }
    }
}