using System;
using System.Collections.Generic;
using System.Linq;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types
{
    public class FieldViewModel : TypeViewModelAbstract
    {

        private TypeMetadata typeMetadata;
        public override string Description => "Field";
        public override string IconPath => "Icons/Field.png";
        public override bool HaveChildren => true;
        public override string TypeName { get; }
        public override string Name { get; }

        public FieldViewModel(FieldMetadata metadata) : base()
        {

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
