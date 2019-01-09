using System.ComponentModel.Composition;
using System.Windows;
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
        }

        [Import]
        public TypesTreeViewModel TypesTreeViewModel
        {
            set { DataContext = value; }
            get { return DataContext as TypesTreeViewModel; }
        }
    }
}
