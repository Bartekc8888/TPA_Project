using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ReferenceTypes
{
    public class ArrayView : ReferenceView
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public ArrayView(TypeMetadata type, string name) : base(type, name)
        {
            Log.Info("Creating Array View");
        }

        public override string Description => "Array";
        public override string IconPath => "Icons/Collection.png";
    }
}
