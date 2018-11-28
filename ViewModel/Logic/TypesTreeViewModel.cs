using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using log4net;
using ViewModel.ExtractionTools;
using ViewModel.View.TypesView;

namespace ViewModel.Logic
{
    public class TypesTreeViewModel : INotifyPropertyChanged
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => {};

        private ObservableCollection<TypesTreeItemViewModel> _items;
        public ObservableCollection<TypesTreeItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }

        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedPath)));
            }
        }

        public ICommand ExpandCommand { get; set; }
        private object _itemsLock;

        public TypesTreeViewModel()
        {
            Log.Info("Creating TreeVIewModel");

            SelectedPath = "";
            Items = new ObservableCollection<TypesTreeItemViewModel>();
            ExpandCommand = new RelayCommand(ExtractData);

            _itemsLock = new object();
            BindingOperations.EnableCollectionSynchronization(Items, _itemsLock);
        }
        
        public bool IsPathValid()
        {
            string extension = Path.GetExtension(SelectedPath);
            return File.Exists(SelectedPath) && (extension == ".dll" || extension == ".exe");
        }

        public void ExtractData()
        {
            if (!String.IsNullOrEmpty(SelectedPath))
            {
                AssemblyExtractor assemblyExtractor = new AssemblyExtractor(SelectedPath);
                TypeViewAbstract view = ViewTypeFactory.CreateTypeViewClass(assemblyExtractor.AssemblyModel);
                TypesTreeItemViewModel item = new TypesTreeItemViewModel(view);
                
                lock (_itemsLock)
                {
                    Items.Clear();
                    Items.Add(item);
                };
            }
        }
    }
}
