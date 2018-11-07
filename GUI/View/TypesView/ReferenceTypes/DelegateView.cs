using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public class DelegateView : ReferenceView
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DelegateView(TypeMetadata type, string name) : base(type, name)
        {
            log.Debug("Creating Delegate View");
        }

        public override string Description => "Delegate";
        public override string IconPath => "Icons/Delegate.png";
    }
}
