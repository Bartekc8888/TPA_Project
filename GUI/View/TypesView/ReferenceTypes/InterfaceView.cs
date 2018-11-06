using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public class InterfaceView : ReferenceView
    {
        public InterfaceView(TypeMetadata type, string name) : base(type, name)
        {
        }

        public override string Description => "Interface";
        public override string IconPath => "Icons/Interface.png";
    }
}
