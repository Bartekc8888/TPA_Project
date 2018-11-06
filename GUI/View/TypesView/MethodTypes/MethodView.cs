using System;
using System.Collections.Generic;
using Model.MetadataClasses.Types.Members;

namespace GUI.View.TypesView.MethodTypes
{
    public class MethodView : TypeViewAbstract
    {

        public override string Description => "Method";
        public override string IconPath => "Icons/Method.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public MethodView(MethodMetadata metadata) : base()
        {
            mName = metadata.Name;
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
