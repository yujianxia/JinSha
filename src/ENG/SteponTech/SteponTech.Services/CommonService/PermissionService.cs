/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using System;
using System.Collections.Generic;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using SteponTech.Data.CommonModels;

namespace SteponTech.Services.CommonService
{
    public class PermissionService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public PermissionService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        #region 查询权限
        /// <summary>
        /// 根据角色查询权限信息
        /// </summary>
        /// <param name="roles">角色</param>
        /// <returns></returns>
        public List<string> GetMappingByRoles(List<string> roles)
        {
            var marks = new List<string>();
            var mappings = _context.Mapping.Where(e => roles.Contains(e.Role)).ToList();
            if (mappings?.Count > 0)
            {
                marks = mappings.Select(e => e.Mark).Distinct().ToList();
            }
            return marks;
        }
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public List<Permission> GetAllPermissions()
        {
            return _context.Permission.Where(e => e.Id != null).ToList();
        }
        /// <summary>
        /// 根据权限的标识获取权限
        /// </summary>
        /// <param name="marks">权限的标识</param>
        /// <returns></returns>
        public List<Permission> GetPermissionsByMarks(List<string> marks)
        {
            return _context.Permission.Where(e => marks.Contains(e.Mark)).ToList();
        }
        #endregion
    }
}

