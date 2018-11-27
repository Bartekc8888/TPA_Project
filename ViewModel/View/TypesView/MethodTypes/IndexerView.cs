using System;
using System.Collections.Generic;
using Model.MetadataClasses.Types.Members;

namespace ViewModel.View.TypesView.MethodTypes
{
    public class IndexerView : TypeViewAbstract
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string Description => "Indexer";
        public override string IconPath => "Icons/Property.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public IndexerView(IndexerMetadata metadata)
        {
            Log.Info("Creating Indexer View");

            mName = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                mTypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewAbstract> CreateChildren()
        {
            Log.Error("Cannot create children");
            throw new NotSupportedException();
        }
    }
}