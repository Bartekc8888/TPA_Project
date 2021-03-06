﻿using System;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using ReflectionModel;
using ViewModel.Logic;
using ViewModel.ModelRepresentation.Types.MethodTypes;
using ViewModel.ModelRepresentation.Types.ReferenceTypes;
using ViewModel.ModelRepresentation.Types.ValueTypes;

namespace ViewModel.ModelRepresentation.Types
{
    public static class ModelViewTypeFactory
    {
        public static Reflector CurrentAssemblyExtractor { get; set; }
        
        public static TypeViewModelAbstract CreateTypeViewClass(TypeMetadata type, string name = "")
        {
            switch (type.TypeEnum)
            {
                case TypeTypesEnumMetadata.Array:
                    return new ArrayViewModel(type, name);
                case TypeTypesEnumMetadata.Class:
                    return new ClassViewModel(type, name);
                case TypeTypesEnumMetadata.Delegate:
                    return new DelegateViewModel(type, name);
                case TypeTypesEnumMetadata.Interface:
                    return new InterfaceViewModel(type, name);
                case TypeTypesEnumMetadata.Enum:
                    return new EnumViewModel(type, name);
                case TypeTypesEnumMetadata.Primitive:
                    return new PrimitiveViewModel(type, name);
                case TypeTypesEnumMetadata.Structure:
                    return new StructureViewModel(type, name);
                default:
                    throw new NotSupportedException("Unknown types type enum.");

            }
        }

        public static TypeViewModelAbstract CreateTypeViewClass(NamespaceMetadata metadata)
        {
            return new NamespaceViewModel(metadata);
        }
        
        public static TypeViewModelAbstract CreateTypeViewClass(MethodMetadata metadata)
        {
            switch (metadata)
            {
                case ConstructorMetadata constructorMetadata:
                    return new ConstructorViewModel(constructorMetadata);
                default:
                    return new MethodViewModel(metadata);
            }
        }

        public static TypeViewModelAbstract CreateTypeViewClass(MemberAbstractMetadata member)
        {
            switch (member)
            {
                case EventMetadata eventMetadata:
                    return new EventViewModel(eventMetadata);
                case FieldMetadata fieldMetadata:
                    return new FieldViewModel(fieldMetadata);
                case IndexerMetadata indexerMetadata:
                    return new IndexerViewModel(indexerMetadata);
                case PropertyMetadata propertyMetadata:
                    return new PropertyViewModel(propertyMetadata);
                default:
                    return CreateTypeViewClass(new TypeMetadata(GetFromFullName(member.TypeName)), member.Name);
            }
        }

        public static TypeViewModelAbstract CreateTypeViewClass(AssemblyMetadata assemblyMetadata)
        {
            return new AssemblyViewModel(assemblyMetadata);
        }

        public static Type GetFromFullName(String fullTypeName)
        {
            Type type = null;
            if (CurrentAssemblyExtractor != null && CurrentAssemblyExtractor.LoadedAssembly != null)
            {
                type = CurrentAssemblyExtractor.LoadedAssembly.GetType(fullTypeName);
            }

            return type == null ? Type.GetType(fullTypeName) : type;
        }
    }
}
