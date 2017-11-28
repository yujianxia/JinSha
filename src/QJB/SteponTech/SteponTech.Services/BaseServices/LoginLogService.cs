/*************************************
 * 作者：xqx
 * 日期：2017年7月13日
 * 操作：创建
 * ***********************************/
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Stepon.EntityFrameworkCore;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModel;

namespace SteponTech.Services.BaseService
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLogService : ServiceContract<SteponContext>
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private readonly SteponContext _context = null;

        public LoginLogService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }

        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="loginlog">登录日志信息</param>
        /// <returns></returns>
        public OperationResult AddLoginLog(LoginLog loginlog)
        {
            var result = new OperationResult();
            loginlog.Id = Guid.NewGuid();
            loginlog.CreationDate = loginlog.LastUpdate = DateTime.Now;
            var oldlogin = GetLoginLogByUserName(loginlog.UserName);
            if (oldlogin != null)
            {
                if (oldlogin.Address != loginlog.Address)
                {
                    loginlog.Message = "警告：本次登录的IP地址（" + loginlog.Address + "）和上一次登录的IP（" + oldlogin.Address + "）地址不一样！";
                }
            }
            var newlognlog = _context.Create(loginlog);
            if (newlognlog != null)
            {
                result.State = OperationState.Success;
                result.Message = "添加成功！";
            }
            else
            {
                result.State = OperationState.Faild;
                result.Message = "添加失败,请重试！";
            }

            return result;
        }

        /// <summary>
        /// 根据用户名获取登录日志
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public LoginLog GetLoginLogByUserName(string username)
        {
            return _context.LoginLog.OrderByDescending(e => e.CreationDate).FirstOrDefault(e => e.UserName == username);
        }

        /// <summary>
        /// 获取上次登录日志
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public LoginLog GetpreLoginLog(string username)
        {
            var log = _context.LoginLog.OrderByDescending(e => e.CreationDate).Where(e => e.UserName == username).Take(2).ToList();
            if (log.Any())
            {
                if (log.Count == 1)
                {
                    return log[0];
                }
                return log[1];
            }
            return null;
        }
    }
}
