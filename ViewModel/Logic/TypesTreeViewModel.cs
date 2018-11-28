using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;
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
        
        private string _serializationPath;
        public string SerializationPath
        {
            get => _serializationPath;
            set
            {
                _serializationPath = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SerializationPath)));
            }
        }

        public ICommand AnalyzeCommand { get; set; }

        public ICommand SerializeCommand { get; set; }
        public ICommand DeserializeCommand { get; set; }

        private object _itemsLock;

        public TypesTreeViewModel()
        {
            Log.Info("Creating TreeVIewModel");

            SelectedPath = "";
            SerializationPath = "";
            Items = new ObservableCollection<TypesTreeItemViewModel>();
            AnalyzeCommand = new RelayCommand(ExtractData);
            
            SerializeCommand = new RelayCommand(SerializeData);
            DeserializeCommand = new RelayCommand(DeserializeData);

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
                }
            }
        }

        public void SerializeData()
        {
            if (!String.IsNullOrEmpty(SerializationPath) && _items.Count > 0)
            {
                TypeViewAbstract typeViewAbstract = _items[0].CurrentType;
                // Serialize(typeViewAbstract, SerializationPath)
            }
        }

        public void DeserializeData()
        {
            if (!String.IsNullOrEmpty(SerializationPath))
            {
                // TypeViewAbstract typeViewAbstract = Deserialize(SerializationPath)
                
//                TypeViewAbstract view = ViewTypeFactory.CreateTypeViewClass(typeViewAbstract);
//                TypesTreeItemViewModel item = new TypesTreeItemViewModel(view);
//                
//                lock (_itemsLock)
//                {
//                    Items.Clear();
//                    Items.Add(item);
//                }
            }
        }
    }
}
