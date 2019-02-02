using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5_Ref.Utils
{
    public static class SessionWrapper
    {
        public static string UserId
        {
            get { return HttpContext.Current.Session["UserID"].ToString(); }
            internal set { HttpContext.Current.Session["UserID"] = value; }
        }
    }
}