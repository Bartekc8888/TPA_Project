using MEF;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        AppBootstrapper mefBootstrapper = new AppBootstrapper();

        private void App_Startup(object sender, StartupEventArgs e)
        {
            mefBootstrapper.Run();
        }
    }
}
