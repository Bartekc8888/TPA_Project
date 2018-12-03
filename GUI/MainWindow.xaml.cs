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

            TypesTreeViewModel viewModel = new TypesTreeViewModel();
            DataContext = viewModel;
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = ".exe",
                Filter = "Executable Files (*.exe)|*.exe|DLL Files (*.dll)|*.dll"
            };

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string filename = dialog.FileName;
                PathText.Text = filename;
            }
        }

        private void AnalizeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
