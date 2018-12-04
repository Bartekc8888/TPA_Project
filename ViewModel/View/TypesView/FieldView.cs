using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.View.TypesView
{
    public class FieldView : TypeViewAbstract
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        private TypeMetadata typeMetadata;
        public override string Description => "Field";
        public override string IconPath => "Icons/Field.png";
        public override bool HaveChildren => true;
        public override string TypeName { get; }
        public override string Name { get; }

        public FieldView(FieldMetadata metadata) : base()
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

        public override IList<TypeViewAbstract> CreateChildren()
        {
            Log.Debug("Set members");

            List<TypeViewAbstract> typeViewList = new List<TypeViewAbstract>();

            typeViewList.AddRange(typeMetadata.Constructors.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Methods.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Properties.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Indexers.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Fields.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.NestedTypes.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(typeMetadata.Events.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));

            return typeViewList;
        }
    }
}
