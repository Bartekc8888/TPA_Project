using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ValueTypes
{
    public abstract class ValueViewModel : BaseTypeViewModel
    {
        public ValueViewModel(TypeMetadata type, string name) : base(type, name)
        {
        }
    }
}
