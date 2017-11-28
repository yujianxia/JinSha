/********************************
 * 作者：xqx
 * 日期：2017年6月23日
 * 操作：创建
 * *******************************/
using System;
/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using System.Collections.Generic;
using Dapper;
using SteponTech.Data.BaseModels;
using SteponTech.Data.CommonModel;
using Stepon.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SteponTech.Services.CommonService
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class SystemLogService : ServiceContract<SteponContext>
    {
        /// <summary>
        /// 数据库连接实例
        /// </summary>
        private readonly SteponContext _context = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public SystemLogService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        #region 添加系统日志

        /// <summary>
        /// 添加系统日志
        /// </summary>
        /// <param name="systemlog">日志信息</param>
        /// <returns></returns>
        public bool AddLog(SystemLog systemlog)
        {
            try
            {
                try
                {
                    UserRolesView user = _context.UserRolesView.FirstOrDefault(e => e.UserName == systemlog.UserName);
                    systemlog.RealName = user.Role;
                }
                catch (Exception)
                {
                    // ignored
                }
                systemlog.Id = Guid.NewGuid();
                return _context.Create(systemlog, true, true) != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
