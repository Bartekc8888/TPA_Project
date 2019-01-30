using System;
using System.Collections.Generic;
using System.Configuration;

namespace MEF
{
    public class MefChooser<T> : List <T>
    {
        public T Imported()
        {
            var settings = ConfigurationManager.AppSettings;
            string name = typeof(T).Name;
            string result = settings[name];
            return Find(item => item.GetType().Name == result);
        }
    }
}