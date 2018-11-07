using GUI.Logic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GUI.View
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
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".exe",
                Filter = "Executable Files (*.exe)|*.exe|DLL Files (*.dll)|*.dll"
            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dialog.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dialog.FileName;
                this.PathText.Text = filename;
            }
        }

        private void AnalizeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ItemExpanded(object sender, RoutedEventArgs e)
        {

        }
    }
}
