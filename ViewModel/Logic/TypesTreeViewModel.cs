using System.Collections.ObjectModel;
using System.Reflection;
using log4net;
using ViewModel.ExtractionTools;
using ViewModel.View.TypesView;

namespace ViewModel.Logic
{
    public class TypesTreeViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

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
