using System.Collections.Generic;
using Model.MetadataClasses;

namespace GUI.View.TypesView.ValueTypes
{
    public abstract class ValueView : BaseTypeView
    {
        public ValueView(TypeMetadata type, string name) : base(type, name)
        {
        }
    }
}
