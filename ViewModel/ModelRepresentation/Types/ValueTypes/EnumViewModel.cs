using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ValueTypes
{
    public class EnumViewModel : ValueViewModel
    {
        public EnumViewModel(TypeMetadata type, string name) : base(type, name)
        {
        }

        public override string Description => "Enumerator";
        public override string IconPath => "Icons/Enumerator.png";
    }
}
