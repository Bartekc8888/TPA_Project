﻿using Model.MetadataClasses;

namespace GUI.View.TypesView.ValueTypes
{
    public class EnumView : ValueView
    {
        public EnumView(TypeMetadata type, string name) : base(type, name)
        {
        }

        public override string Description => "Enumerator";
        public override string IconPath => "Icons/Enumerator.png";
    }
}
