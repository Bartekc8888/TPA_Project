﻿using Model.MetadataClasses;
using Model.MetadataClasses.Types;

namespace ViewModel.View.TypesView.ValueTypes
{
    public abstract class ValueView : BaseTypeView
    {
        public ValueView(TypeMetadata type, string name) : base(type, name)
        {
        }
    }
}