using System;
using System.Collections.Generic;
using System.Reflection;
using GUI.View.TypesView;
using Model.MetadataClasses.Types.Members;

namespace TPA_project.View.TypesView.MethodTypes
{
    public class IndexerView : TypeViewAbstract
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string Description => "Indexer";
        public override string IconPath => "Icons/Property.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public IndexerView(IndexerMetadata metadata) : base()
        {
            log.Debug("Creating Indexer View");

            mName = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                mTypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewAbstract> CreateChildren()
        {
            log.Error("Cannot create children");
            throw new NotSupportedException();
        }
    }
}