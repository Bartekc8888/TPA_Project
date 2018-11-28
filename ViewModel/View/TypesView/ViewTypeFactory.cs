
using System;
using System.Reflection;
using log4net;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using ViewModel.View.TypesView.MethodTypes;
using ViewModel.View.TypesView.ReferenceTypes;
using ViewModel.View.TypesView.ValueTypes;

namespace ViewModel.View.TypesView
{
    public static class ViewTypeFactory
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public static TypeViewAbstract CreateTypeViewClass(TypeMetadata type, string name = "")
        {
            switch (type.TypeEnum)
            {
                case TypeTypesEnum.Array:
                    return new ArrayView(type, name);
                case TypeTypesEnum.Class:
                    return new ClassView(type, name);
                case TypeTypesEnum.Delegate:
                    return new DelegateView(type, name);
                case TypeTypesEnum.Interface:
                    return new InterfaceView(type, name);
                case TypeTypesEnum.Enum:
                    return new EnumView(type, name);
                case TypeTypesEnum.Primitive:
                    return new PrimitiveView(type, name);
                case TypeTypesEnum.Structure:
                    return new StructureView(type, name);
                default:
                    Log.Error("Unknown types type enum.");
                    throw new NotSupportedException("Unknown types type enum.");

            }
        }

        public static TypeViewAbstract CreateTypeViewClass(NamespaceMetadata metadata)
        {
            return new NamespaceView(metadata);
        }
        
        public static TypeViewAbstract CreateTypeViewClass(MethodMetadata metadata)
        {
            switch (metadata)
            {
                case ConstructorMetadata constructorMetadata:
                    return new ConstructorView(constructorMetadata);
                default:
                    return new MethodView(metadata);
            }
        }

        public static TypeViewAbstract CreateTypeViewClass(MemberAbstract member)
        {
            switch (member)
            {
                case EventMetadata eventMetadata:
                    return new EventView(eventMetadata);
                case FieldMetadata fieldMetadata:
                    return new FieldView(fieldMetadata);
                case IndexerMetadata indexerMetadata:
                    return new IndexerView(indexerMetadata);
                case PropertyMetadata propertyMetadata:
                    return new PropertyView(propertyMetadata);
                default:
                    return CreateTypeViewClass(new TypeMetadata(Type.GetType(member.TypeMetadata.FullTypeName)), member.Name);
            }
        }

        public static TypeViewAbstract CreateTypeViewClass(TypeBasicInfo basicInfo)
        {
            return CreateTypeViewClass(new TypeMetadata(Type.GetType(basicInfo.FullTypeName)));
        }

        public static TypeViewAbstract CreateTypeViewClass(AssemblyMetadata assemblyMetadata)
        {
            return new AssemblyView(assemblyMetadata);
        }
    }
}
