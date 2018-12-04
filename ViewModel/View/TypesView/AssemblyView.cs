using System.Collections.Generic;
using System.Linq;
using Model.MetadataClasses;
using ViewModel.Logic;

namespace ViewModel.View.TypesView
{
    public class AssemblyView : TypeViewAbstract
    {
        public override string TypeName { get; }
        public override string Name { get; }
        public override string Description => "Assembly";
        public override string IconPath => "Icons/Namespace.png";
        public override bool HaveChildren => true;

        private readonly IEnumerable<NamespaceMetadata> children;
        
        public AssemblyView(AssemblyMetadata metadata)
        {
            TypeName = metadata.TypeName;
            Name = metadata.Name;

            children = metadata.Namespaces;
        }
        
        public override IList<TypeViewAbstract> CreateChildren()
        {
            List<TypeViewAbstract> typeViewList = new List<TypeViewAbstract>();
            typeViewList.AddRange(children.Select(ViewTypeFactory.CreateTypeViewClass));

            return typeViewList;
        }
    }
}