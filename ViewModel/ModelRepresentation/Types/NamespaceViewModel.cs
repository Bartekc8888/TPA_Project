using System.Collections.Generic;
using System.Linq;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types
{
    public class NamespaceViewModel : TypeViewModelAbstract
    {
        public override string TypeName => "Namespace";
        public override string Name { get; }
        public override string Description => "Namespace";
        public override string IconPath => "Icons/Namespace.png";
        public override bool HaveChildren => true;

        private readonly IEnumerable<TypeMetadata> children;
        
        public NamespaceViewModel(NamespaceMetadata metadata)
        {
            Name = metadata.NamespaceName;

            children = metadata.Types;
        }
        
        public override IList<TypeViewModelAbstract> CreateChildren()
        {
            List<TypeViewModelAbstract> typeViewList = new List<TypeViewModelAbstract>();
            typeViewList.AddRange(children.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));

            return typeViewList;
        }
    }
}