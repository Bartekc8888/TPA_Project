using System;
using System.Collections.Generic;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types.MethodTypes
{
    public class ConstructorViewModel : MethodViewModel
    {
        public override string Description => "Constructor";
        public override string IconPath => "Icons/Method.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public ConstructorViewModel(ConstructorMetadata metadata) : base(metadata)
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

    }
}
