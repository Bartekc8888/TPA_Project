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
        protected override Object CreateShell()
        {
            return Container.GetExportedValue<CommandLineView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            CommandLineView commandLine = (CommandLineView)this.Shell;
            commandLine.Run();
        }
    }
}
