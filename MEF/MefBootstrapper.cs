﻿using CommonServiceLocator;
using Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows;
using Interfaces;

namespace MEF
{
    public abstract class MefBootstrapper
    {
        protected ILogging Logger { get; set; }
        protected Object Shell { get; set; }
        protected AggregateCatalog AggregateCatalog { get; set; }
        protected CompositionContainer Container { get; set; }


        public virtual void Run()
        {
            Logger = CreateLogger();
            if (Logger == null)
                throw new InvalidOperationException("Null Logger Exception");
         
            AggregateCatalog = CreateAggregateCatalog();
            RegisterDefaultTypesIfMissing();
            Container = CreateContainer();

            if (Container == null)
                throw new InvalidOperationException("Null Container Exception");

            ConfigureContainer();
            Container.ComposeParts(this);
            
            Shell = CreateShell();
            InitializeShell();
            OnInitialized();
        }


        protected virtual ILogging CreateLogger()
        {
            return new FileLogging("fileLogs.txt", "Logging");
        }

        protected virtual AggregateCatalog CreateAggregateCatalog()
        {
            NameValueCollection paths = (NameValueCollection)ConfigurationManager.GetSection("paths");
            string[] pathsCatalogs = paths.AllKeys;
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            foreach (string pathsCatalog in pathsCatalogs)
            {
                if (Directory.Exists(pathsCatalog))
                    directoryCatalogs.Add(new DirectoryCatalog(pathsCatalog));
            }
            directoryCatalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.exe"));
            
            return new AggregateCatalog(directoryCatalogs);
        }

        protected virtual void RegisterDefaultTypesIfMissing()
        {
            List<ComposablePartCatalog> catalog = GetDefaultComposablePartCatalog();
            if (AggregateCatalog != null)
            {
                catalog.Add(AggregateCatalog);
            }
            AggregateCatalog = new AggregateCatalog(catalog);
        }

        private List<ComposablePartCatalog> GetDefaultComposablePartCatalog()
        {
            return new List<ComposablePartCatalog>(new ComposablePartCatalog[] { new AssemblyCatalog(Assembly.GetAssembly(typeof(MefBootstrapper)))} );
        }

        protected virtual CompositionContainer CreateContainer()
        {
            CompositionContainer container = new CompositionContainer(AggregateCatalog);
            return container;
        }

        protected virtual void ConfigureContainer()
        {
            RegisterBootstrapperProvidedTypes();
        }

        protected virtual void RegisterBootstrapperProvidedTypes()
        {
            Container.ComposeExportedValue(Logger);
            Container.ComposeExportedValue(AggregateCatalog);
        }

        protected virtual Object CreateShell()
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
