using System.Reflection;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public class ClassViewModel : ReferenceViewModel
    {
        public ClassViewModel(TypeMetadata type, string name) : base(type, name)
        {
        }

        public override string Description => "Class";
        public override string IconPath => "Icons/Class.png";
    }
}
