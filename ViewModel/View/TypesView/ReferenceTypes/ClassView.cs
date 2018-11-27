using Model.MetadataClasses;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public class ClassView : ReferenceView
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ClassView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Info("Creating Class View");
        }

        public override string Description => "Class";
        public override string IconPath => "Icons/Class.png";
    }
}
