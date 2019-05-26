using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameShop.Extra
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "home", action = "index" }));
        }
    }
}