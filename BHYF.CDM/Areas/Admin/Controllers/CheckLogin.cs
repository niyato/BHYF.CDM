using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace BHYF.CDM.Areas.Admin.Controllers
{
    public class CheckLogin : ActionFilterAttribute
    {
        /// <summary>
        /// 检测session是否失效
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                if (filterContext.HttpContext.Session["personinfo"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "manage", Action = "LogOff" }));//这里是跳转到Account下的LogOff,自己定义
                }
                if (filterContext.HttpContext.Session.IsNewSession)
                {
                    var sessionCookie = filterContext.HttpContext.Request.Headers["Cookie"];
                    if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET_SessionId", StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "manage", Action = "LogOff" }));//这里是跳转到Account下的LogOff,自己定义
                    }
                }
            }
        }
    }
}