using log4net;
using System;
using System.Web;
using System.Web.Mvc;

namespace MVC5_Ref.Utils
{
    public class ExceptionFilter : IExceptionFilter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ExceptionFilter));

        //https://aspnetwebstack.codeplex.com/SourceControl/latest#src/System.Web.Mvc/HandleErrorAttribute.cs
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");
            if (filterContext.IsChildAction) return;
            if (filterContext.ExceptionHandled) return;

            Exception exception = filterContext.Exception;
            if (new HttpException(null, exception).GetHttpCode() != 500) return;

            log.Error(exception);

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml",
                MasterName = "~/Views/Shared/Partials/_Layout.cshtml",
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            //filterContext.HttpContext.Response.StatusCode = 500;

            //Certain version of IIS will sometimes use their own error page when they detect a server error. Setting this property indicates that we want it to try to render ASP.NET MVC's error page instead.

            //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}