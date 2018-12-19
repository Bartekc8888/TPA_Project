using MEF;
using System.Windows;

namespace GUI
{
    public class GuiBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (MainWindow)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void OnInitialized()
        {
            //to do
        }
    }
}
