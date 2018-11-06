﻿using Model.MetadataClasses;

namespace GUI.View.TypesView.ValueTypes
{
    public class StructureView : ValueView
    {
        public StructureView(TypeMetadata type, string name) : base(type, name)
        {

        }

        public override string Description => "Structure";
        public override string IconPath => "Icons/Structure.png";
    }
}
