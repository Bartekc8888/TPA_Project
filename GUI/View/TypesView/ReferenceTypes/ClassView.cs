using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public class ClassView : ReferenceView
    {
        public ClassView(TypeMetadata type, string name) : base(type, name)
        {

        }

        public override string Description => "Class";
        public override string IconPath => "Icons/Class.png";
    }
}
