using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;

namespace ViewModel.ModelRepresentation.Types.ReferenceTypes
{
    public class ArrayViewModel : ReferenceViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public ArrayViewModel(TypeMetadata type, string name) : base(type, name)
        {
            Log.Info("Creating Array View");
        }

        public override string Description => "Array";
        public override string IconPath => "Icons/Collection.png";
    }
}
