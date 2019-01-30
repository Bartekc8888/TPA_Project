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
using Model;
using Model.MetadataClasses;
using ReflectionModel;
using ViewModel.ModelRepresentation.Types;

namespace ViewModel.Logic
{
    [Export]
    public class TypesTreeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => {};
        private Reflector _reflector;
        private readonly SynchronizationContext _context;

        [Import]
        private Repository _repository = null;

        [Import]
        private IFileChooser _fileChooser = null;
        
        [Import]
        private ILogging _logger = null;

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
            _logger.Debug("Check if file path is correct");
            string extension = Path.GetExtension(SelectedPath);
            return File.Exists(SelectedPath) && (extension == ".dll" || extension == ".exe");
        }

        public void ChooseFile()
        {
            SelectedPath = _fileChooser.ChooseFilePath();
            _logger.Info("File was chosen");
        }

        public void ExtractData()
        {
            if (!String.IsNullOrEmpty(SelectedPath))
            {
                _reflector = new Reflector(SelectedPath);
                _repository._model = _reflector.AssemblyModel.ToModel();
                
                TypeViewModelAbstract item = ModelViewTypeFactory.CreateTypeViewClass(_reflector.AssemblyModel);
                SetNewData(item);
                _logger.Info("New data was set");
            }
        }

        public void SerializeData()
        {
            _logger.Debug("Start serialize data");
            if (_repository._model == null)
                return;

            _repository.Save(SerializationPath);

            _logger.Info("End serialize data");
        }

        public void DeserializeData()
        {
            _logger.Debug("Start deserialize data");

            _repository.Read(SerializationPath);
            _reflector.AssemblyModel = new AssemblyMetadata(_repository._model);
                
            TypeViewModelAbstract item = ModelViewTypeFactory.CreateTypeViewClass(_reflector.AssemblyModel);
            SetNewData(item);

            _logger.Info("End deserialize data");
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
