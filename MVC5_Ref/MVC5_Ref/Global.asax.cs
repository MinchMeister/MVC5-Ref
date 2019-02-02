using log4net;
using log4net.Config;
using MVC5_Ref.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MVC5_Ref.Utils;

namespace MVC5_Ref
{
    public class MvcApplication : HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        private UserLogonService logonService = new UserLogonService();

        protected void Application_Start()
        {
            try
            {
                XmlConfigurator.Configure();
                ConfigureRegistrations();
            }
            catch (Exception ex)
            {
                log.Fatal("Internal Application failed to start.", ex);
                HttpException lastErrorWrapper = Server.GetLastError() as HttpException;

                Exception lastError = lastErrorWrapper;

                if (lastError.InnerException != null)
                    lastError = lastError.InnerException;

                string lastErrorTypeName = lastError.GetType().ToString();
                string lastErrorMessage = lastError.Message;
                string lastErrorStackTrace = lastError.StackTrace;

                log.Error("Internal Application Failed to Start more details here. Error Type Name: " + lastErrorTypeName + Environment.NewLine +
                    "Error Message: " + lastErrorMessage + Environment.NewLine +
                    "Error Stack Trace: " + lastErrorStackTrace);
                throw;
            }
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            logonService.GetLogonUserInformation();

            Session.Timeout = 10;

            string sessionID = Session.SessionID;
        }

        protected void Session_End(Object sender, EventArgs e) { }

        protected void Application_Error()
        {
            //https://stackoverflow.com/questions/24932965/how-can-i-provide-a-handleerrorinfo-model-with-an-object

            var exception = Server.GetLastError();
            Response.Clear();
            Server.ClearError();

            if (!Response.IsRequestBeingRedirected)
            {
                if (exception == null)
                {
                    log.Fatal("Null error in Application_Error");
                    Response.Redirect("~/Error.html"); //send error message like => Error?Message='message'
                }
                if (exception is HttpUnhandledException) //this is a runtime code problem that are not wrapped in a try catch
                {
                    log.Fatal("HttpUnhandedException", exception);
                    Response.Redirect("~/Error.html"); //send error message like => Error?Message='message'
                }
                else if (exception is HttpException) //this is a status code like 404 500
                {
                    var httpEx = exception as HttpException;
                    log.Fatal("HttpException", httpEx);

                    switch (httpEx.GetHttpCode())
                    {
                        case 400:
                            Response.Redirect("PersonInfo/AccessDenied");
                            break;
                        case 401:
                            Response.Redirect("PersonInfo/AccessDenied");
                            break;
                        case 403;
                            Response.Redirect("Base/SessionLogout");
                            break;
                        case 404;
                            Response.Redirect("PersonInfo/NotFound");
                            break;
                        default:
                            Response.Redirect("PersonInfo/Error");
                            break;
                    }
                }
                else
                {
                    var ex = exception as Exception;
                    log.Error("Standard Exception", ex);
                    Response.Redirect("~/Error.html"); //send error message like => Error?Message='message'
                }
            }
        }


        private static void ConfigureRegistrations()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new ExceptionFilter());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(string), new TrimModelBinder());
        }
    }
}