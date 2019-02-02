using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVC5_Ref.Utils
{
    public static class AppSettingsGet
    {
        internal static string MailHost
        {
            get { return ConfigurationManager.AppSettings["keyName"]; }
        }
    }
}