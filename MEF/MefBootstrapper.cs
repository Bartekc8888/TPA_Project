using CommonServiceLocator;
using Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Windows;

namespace MEF
{
    public abstract class MefBootstrapper
    {
        protected ILogging Logger { get; set; } = null;
        protected DependencyObject Shell { get; set; } = null;
        protected AggregateCatalog AggregateCatalog { get; set; }
        protected CompositionContainer Container { get; set; }


        public virtual void Run()
        {
            this.Logger = this.CreateLogger();
            if (this.Logger == null)
                throw new InvalidOperationException("Null Logger Exception");
         
            this.AggregateCatalog = this.CreateAggregateCatalog();
            this.ConfigureAggregateCatalog();
            this.RegisterDefaultTypesIfMissing();
            this.Container = this.CreateContainer();

            if (this.Container == null)
                throw new InvalidOperationException("Null Container Exception");

            this.ConfigureContainer();
            this.RegisterFrameworkExceptionTypes();
            this.Shell = this.CreateShell();
            this.InitializeShell();
            this.OnInitialized();
        }


        protected virtual ILogging CreateLogger()
        {
            return new FileLogging("fileLogs.txt", "Logging");
        }

        protected virtual AggregateCatalog CreateAggregateCatalog()
        {
            return new AggregateCatalog(new DirectoryCatalog("."), new AssemblyCatalog(Assembly.GetExecutingAssembly()));
        }

        protected virtual void ConfigureAggregateCatalog()
        {

        }

        protected virtual void RegisterDefaultTypesIfMissing()
        {
            List<ComposablePartCatalog> catalog = GetDefaultComposablePartCatalog();
            if (this.AggregateCatalog != null)
            {
                catalog.Add(this.AggregateCatalog);
            }
            this.AggregateCatalog = new AggregateCatalog(catalog);
        }

        private List<ComposablePartCatalog> GetDefaultComposablePartCatalog()
        {
            return new List<ComposablePartCatalog>(new ComposablePartCatalog[] { new AssemblyCatalog(Assembly.GetAssembly(typeof(MefBootstrapper)))} );
        }

        protected virtual CompositionContainer CreateContainer()
        {
            CompositionContainer container = new CompositionContainer(this.AggregateCatalog);
            return container;
        }

        protected virtual void ConfigureContainer()
        {
            RegisterBootstrapperProvidedTypes();
        }

        protected virtual void RegisterBootstrapperProvidedTypes()
        {
            this.Container.ComposeExportedValue<ILogging>(this.Logger);
            this.Container.ComposeExportedValue<AggregateCatalog>(this.AggregateCatalog);
        }


        protected virtual void RegisterFrameworkExceptionTypes()
        {
            //to do
        }

        protected virtual DependencyObject CreateShell()
        {
            return null;
        }

        protected virtual void InitializeShell()
        {

        }

        protected virtual void OnInitialized()
        {

        }
    }
}
