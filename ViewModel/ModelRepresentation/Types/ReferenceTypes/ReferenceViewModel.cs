using System.Reflection;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public abstract class ReferenceViewModel : BaseTypeViewModel
    {

        public ReferenceViewModel(TypeMetadata type, string name) : base(type, name)
        {
        }
    }
}
