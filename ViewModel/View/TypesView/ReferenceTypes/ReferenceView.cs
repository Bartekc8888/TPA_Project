using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public abstract class ReferenceView : BaseTypeView
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public ReferenceView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Debug("Creating Reference View");
        }
    }
}
