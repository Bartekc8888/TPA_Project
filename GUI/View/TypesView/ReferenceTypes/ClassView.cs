using Model.MetadataClasses;

namespace GUI.View.TypesView.ReferenceTypes
{
    public class ClassView : ReferenceView
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ClassView(TypeMetadata type, string name) : base(type, name)
        {
            log.Debug("Creating Class View");
        }

        public override string Description => "Class";
        public override string IconPath => "Icons/Class.png";
    }
}
