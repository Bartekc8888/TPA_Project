using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace ViewModel.Logic
{
    public abstract class TypeViewModelAbstract : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => {};

        public abstract string TypeName { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string IconPath { get; }
        public abstract bool HaveChildren { get; }

        public abstract IList<TypeViewModelAbstract> CreateChildren();
        
        public string FullIconPath => "pack://application:,,,/" + IconPath;

        private ObservableCollection<TypeViewModelAbstract> mChildren;
        public ObservableCollection<TypeViewModelAbstract> Children
        {
            get => mChildren;
            private set
            {
                mChildren = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Children)));
            }
        }
        
        public bool CanExpand => HasChildren();

        private bool _isExpanded;

        public bool IsExpanded
        {

            get
            {
                return _isExpanded;
            }
            set
            {
                if (_isExpanded != value)
                {
                    if (value)
                    {
                        Expand();
                    }
                    else
                    {
                        Collapse();
                    }

                    _isExpanded = value;
                }
            }
        }

        public TypeViewModelAbstract()
        {

            if (CanExpand)
            {
                Children = new ObservableCollection<TypeViewModelAbstract>();
                Children.Add(null);
            }
        }

        private void Collapse()
        {
            
        }

        private void Expand()
        {

            Children = new ObservableCollection<TypeViewModelAbstract>(CreateChildren());
        }

        private bool HasChildren()
        {

            return HaveChildren;
        }
        
    }
}
