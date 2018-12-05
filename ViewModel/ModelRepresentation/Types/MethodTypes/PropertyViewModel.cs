using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types.MethodTypes
{
    public class PropertyViewModel : TypeViewModelAbstract
    {
        PropertyMetadata metadata;
        public override string Description => "Property";
        public override string IconPath => "Icons/Property.png";
        public override bool HaveChildren => true;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public PropertyViewModel(PropertyMetadata metadata) : base()
        {

            this.metadata = metadata;
            mName = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                mTypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewModelAbstract> CreateChildren()
        {

            List<TypeViewModelAbstract> typeViewList = new List<TypeViewModelAbstract>();

            typeViewList.AddRange(metadata.propertyMethods.Select(ModelViewTypeFactory.CreateTypeViewClass));
            
            return typeViewList;
        }
    }
}