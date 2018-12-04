using System.Windows;
using Microsoft.Win32;
using ViewModel.Logic;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TypesTreeViewModel viewModel = new TypesTreeViewModel(new DialogFileChooser());
            DataContext = viewModel;
        }
    }
}
