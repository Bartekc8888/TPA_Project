using GUI.View.TypesView;
using Model.MetadataClasses;
using System.Collections.ObjectModel;

namespace GUI.Logic
{
    public class TypesTreeViewModel
    {
        public ObservableCollection<TypesTreeItemViewModel> Items { get; set; }

        public TypesTreeViewModel()
        {
            TypeViewAbstract view = ViewTypeFactory.CreateTypeViewClass(new TypeMetadata(typeof(TypeMetadata)));
            TypesTreeItemViewModel item = new TypesTreeItemViewModel(view);
            this.Items = new ObservableCollection<TypesTreeItemViewModel>() { item };
        }
    }
}
