/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using System.Collections.Generic;
using Dapper;
using SteponTech.Data.BaseModels;

namespace SteponTech.Services.BaseServices
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public UserService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        public List<UserRolesView> GeyUser_All()
        {
            return _context.UserRolesView.ToList();
        }
    }
}
