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
            TreeViewItem item = new TreeViewItem();
            item.Header = "Some class";
            item.Tag = "Icons/Class.png";

            item.Items.Add(null);
            item.Expanded += ItemExpanded;

            ReflectionTreeView.Items.Add(item);
        }

        private void ItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            // check if not initialized
            if (item.Items.Count == 1 && item.Items[0] == null)
            {
                item.Items.Clear();

                string typeName = (string)item.Tag;
            }
        }
    }
}
