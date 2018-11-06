using System.Collections.Generic;

namespace GUI.View.TypesView
{
    public abstract class TypeViewAbstract
    {
        public abstract string TypeName { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string IconPath { get; }
        public abstract bool HaveChildren { get; }

        public abstract IList<TypeViewAbstract> CreateChildren();
    }
}
