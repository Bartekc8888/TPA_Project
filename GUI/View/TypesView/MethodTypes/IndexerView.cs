using System;
using System.Collections.Generic;
using System.Reflection;
using GUI.View.TypesView;
using Model.MetadataClasses.Types.Members;

namespace TPA_project.View.TypesView.MethodTypes
{
    public class IndexerView : TypeViewAbstract
    {

        public override string Description => "Indexer";
        public override string IconPath => "Icons/Property.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public IndexerView(IndexerMetadata metadata) : base()
        {
            mName = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                mTypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewAbstract> CreateChildren()
        {
            throw new NotSupportedException();
        }
    }
}