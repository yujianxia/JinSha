using System;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Services.CommonService;
using SteponTech.Data.BaseModels;
using Microsoft.Extensions.Logging;
using SteponTech.Services.BaseServices;
using System.Collections.Generic;
using SteponTech.Data.CommonModels;
using System.Linq;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 首页（青教版）
    /// </summary>
    public class HomeController : BaseController<HomeController,SteponContext>
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();

            var webConfigs = new WebConfig();
            var infomations = new List<InformationYoungView>();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var informationService = new InformationService(Services);
                infomations = informationService.GetAllInfoViewByColumName("走进金沙");
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Footer = webConfigs;
            ViewBag.Infos = infomations;
            return View();
        }
    }
}