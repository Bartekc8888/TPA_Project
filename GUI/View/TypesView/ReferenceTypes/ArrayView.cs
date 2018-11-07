using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public class ArrayView : ReferenceView
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ArrayView(TypeMetadata type, string name) : base(type, name)
        {
            log.Debug("Creating Array View");
        }

        public override string Description => "Array";
        public override string IconPath => "Icons/Collection.png";
    }
}
