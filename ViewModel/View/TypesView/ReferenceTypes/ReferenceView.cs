using Model.MetadataClasses;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public abstract class ReferenceView : BaseTypeView
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ReferenceView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Reference View");
        }
    }
}
