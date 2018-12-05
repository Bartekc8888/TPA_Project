using System.Reflection;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public class InterfaceViewModel : ReferenceViewModel
    {

        public InterfaceViewModel(TypeMetadata type, string name) : base(type, name)
        {
        }

        public override string Description => "Interface";
        public override string IconPath => "Icons/Interface.png";
    }
}
