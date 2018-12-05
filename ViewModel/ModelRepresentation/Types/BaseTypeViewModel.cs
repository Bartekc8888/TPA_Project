using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.MetadataClasses.Types;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types
{
    public abstract class BaseTypeViewModel : TypeViewModelAbstract
    {

        protected TypeMetadata mTypeMetadata;
        protected string mName;

        public BaseTypeViewModel(TypeMetadata type, string name)
        {

            mTypeMetadata = type;
            mName = name;
        }

        public override string TypeName => mTypeMetadata.TypeBasicInfo.TypeName;
        public override string Name => mName;
        public override bool HaveChildren => true;

        public override IList<TypeViewModelAbstract> CreateChildren()
        {

            List<TypeViewModelAbstract> typeViewList = new List<TypeViewModelAbstract>();

            typeViewList.AddRange(mTypeMetadata.Constructors.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Methods.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Properties.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Indexers.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Fields.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.NestedTypes.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Events.Select(elem => ModelViewTypeFactory.CreateTypeViewClass(elem)));

            return typeViewList;
        }
    }
}
