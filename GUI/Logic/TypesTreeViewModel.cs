using GUI.View.TypesView;
using Model.MetadataClasses;
using System.Collections.ObjectModel;

namespace GUI.Logic
{
    public class TypesTreeViewModel
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<TypesTreeItemViewModel> Items { get; set; }

        public TypesTreeViewModel()
        {
            log.Info("Creating TreeVIewModel");

            TypeViewAbstract view = ViewTypeFactory.CreateTypeViewClass(new TypeMetadata(typeof(TypeMetadata)));
            TypesTreeItemViewModel item = new TypesTreeItemViewModel(view);
            this.Items = new ObservableCollection<TypesTreeItemViewModel>() { item };
        }
    }
}
