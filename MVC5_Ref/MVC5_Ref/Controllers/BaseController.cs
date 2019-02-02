using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using MVC5_Ref.Constants;
using MVC5_Ref.Utils;

namespace MVC5_Ref.Controllers
{
    [NoCache]
    public class BaseController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BaseController));

        //TODO HTTP Status Code Handler Class => in EAF

        public void SessionLogout()
        {
            ExpireAllCookies();
            Session.Clear();
            Session.Abandon();
            Response.Redirect(Routes.LogonPage);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult SessionExtend()
        {
            Session.Timeout = 10; //change to what site needs
            return new EmptyResult();
        }

        public ActionResult SessionExpire()
        {
            SessionLogout();
            return new EmptyResult();
        }


        public ActionResult SuccessfulEntry()
        {
            return View();
        }

        public ActionResult PreviouslySubmitted()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }

        //http://benfoster.io/blog/aspnet-mvc-custom-error-pages
        public ActionResult Error()
        {
            return View();
        }







        private void ExpireAllCookies()
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);
            }
        }




    }
}