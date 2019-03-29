using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sys.Application.app.Controllers
{
    public class ActionExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            var actionName = filterContext.RouteData.GetRequiredString("action");
            var controllerName = filterContext.RouteData.GetRequiredString("controller"); 

        }
    }
}