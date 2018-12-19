using MEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CLI
{
    class CliBootstrapper : MefBootstrapper
    {
        public override void Run()
        {
            base.Run();
        }
        protected override DependencyObject CreateShell()
        {
            return /*this.Container.GetExportedValue<CommandLineView>();*/ null;
        }

        protected override void InitializeShell()
        {
            /*base.InitializeShell();
            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Show();*/
        }

        protected override void OnInitialized()
        {
            //to do
        }
    }
}
