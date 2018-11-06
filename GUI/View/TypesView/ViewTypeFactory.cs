﻿
using Model.MetadataClasses;
using Model.MetadataDefinitions;
using GUI.View.TypesView.ReferenceTypes;
using GUI.View.TypesView.ValueTypes;
using GUI.View.TypesView.MethodTypes;
using System;
using Model.MetadataClasses.Types.Members;
using Model.MetadataClasses.Types;

namespace GUI.View.TypesView
{
    public static class ViewTypeFactory
    {
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
                    throw new NotSupportedException("Unknown types type enum.");
            }
        }

        public static TypeViewAbstract CreateTypeViewClass(MethodMetadata metadata)
        {
            return new MethodView(metadata);
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
