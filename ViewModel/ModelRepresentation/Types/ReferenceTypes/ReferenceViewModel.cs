using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public abstract class ReferenceViewModel : BaseTypeViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public ReferenceViewModel(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Reference View");
        }
    }
}
