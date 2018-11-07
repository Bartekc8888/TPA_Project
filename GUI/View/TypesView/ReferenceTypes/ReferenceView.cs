using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public abstract class ReferenceView : BaseTypeView
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ReferenceView(TypeMetadata type, string name) : base(type, name)
        {
            log.Debug("Creating Reference View");
        }
    }
}
