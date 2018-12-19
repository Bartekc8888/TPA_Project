using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Win32;
using ViewModel.Logic;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    [Export()]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //TypesTreeViewModel viewModel = new TypesTreeViewModel(new DialogFileChooser());
            //DataContext = viewModel;
        }

        [Import]
        public TypesTreeViewModel TypesTreeViewModel
        {
            set { DataContext = value; }
            get { return DataContext as TypesTreeViewModel; }
        }
    }
}
