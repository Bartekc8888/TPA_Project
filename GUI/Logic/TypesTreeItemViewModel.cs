using GUI.View.TypesView;
using Model.MetadataDefinitions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace GUI.Logic
{
    public class TypesTreeItemViewModel : INotifyPropertyChanged
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => {};

        public TypeViewAbstract CurrentType { get; }
        public string TypeIcon { get { return CurrentType.IconPath; } }
        public string ItemName { get { return CurrentType.Name; } }
        public string ItemType { get { return CurrentType.TypeName; } }
        public string ItemDescription { get { return CurrentType.Description; } }

        private ObservableCollection<TypesTreeItemViewModel> mChildren;
        public ObservableCollection<TypesTreeItemViewModel> Children
        {
            get { return mChildren; }
            private set
            {
                mChildren = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Children)));
            }
        }

        public ICommand ExpandCommand { get; set; }
        public bool CanExpand { get { return HasChildren(); } }

        private bool isExpanded = false;

        public bool IsExpanded
        {

            get
            {
                return isExpanded;
            }
            set
            {
                if (isExpanded != value)
                {
                    if (value == true)
                    {
                        Expand();
                    }
                    else
                    {
                        Collapse();
                    }

                    isExpanded = value;
                }
            }
        }

        public TypesTreeItemViewModel(TypeViewAbstract type)
        {
            log.Info("Creating TypesTreeItemViewModel");

            CurrentType = type;

            ExpandCommand = new RelayCommand(Expand);
            if (CanExpand)
            {
                Children = new ObservableCollection<TypesTreeItemViewModel>();
                Children.Add(null);
            }
        }

        private void Collapse()
        {
            
        }

        private void Expand()
        {
            log.Info("Set members of current type");

            Children = new ObservableCollection<TypesTreeItemViewModel>(CurrentType.CreateChildren().Select(child => new TypesTreeItemViewModel(child)));
        }

        private bool HasChildren()
        {
            log.Info("Set members of current type");

            return CurrentType.HaveChildren;
        }
    }
}
