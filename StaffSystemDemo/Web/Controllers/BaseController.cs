using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StaffSystemDemo.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"] as string;
            string action = filterContext.RouteData.Values["action"] as string;

            //filterContext.RequestContext.HttpContext.Response.Write();
            string msg = string.Format("{0}发生错误!{1}",  action, filterContext.Exception.Message);

            var dic=new RouteValueDictionary();
            dic.Add("controller", "Staff");
            dic.Add("action", "Error");
            dic.Add("msg", msg);
            dic.Add("id", "-1");
            filterContext.RequestContext.HttpContext.Response.RedirectToRoute(dic);
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }
    }
}
