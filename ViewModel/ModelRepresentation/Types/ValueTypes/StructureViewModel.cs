using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ValueTypes
{
    public class StructureViewModel : ValueViewModel
    {
        public StructureViewModel(TypeMetadata type, string name) : base(type, name)
        {

        }

        public override string Description => "Structure";
        public override string IconPath => "Icons/Structure.png";
    }
}
