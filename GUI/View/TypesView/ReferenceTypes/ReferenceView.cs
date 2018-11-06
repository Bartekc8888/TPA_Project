using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public abstract class ReferenceView : BaseTypeView
    {
        public ReferenceView(TypeMetadata type, string name) : base(type, name)
        {
        }
    }
}
