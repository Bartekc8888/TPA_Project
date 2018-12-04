using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public class ClassViewModel : ReferenceViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public ClassViewModel(TypeMetadata type, string name) : base(type, name)
        {
            Log.Info("Creating Class View");
        }

        public override string Description => "Class";
        public override string IconPath => "Icons/Class.png";
    }
}
