using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public class ArrayView : ReferenceView
    {
        public ArrayView(TypeMetadata type, string name) : base(type, name)
        {

        }

        public override string Description => "Array";
        public override string IconPath => "Icons/Collection.png";
    }
}
