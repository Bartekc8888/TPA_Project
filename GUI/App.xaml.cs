using System.Windows;

namespace GUI.View
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {

            this.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
        }
    }
}
