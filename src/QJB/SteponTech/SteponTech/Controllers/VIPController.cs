using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using Microsoft.AspNetCore.Hosting;
using SteponTech.Services.CommonService;
using SteponTech.Filter;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 会员
    /// </summary>
    /// <returns></returns>
    [LoginFilter]
    public class VIPController : BaseController<VIPController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public VIPController(IHostingEnvironment env)
        {
            Environment = env;
        }

        /// <summary>
        /// 会员章程
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 我的活动
        /// </summary>
        /// <returns></returns>
        public IActionResult MyActivity()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 我的积分
        /// </summary>
        /// <returns></returns>
        public IActionResult MyIntegral()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 我的兑换
        /// </summary>
        /// <returns></returns>
        public IActionResult MyExchange()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 我的兑换
        /// </summary>
        /// <returns></returns>
        public IActionResult MyInformation()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
    }
}