using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types
{
    public class FieldViewModel : TypeViewModelAbstract
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        private TypeMetadata typeMetadata;
        public override string Description => "Field";
        public override string IconPath => "Icons/Field.png";
        public override bool HaveChildren => true;
        public override string TypeName { get; }
        public override string Name { get; }

        public FieldViewModel(FieldMetadata metadata) : base()
        {
            Log.Debug("Creating Field View");

            Type type = Type.GetType(metadata.TypeMetadata.FullTypeName);
            typeMetadata = new TypeMetadata(type);
            Name = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                TypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewModelAbstract> CreateChildren()
        {
            Log.Debug("Set members");

            List<TypeViewModelAbstract> typeViewList = new List<TypeViewModelAbstract>();

            typeViewList.AddRange(typeMetadata.Constructors.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Methods.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Properties.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Indexers.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Fields.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.NestedTypes.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Events.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));

            return typeViewList;
        }
    }
}
