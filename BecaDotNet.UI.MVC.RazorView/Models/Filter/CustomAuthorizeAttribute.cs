using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BecaDotNet.UI.MVC.RazorView.Models.Filter
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute 
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["CurrentUser"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    { "Controller", "Login" },
                    { "Action", "Index" },
                    { "returnUrl", HttpContext.Current.Request.RawUrl }
                });
        }
    }
}