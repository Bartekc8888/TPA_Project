﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GUI.View.TypesView;
using Model.MetadataClasses;
using Model.MetadataClasses.Types.Members;

namespace TPA_project.View.TypesView.MethodTypes
{
    public class PropertyView : TypeViewAbstract
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        PropertyMetadata metadata;
        public override string Description => "Property";
        public override string IconPath => "Icons/Property.png";
        public override bool HaveChildren => true;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public PropertyView(PropertyMetadata metadata) : base()
        {
            log.Debug("Creating Property View");

            this.metadata = metadata;
            mName = metadata.Name;
            if (metadata.TypeMetadata != null)
            {
                mTypeName = metadata.TypeMetadata.TypeName;
            }
        }

        public override IList<TypeViewAbstract> CreateChildren()
        {
            log.Debug("Set members");

            List<TypeViewAbstract> typeViewList = new List<TypeViewAbstract>();

            typeViewList.AddRange(EmitMethod(metadata.propertyMethods).Select(elem => ViewTypeFactory.CreateTypeViewClass(elem)));
            
            return typeViewList;
        }

        private IEnumerable<MethodMetadata> EmitMethod(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   select new MethodMetadata(_currentMethod);
        }
    }
}