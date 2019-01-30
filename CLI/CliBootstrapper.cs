using MEF;
using System;

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
