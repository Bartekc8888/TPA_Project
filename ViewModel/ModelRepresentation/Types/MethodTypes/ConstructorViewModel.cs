﻿using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Model.MetadataClasses.Types.Members;
using ViewModel.Logic;

namespace ViewModel.ModelRepresentation.Types.MethodTypes
{
    public class ConstructorViewModel : MethodViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger
               (MethodBase.GetCurrentMethod().DeclaringType);

        public override string Description => "Constructor";
        public override string IconPath => "Icons/Method.png";
        public override bool HaveChildren => false;
        public override string TypeName => mTypeName;
        public override string Name => mName;

        private string mTypeName;
        private string mName;

        public ConstructorViewModel(ConstructorMetadata metadata) : base(metadata)
        {
            Log.Info("Creating Constructor View");
            mName = metadata.Name + GetParameters(metadata.Parameters);
            if (metadata.ReturnType != null)
            {
                mTypeName = metadata.ReturnType.TypeName;
            }
        }

        public override IList<TypeViewModelAbstract> CreateChildren()
        {
            Log.Error("Cannot create children");
            throw new NotSupportedException();
        }

    }
}