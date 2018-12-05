using System;
using System.Collections.Generic;
using System.Reflection;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types
{
    public class EventViewModel : TypeViewModelAbstract
    {

        private TypeMetadata typeMetadata;
        public override string Description => "Event";
        public override string IconPath => "Icons/Event.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public EventViewModel(EventMetadata metadata) : base()
        {

            Type type = Type.GetType(metadata.TypeMetadata.FullTypeName);
            typeMetadata = new TypeMetadata(type);
            mName = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                mTypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewModelAbstract> CreateChildren()
        {

            throw new NotSupportedException();
        }
    }
}
