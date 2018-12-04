using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.View.TypesView
{
    public class EventView : TypeViewAbstract
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

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

            Type type = Type.GetType(metadata.TypeMetadata.FullTypeName);
            typeMetadata = new TypeMetadata(type);
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
