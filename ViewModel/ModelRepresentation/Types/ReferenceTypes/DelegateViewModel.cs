using System.Reflection;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public class DelegateViewModel : ReferenceViewModel
    {

        public DelegateViewModel(TypeMetadata type, string name) : base(type, name)
        {
        }

        public override string Description => "Delegate";
        public override string IconPath => "Icons/Delegate.png";
    }
}
