using MEF;
using System.Windows;
using System.ComponentModel.Composition.Hosting;

namespace GUI
{
    public class GuiBootstrapper : MefBootstrapper
    {
        public override void Run()
        {
            base.Run();
        }
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void OnInitialized()
        {
            //to do
        }
    }
}
