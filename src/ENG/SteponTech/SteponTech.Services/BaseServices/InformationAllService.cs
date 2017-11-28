/*****************************
 * 作者：xqx
 * 日期：2017年7月21日
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
using Stepon.EntityFrameworkCore;

namespace SteponTech.Services.BaseServices
{
    public class InformationAllService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public InformationAllService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有链接列表
        /// </summary>
        public List<InformationAll> GeyInformationAll_All()
        {
            return _context.InformationEnglishAll.ToList();
        }
        /// <summary>
        /// 根据ID获取链接信息
        /// </summary>
        public InformationAll GetInformationAllById(Guid id)
        {
            return _context.InformationEnglishAll.FirstOrDefault(e => e.Id == id);
        }

    }
}
