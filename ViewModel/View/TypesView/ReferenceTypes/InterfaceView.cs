using Model.MetadataClasses;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public class InterfaceView : ReferenceView
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public InterfaceView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Interface View");
        }

        public override string Description => "Interface";
        public override string IconPath => "Icons/Interface.png";
    }
}
