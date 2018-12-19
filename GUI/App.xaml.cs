using MEF;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        GuiBootstrapper mefBootstrapper = new GuiBootstrapper();

        protected override void OnStartup(StartupEventArgs e)
        {
            mefBootstrapper.Run();
        }
    }
}
