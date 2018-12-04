using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types.MethodTypes
{
    public class PropertyViewModel : TypeViewModelAbstract
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

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
            Log.Info("Creating Property View");

            this.metadata = metadata;
            mName = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                mTypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewModelAbstract> CreateChildren()
        {
            Log.Info("Set members");

            List<TypeViewModelAbstract> typeViewList = new List<TypeViewModelAbstract>();

            typeViewList.AddRange(metadata.propertyMethods.Select(ModelViewTypeFactory.CreateTypeViewClass));
            
            return typeViewList;
        }
    }
}