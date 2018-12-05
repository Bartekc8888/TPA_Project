using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types.MethodTypes
{
    public class MethodViewModel : TypeViewModelAbstract
    {

        public override string Description => CheckIfFinalizer();
        public override string IconPath => "Icons/Method.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public MethodViewModel(MethodMetadata metadata) : base()
        {

            mName = metadata.Name + GetParameters(metadata.Parameters);
            if (metadata.ReturnType != null)
            {
                mTypeName = metadata.ReturnType.TypeName;
            }
        }

        public override IList<TypeViewModelAbstract> CreateChildren()
        {

            throw new NotSupportedException();
        }

        private string CheckIfFinalizer()
        {

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
