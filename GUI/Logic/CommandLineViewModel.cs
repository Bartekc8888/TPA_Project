using GUI.View.TypesView;
using Model.MetadataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Logic
{
    public class CommandLineViewModel
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CommandLineViewModel()
        {
            log.Info("Creating CommandLineViewModel");

            TypeViewAbstract view = ViewTypeFactory.CreateTypeViewClass(new TypeMetadata(typeof(TypeMetadata)));
            CommandLineItemViewModel item = new CommandLineItemViewModel(view);
        }
    }
}
