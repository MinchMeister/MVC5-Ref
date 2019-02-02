using System.Web;
using System.Web.Mvc;

namespace MVC5_Ref.Utils
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly string[] AccessLevel;

        public CustomAuthorizationAttribute(params string[] roles)
        {
            this.AccessLevel = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            foreach (var i in AccessLevel)
            {
                if (httpContext.Session[i] != null && httpContext.Session[i].Equals(true))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}