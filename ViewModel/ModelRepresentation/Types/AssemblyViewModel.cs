using System.Collections.Generic;
using System.Linq;
using Model.MetadataClasses;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types
{
    public class AssemblyViewModel : TypeViewModelAbstract
    {
        public override string TypeName { get; }
        public override string Name { get; }
        public override string Description => "Assembly";
        public override string IconPath => "Icons/Namespace.png";
        public override bool HaveChildren => true;

        private readonly IEnumerable<NamespaceMetadata> children;
        
        public AssemblyViewModel(AssemblyMetadata metadata)
        {
            TypeName = metadata.TypeName;
            Name = metadata.Name;

            children = metadata.Namespaces;
        }
        
        public override IList<TypeViewModelAbstract> CreateChildren()
        {
            List<TypeViewModelAbstract> typeViewList = new List<TypeViewModelAbstract>();
            typeViewList.AddRange(children.Select(ModelViewTypeFactory.CreateTypeViewClass));

            return typeViewList;
        }
    }
}