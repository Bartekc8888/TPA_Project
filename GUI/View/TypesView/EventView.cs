using System;
using System.Collections.Generic;
using System.Linq;
using GUI.View.TypesView;
using Model.MetadataClasses;
using Model.MetadataClasses.Types.Members;

namespace TPA_project.View.TypesView
{
    public class EventView : TypeViewAbstract
    {
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
            typeMetadata = new TypeMetadata(metadata.TypeMetadata.InfoType);
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
