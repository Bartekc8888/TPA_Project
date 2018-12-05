using System.Reflection;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public class ArrayViewModel : ReferenceViewModel
    {
        public ArrayViewModel(TypeMetadata type, string name) : base(type, name)
        {
        }

        public override string Description => "Array";
        public override string IconPath => "Icons/Collection.png";
    }
}
