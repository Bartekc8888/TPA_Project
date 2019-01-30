using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Interfaces;
using Model.MetadataClasses;
using ViewModel.ExtractionTools;
using ViewModel.ModelRepresentation.Types;

namespace ViewModel.Logic
{
    [Export]
    public class TypesTreeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => {};
        private AssemblyMetadata _assemblyModel;
        private readonly SynchronizationContext _context;

        [Import]
        private IFileChooser _fileChooser;
        
        [Import]
        private ILogging logger;

        [Import]
        private ISerialization _serializer;
        
        private ObservableCollection<TypeViewModelAbstract> _items;
        public ObservableCollection<TypeViewModelAbstract> Items
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
        public ICommand ChooseAnalyzePathCommand { get; set; }

        public ICommand SerializeCommand { get; set; }
        public ICommand DeserializeCommand { get; set; }

        public TypesTreeViewModel()
        {            
            _context = SynchronizationContext.Current;
            
            SelectedPath = "";
            SerializationPath = "";
            Items = new ObservableCollection<TypeViewModelAbstract>();
            AnalyzeCommand = new RelayCommand(ExtractData);
            ChooseAnalyzePathCommand = new RelayCommand(ChooseFile);

            SerializeCommand = new RelayCommand(SerializeData);
            DeserializeCommand = new RelayCommand(DeserializeData);
        }

        public bool IsPathValid()
        {
            logger.Debug("Check if file path is correct");
            string extension = Path.GetExtension(SelectedPath);
            return File.Exists(SelectedPath) && (extension == ".dll" || extension == ".exe");
        }

        public void ChooseFile()
        {
            SelectedPath = _fileChooser.ChooseFilePath();
            logger.Info("File was chosen");
        }

        public void ExtractData()
        {
            if (!String.IsNullOrEmpty(SelectedPath))
            {
                _assemblyModel = new AssemblyExtractor(SelectedPath).AssemblyModel;
                
                TypeViewModelAbstract item = ModelViewTypeFactory.CreateTypeViewClass(_assemblyModel);
                SetNewData(item);
                logger.Info("New data was set");
            }
        }

        public void SerializeData()
        {
            logger.Debug("Start serialize data");

            _serializer.Save(_assemblyModel.ToModel(), SerializationPath);

            logger.Info("End serialize data");
        }

        public void DeserializeData()
        {
            logger.Debug("Start deserialize data");

            _assemblyModel = new AssemblyMetadata(_serializer.Read(SerializationPath));
                
            TypeViewModelAbstract item = ModelViewTypeFactory.CreateTypeViewClass(_assemblyModel);
            SetNewData(item);

            logger.Info("End deserialize data");
        }

        private void SetNewData(TypeViewModelAbstract item)
        {
            if (_context != null)
            {
                _context.Post(delegate
                {
                    Items.Clear();
                    Items.Add(item);
                }, null);
            }
            else
            {
                Items.Clear();
                Items.Add(item);
            }
        }
    }
}
