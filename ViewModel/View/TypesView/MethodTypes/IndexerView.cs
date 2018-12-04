using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.View.TypesView.MethodTypes
{
    public class IndexerView : TypeViewAbstract
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

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