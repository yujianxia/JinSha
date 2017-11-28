using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SteponTech.Filter
{
    /// <summary>
    /// 登录验证
    /// </summary>
    public class VolFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 验证
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("loginsession") == null)
            {
filterContext.Result = new RedirectResult("/VIP/Login?type=1");
                //filterContext.HttpContext.Response.WriteAsync("<head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /></head>");
                //filterContext.HttpContext.Response.WriteAsync("window.location.href='/Vip/Login';</script>");
                //filterContext.Result = new ContentResult()
                //{
                //    Content = "<script>alert('第二种方式,有白屏！')</script>"
                //};
            }
        }
    }
}

