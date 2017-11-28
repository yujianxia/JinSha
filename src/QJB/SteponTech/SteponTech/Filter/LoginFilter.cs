using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SteponTech.Filter
{
    /// <summary>
    /// 登录验证
    /// </summary>
    public class LoginFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 验证
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("loginsession") == null)
            {
filterContext.Result = new RedirectResult("/Member/Login");
                //filterContext.HttpContext.Response.WriteAsync("<meta charset='UTF-8'>");
                //filterContext.HttpContext.Response.WriteAsync("<script>alert('No Login!');window.location.href='/Member/Login';</script>");
                //filterContext.Result = new ContentResult()
                //{
                //    Content = "<script>alert('第二种方式,有白屏！')</script>"
                //};
            }
        }
    }
}

