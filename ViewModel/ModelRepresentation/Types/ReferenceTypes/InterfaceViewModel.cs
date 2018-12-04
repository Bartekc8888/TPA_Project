using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public class InterfaceViewModel : ReferenceViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public InterfaceViewModel(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Interface View");
        }

        public override string Description => "Interface";
        public override string IconPath => "Icons/Interface.png";
    }
}
