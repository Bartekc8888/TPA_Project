using System;
using System.Collections.Generic;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types.MethodTypes
{
    public class IndexerViewModel : TypeViewModelAbstract
    {
        public override string Description => "Indexer";
        public override string IconPath => "Icons/Property.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public IndexerViewModel(IndexerMetadata metadata)
        {

            mName = metadata.Name;
            mTypeName = metadata.TypeName;
        }

        public override IList<TypeViewModelAbstract> CreateChildren()
        {
            throw new NotSupportedException();
        }
    }
}