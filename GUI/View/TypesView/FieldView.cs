using System;
using System.Collections.Generic;
using System.Linq;
using GUI.View.TypesView;
using Model.MetadataClasses;
using Model.MetadataClasses.Types.Members;

namespace TPA_project.View.TypesView
{
    public class FieldView : TypeViewAbstract
    {
        private TypeMetadata typeMetadata;
        public override string Description => "Field";
        public override string IconPath => "Icons/Field.png";
        public override bool HaveChildren => true;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public FieldView(FieldMetadata metadata) : base()
        {
            typeMetadata = new TypeMetadata(metadata.TypeMetadata.InfoType);
            mName = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                mTypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewAbstract> CreateChildren()
        {
            List<TypeViewAbstract> typeViewList = new List<TypeViewAbstract>();

            typeViewList.AddRange(typeMetadata.Constructors.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Methods.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Properties.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Indexers.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Fields.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.NestedTypes.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Events.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));

            return typeViewList;
        }
    }
}
