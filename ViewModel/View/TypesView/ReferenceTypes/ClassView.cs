using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public class ClassView : ReferenceView
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public ClassView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Info("Creating Class View");
        }

        public override string Description => "Class";
        public override string IconPath => "Icons/Class.png";
    }
}
