using System;
using System.Collections.Generic;
using Model.MetadataClasses.Types.Members;

namespace GUI.View.TypesView.MethodTypes
{
    public class ConstructorView : MethodView
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string Description => "Constructor";
        public override string IconPath => "Icons/Method.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public ConstructorView(ConstructorMetadata metadata) : base(metadata)
        {
            log.Debug("Creating Constructor View");
            mName = metadata.Name + GetParameters(metadata.Parameters);
            if (metadata.ReturnType != null)
            {
                mTypeName = metadata.ReturnType.TypeName;
            }
        }

        public override IList<TypeViewAbstract> CreateChildren()
        {
            throw new NotSupportedException();
        }

    }
}
