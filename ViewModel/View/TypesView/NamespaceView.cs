using System.Collections.Generic;
using System.Linq;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView
{
    public class NamespaceView : TypeViewAbstract
    {
        public override string TypeName => "Namespace";
        public override string Name { get; }
        public override string Description => "Namespace";
        public override string IconPath => "Icons/Namespace.png";
        public override bool HaveChildren => true;

        private readonly IEnumerable<TypeMetadata> children;
        
        public NamespaceView(NamespaceMetadata metadata)
        {
            Name = metadata.NamespaceName;

            children = metadata.Types;
        }
        
        public override IList<TypeViewAbstract> CreateChildren()
        {
            List<TypeViewAbstract> typeViewList = new List<TypeViewAbstract>();
            typeViewList.AddRange(children.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));

            return typeViewList;
        }
    }
}