/*****************************
 * 作者：xqx
 * 日期：2017年7月20日
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
using SteponTech.Data.CommonModels;
using SteponTech.Data.TrunkSystem;

namespace SteponTech.Services.CommonService
{
    public class RequestStatisticalService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public RequestStatisticalService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }

        /// <summary>
        /// 增加RequestStatistical
        /// </summary>
        public OperationResult AddRequestStatistical(RequestStatistical RequestStatistical)
        {
            var result = new OperationResult();

            if (_context.Create(RequestStatistical, true, true) != null)
            {
                result.State = OperationState.Success;
                result.Message = "添加成功";
            }
            else
            {
                result.State = OperationState.Faild;
                result.Message = "添加失败";
            }

            return result;
        }
        public RequestStatisticalByAll SelectRequestStatisticalBy(DateTime StartTime, DateTime EndTime)
        {
            var resultall = Context.RequestStatistical.ToList();
            //总访问量
            var RequestTotal = resultall.Count();
            //获取一周访问量
            var OneWeekRequest = resultall.Where(x => x.CreationDate >= StartTime && x.CreationDate <= EndTime).Count();
            //获取访问量
            var RequestStatisticalUrl = SelectRequestStatisticalByUrl().OrderByDescending(x => x.traffic).Take(5).ToList();
            var RequestStatisticalCountry = SelectRequestStatisticalByCountry(StartTime, EndTime).OrderByDescending(x => x.traffic).Take(5).ToList();


            //获取Ip
            //RequestStatisticalBy rbnew1 = new RequestStatisticalBy();
            //rbnew1.request = StartTime.ToString("yyyy-MM-dd");
            //rbnew1.traffic = SelectRequestStatisticalByIpMac(resultall, StartTime, StartTime.AddDays(1)).ToString();
            //RequestStatisticalIp.Add(rbnew1);
            var data = new RequestStatisticalByAll { RequestTotal= RequestTotal, OneWeekRequest= OneWeekRequest, RequestStatisticalUrl = RequestStatisticalUrl, RequestStatisticalCountry = RequestStatisticalCountry };
            return data;
        }

        /// <summary>
        /// 查询RequestStatistical访问量
        /// </summary>
        public List<RequestStatisticalBy> SelectRequestStatisticalByUrl()
        {
            string sql = "select \"UrlName\" as request,count(0) as traffic from \"RequestStatistical\" group by \"UrlName\" having count(\"UrlName\") > 0";
            return _context.Database.GetDbConnection().Query<RequestStatisticalBy>(sql).ToList();
        }

        /// <summary>
        /// 查询RequestStatistical访问量
        /// </summary>
        public List<RequestStatisticalBy> SelectRequestStatisticalByCountry(DateTime StartTime, DateTime EndTime)
        {
            string sql = "select \"RequestCountry\" as request,count(0) as traffic from \"RequestStatistical\" where \"CreationDate\">='" + StartTime + "' and \"CreationDate\"<'" + EndTime + "' group by \"RequestCountry\"";
            return _context.Database.GetDbConnection().Query<RequestStatisticalBy>(sql).ToList();
        }
    }
}
