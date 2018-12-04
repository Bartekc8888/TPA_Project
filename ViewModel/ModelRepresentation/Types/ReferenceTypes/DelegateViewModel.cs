using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public class DelegateViewModel : ReferenceViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public DelegateViewModel(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Delegate View");
        }

        public override string Description => "Delegate";
        public override string IconPath => "Icons/Delegate.png";
    }
}
