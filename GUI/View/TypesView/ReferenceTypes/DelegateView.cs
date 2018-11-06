using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public class DelegateView : ReferenceView
    {
        public DelegateView(TypeMetadata type, string name) : base(type, name)
        {

        }

        public override string Description => "Delegate";
        public override string IconPath => "Icons/Delegate.png";
    }
}
