using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using SteponTech.Data.BaseModels;

namespace SteponTech.Data
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 这里处理用户登录相关信息，例如添加用户关联信息到声明中，便于其他模块快速获取用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task OnSigningIn(CookieSigningInContext context)
        {
            //建立视图板块

            var userName = context.Principal.Identity.Name;
            var steponContext = context.HttpContext.RequestServices.GetService<SteponContext>();
            var contentlist = steponContext.Set<UserRolesView>().AsQueryable().AsNoTracking();
            var userInfo = contentlist.FirstOrDefault(e => e.UserName == userName);
            if (userInfo != null)
            {
                context.Principal.AddClaim("UserId", userInfo.Id);
                if (!string.IsNullOrEmpty(userInfo.RealName))
                {
                    //真实姓名
                    context.Principal.AddClaim("RealName", userInfo.RealName);
                }
                if (!string.IsNullOrEmpty(userInfo.roleid))
                {
                    //角色
                    context.Principal.AddClaim("RoleName", userInfo.Role);
                }
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 头像
        /// </summary>
        public System.String HeadPortrait { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public System.String RealName { get; set; }

        /// <summary>
        /// 权限id
        /// </summary>
        public System.String Role { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public System.String Sex { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public System.DateTime CreatTime { get; set; }

        /// <summary>
        /// 上次修改日期
        /// </summary>
        public System.DateTime LastUpdateTime { get; set; }
    }
}
