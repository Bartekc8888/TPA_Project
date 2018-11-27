
using Model.MetadataClasses;
using Model.MetadataDefinitions;
using ViewModel.View.TypesView.ReferenceTypes;
using ViewModel.View.TypesView.ValueTypes;
using ViewModel.View.TypesView.MethodTypes;
using System;
using Model.MetadataClasses.Types.Members;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView
{
    public static class ViewTypeFactory
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                    return CreateTypeViewClass(new TypeMetadata(member.TypeMetadata.InfoType), member.Name);
            }
        }

        public static TypeViewAbstract CreateTypeViewClass(TypeBasicInfo basicInfo)
        {
            return CreateTypeViewClass(new TypeMetadata(basicInfo.InfoType));
        }

        public static TypeViewAbstract CreateTypeViewClass(AssemblyMetadata assemblyMetadata)
        {
            return new AssemblyView(assemblyMetadata);
        }
    }
}
