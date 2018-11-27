using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public class InterfaceView : ReferenceView
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public InterfaceView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Interface View");
        }

        public override string Description => "Interface";
        public override string IconPath => "Icons/Interface.png";
    }
}
