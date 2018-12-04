﻿using System;
using System.Reflection;
using log4net;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using ViewModel.Logic;
using ViewModel.ModelRepresentation.Types.MethodTypes;
using ViewModel.ModelRepresentation.Types.ReferenceTypes;
using ViewModel.ModelRepresentation.Types.ValueTypes;

namespace ViewModel.ModelRepresentation.Types
{
    public static class ModelViewTypeFactory
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public static TypeViewModelAbstract CreateTypeViewClass(TypeMetadata type, string name = "")
        {
            switch (type.TypeEnum)
            {
                case TypeTypesEnum.Array:
                    return new ArrayViewModel(type, name);
                case TypeTypesEnum.Class:
                    return new ClassViewModel(type, name);
                case TypeTypesEnum.Delegate:
                    return new DelegateViewModel(type, name);
                case TypeTypesEnum.Interface:
                    return new InterfaceViewModel(type, name);
                case TypeTypesEnum.Enum:
                    return new EnumViewModel(type, name);
                case TypeTypesEnum.Primitive:
                    return new PrimitiveViewModel(type, name);
                case TypeTypesEnum.Structure:
                    return new StructureViewModel(type, name);
                default:
                    Log.Error("Unknown types type enum.");
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

        public static TypeViewModelAbstract CreateTypeViewClass(MemberAbstract member)
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
                    return CreateTypeViewClass(new TypeMetadata(Type.GetType(member.TypeMetadata.FullTypeName)), member.Name);
            }
        }

        public static TypeViewModelAbstract CreateTypeViewClass(TypeBasicInfo basicInfo)
        {
            return CreateTypeViewClass(new TypeMetadata(Type.GetType(basicInfo.FullTypeName)));
        }

        public static TypeViewModelAbstract CreateTypeViewClass(AssemblyMetadata assemblyMetadata)
        {
            return new AssemblyViewModel(assemblyMetadata);
        }
    }
}