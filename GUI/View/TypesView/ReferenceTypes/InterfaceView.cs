using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public class InterfaceView : ReferenceView
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public InterfaceView(TypeMetadata type, string name) : base(type, name)
        {
            log.Debug("Creating Interface View");
        }

        public override string Description => "Interface";
        public override string IconPath => "Icons/Interface.png";
    }
}
