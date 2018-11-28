using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
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

        public ICommand ExpandCommand { get; set; }

        public TypesTreeViewModel()
        {
            Log.Info("Creating TreeVIewModel");

            SelectedPath = "";
            Items = new ObservableCollection<TypesTreeItemViewModel>();
            ExpandCommand = new RelayCommand(ExtractData);
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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Items.Clear();
                    Items.Add(item);
                });
            }
        }
    }
}
