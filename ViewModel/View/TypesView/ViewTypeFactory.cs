
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

        public static TypeViewAbstract CreateTypeViewClass(TypeMetadata type, string Name = "")
        {
            switch (type.TypeEnum)
            {
                case TypeTypesEnum.Array:
                    return new ArrayView(type, Name);
                case TypeTypesEnum.Class:
                    return new ClassView(type, Name);
                case TypeTypesEnum.Delegate:
                    return new DelegateView(type, Name);
                case TypeTypesEnum.Interface:
                    return new InterfaceView(type, Name);
                case TypeTypesEnum.Enum:
                    return new EnumView(type, Name);
                case TypeTypesEnum.Primitive:
                    return new PrimitiveView(type, Name);
                case TypeTypesEnum.Structure:
                    return new StructureView(type, Name);
                case TypeTypesEnum.Unknown:
                default:
                    Log.Error("Unknown types type enum.");
                    throw new NotSupportedException("Unknown types type enum.");

            }
        }

        public static TypeViewAbstract CreateTypeViewClass(FieldMetadata metadata)
        { 
            return new FieldView(metadata);
        }

        public static TypeViewAbstract CreateTypeViewClass(ConstructorMetadata metadata)
        {
            return new ConstructorView(metadata);
        }

        public static TypeViewAbstract CreateTypeViewClass(MethodMetadata metadata)
        {
            return new MethodView(metadata);
        }

        public static TypeViewAbstract CreateTypeViewClass(PropertyMetadata metadata)
        {
            return new PropertyView(metadata);
        }

        public static TypeViewAbstract CreateTypeViewClass(IndexerMetadata metadata)
        {
            return new IndexerView(metadata);
        }

        public static TypeViewAbstract CreateTypeViewClass(EventMetadata metadata)
        {
            return new EventView(metadata);
        }

        public static TypeViewAbstract CreateTypeViewClass(MemberAbstract member)
        {
            return CreateTypeViewClass(new TypeMetadata(member.TypeMetadata.InfoType), member.Name);
        }

        public static TypeViewAbstract CreateTypeViewClass(TypeBasicInfo basicInfo)
        {
            return CreateTypeViewClass(new TypeMetadata(basicInfo.InfoType));
        }

    }
}
