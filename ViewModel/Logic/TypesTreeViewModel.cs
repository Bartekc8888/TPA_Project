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
            Log.Info("Creating TreeViewModel");

            AssemblyExtractor assemblyExtractor = new AssemblyExtractor(@"C:\Users\Michal\Desktop\TPA_Project\CLI\bin\Debug\log4net.dll");
            TypeViewAbstract view = ViewTypeFactory.CreateTypeViewClass(assemblyExtractor.AssemblyModel);
            IConventer xml = new XmlConventer();
            xml.saveToFile(assemblyExtractor.AssemblyModel, "xmlfile.xml");
            TypesTreeItemViewModel item = new TypesTreeItemViewModel(view);
            Items = new ObservableCollection<TypesTreeItemViewModel> { item };
        }
    }
}
