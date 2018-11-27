using Model.MetadataClasses;

namespace ViewModel.View.TypesView.ValueTypes
{
    public abstract class ValueView : BaseTypeView
    {
        public ValueView(TypeMetadata type, string name) : base(type, name)
        {
        }
    }
}
