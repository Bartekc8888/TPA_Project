using System;
using System.Collections.Generic;
using System.Linq;
using Model.MetadataClasses.Types.Members;

namespace GUI.View.TypesView.MethodTypes
{
    public class MethodView : TypeViewAbstract
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string Description => CheckIfFinalizer();
        public override string IconPath => "Icons/Method.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public MethodView(MethodMetadata metadata) : base()
        {
            log.Debug("Creating Method View");

            mName = metadata.Name + GetParameters(metadata.Parameters);
            if (metadata.ReturnType != null)
            {
                mTypeName = metadata.ReturnType.TypeName;
            }
        }

        public override IList<TypeViewAbstract> CreateChildren()
        {
            log.Error("Cannot create members");

            throw new NotSupportedException();
        }

        private string CheckIfFinalizer()
        {
            log.Debug("Checking if method is finalizer");

            if (mName=="Finalize")
            {
                return "Finalizer";
            }
            else
            {
                return "Method";
            }
        }

        protected string GetParameters(IEnumerable<ParameterMetadata> methodParameters)
        {
            log.Debug("Set parameters");

            string parameters = "(";
            methodParameters.ToList().ForEach(parameter => parameters += parameter.TypeMetadata.TypeName + " " + parameter.Name + ", ");
            if (parameters.EndsWith(", "))
            {
                parameters = parameters.Remove(parameters.Length - 2);
            }
            parameters += ")";

            return parameters;
        }
    }
}
