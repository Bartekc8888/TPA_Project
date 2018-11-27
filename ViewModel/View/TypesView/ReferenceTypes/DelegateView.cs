using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public class DelegateView : ReferenceView
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public DelegateView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Delegate View");
        }

        public override string Description => "Delegate";
        public override string IconPath => "Icons/Delegate.png";
    }
}
