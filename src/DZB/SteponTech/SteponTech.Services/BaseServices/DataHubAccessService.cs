/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using System.Collections.Generic;
using SteponTech.Data.BaseModels;

namespace SteponTech.Services.BaseServices
{
    public class DataHubAccessService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public DataHubAccessService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }

        public List<InformationAll> GetInformationAllByCondition(int index, int pageSize, bool isAll)
        {
            var all = _context.InformationAll.Where(x => x.ColumName == "金沙快讯").OrderByDescending(x => x.CreationDate).ToList();
            if (isAll)
                return all;
            return all.OrderByDescending(e => e.LastUpdate).Skip(index * pageSize).Take(pageSize).ToList();
        }
    }
}