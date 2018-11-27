using Model.MetadataClasses;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public class DelegateView : ReferenceView
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DelegateView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Delegate View");
        }

        public override string Description => "Delegate";
        public override string IconPath => "Icons/Delegate.png";
    }
}
