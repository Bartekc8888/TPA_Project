using ViewModel.View.TypesView;
using Model.MetadataClasses;
using System.Collections.ObjectModel;

namespace ViewModel.Logic
{
    public class TypesTreeViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<TypesTreeItemViewModel> Items { get; set; }

        public TypesTreeViewModel()
        {
            Log.Info("Creating TreeVIewModel");

            TypeViewAbstract view = ViewTypeFactory.CreateTypeViewClass(new TypeMetadata(typeof(TypeMetadata)));
            TypesTreeItemViewModel item = new TypesTreeItemViewModel(view);
            Items = new ObservableCollection<TypesTreeItemViewModel> { item };
        }
    }
}
