using System;
using System.Collections.Generic;
using Model.MetadataClasses;
using Model.MetadataClasses.Types.Members;

namespace ViewModel.View.TypesView
{
    public class EventView : TypeViewAbstract
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TypeMetadata typeMetadata;
        public override string Description => "Event";
        public override string IconPath => "Icons/Event.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public EventView(EventMetadata metadata) : base()
        {
            Log.Debug("Creating Event View");

            typeMetadata = new TypeMetadata(metadata.TypeMetadata.InfoType);
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
