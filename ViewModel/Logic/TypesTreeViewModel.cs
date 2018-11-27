using ViewModel.View.TypesView;
using System.Collections.ObjectModel;
using ViewModel.ExtractionTools;

namespace ViewModel.Logic
{
    public class TypesTreeViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<TypesTreeItemViewModel> Items { get; }

        public TypesTreeViewModel()
        {
            Log.Info("Creating TreeVIewModel");

            AssemblyExtractor assemblyExtractor = new AssemblyExtractor(@"C:\Users\barte\Desktop\tpaProject\TPA_Project\GUI\bin\Debug\log4net.dll");
            TypeViewAbstract view = ViewTypeFactory.CreateTypeViewClass(assemblyExtractor.AssemblyModel);
            TypesTreeItemViewModel item = new TypesTreeItemViewModel(view);
            Items = new ObservableCollection<TypesTreeItemViewModel> { item };
        }
    }
}
