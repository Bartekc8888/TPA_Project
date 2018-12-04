using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ValueTypes
{
    public class PrimitiveViewModel : ValueViewModel
    {
        public PrimitiveViewModel(TypeMetadata type, string name) : base(type, name)
        {

        }

        public override string Description => "Primitive";
        public override string IconPath => "Icons/Value.png";
    }
}
