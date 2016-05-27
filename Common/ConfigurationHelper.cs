using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigurationHelper
    {
        public static string AppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}
