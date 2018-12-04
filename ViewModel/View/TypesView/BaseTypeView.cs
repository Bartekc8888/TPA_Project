using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Model.MetadataClasses.Types;
using ViewModel.Logic;

namespace ViewModel.View.TypesView
{
    public abstract class BaseTypeView : TypeViewAbstract
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        protected TypeMetadata mTypeMetadata;
        protected string mName;

        public BaseTypeView(TypeMetadata type, string name)
        {
            Log.Debug("Creating BaseType View");

            mTypeMetadata = type;
            mName = name;
        }

        public override string TypeName => mTypeMetadata.TypeBasicInfo.TypeName;
        public override string Name => mName;
        public override bool HaveChildren => true;

        public override IList<TypeViewAbstract> CreateChildren()
        {
            Log.Debug("Set members");

            List<TypeViewAbstract> typeViewList = new List<TypeViewAbstract>();

            typeViewList.AddRange(mTypeMetadata.Constructors.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Methods.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Properties.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Indexers.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Fields.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.NestedTypes.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            typeViewList.AddRange(mTypeMetadata.Events.Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));

            return typeViewList;
        }
    }
}
