using Model.MetadataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GUI.View.TypesView
{
    public abstract class BaseTypeView : TypeViewAbstract
    {
        protected TypeMetadata mTypeMetadata;
        protected string mName;

        public BaseTypeView(TypeMetadata type, string name)
        {
            mTypeMetadata = type;
            mName = name;
        }

        public override string TypeName => mTypeMetadata.TypeBasicInfo.TypeName;
        public override string Name => mName;
        public override bool HaveChildren => true;

        public override IList<TypeViewAbstract> CreateChildren()
        {
            List<TypeViewAbstract> typeViewList = new List<TypeViewAbstract>();

            typeViewList.AddRange(mTypeMetadata.Constructors.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Methods.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));

            typeViewList.AddRange(mTypeMetadata.Fields.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Properties.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.NestedTypes.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Events.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));

            return typeViewList;
        }
    }
}
