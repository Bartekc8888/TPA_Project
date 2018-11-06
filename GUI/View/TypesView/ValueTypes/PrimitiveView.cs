using Model.MetadataClasses;

namespace GUI.View.TypesView.ValueTypes
{
    public class PrimitiveView : ValueView
    {
        public PrimitiveView(TypeMetadata type, string name) : base(type, name)
        {

        }

        public override string Description => "Primitive";
        public override string IconPath => "Icons/Value.png";
    }
}
