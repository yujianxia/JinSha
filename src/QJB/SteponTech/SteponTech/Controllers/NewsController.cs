using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Data.CommonModels;
using SteponTech.Services.BaseServices;
using SteponTech.Services.CommonService;
using System;
using System.Linq;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 最新消息
    /// </summary>
    public class NewsController : BaseController<NewsController, SteponContext>
    {
        /// <summary>
        /// 最新消息列表
        /// </summary>
        /// <returns></returns>
        public IActionResult NewsList()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            var webConfigs = new WebConfig();
            var columId = "";
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var colunmsService = new ColunmsService(Services);
                var colunms = colunmsService.GetColunmsYoungByName("最新消息");
                if (colunms != null)
                {
                    columId = colunms.Id.ToString();
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Footer = webConfigs;
            ViewBag.ColumId = columId;
            return View();
        }
        /// <summary>
        /// 最新消息详情
        /// </summary>
        /// <param name="id">消息Id</param>
        /// <returns></returns>
        public IActionResult NewsInfo(string id)
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            var webConfigs = new WebConfig();
            var info = new InformationYoung();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                if (!string.IsNullOrEmpty(id))
                {
                    var informationService = new InformationService(Services);
                    info = informationService.GetInformationById(Guid.Parse(id));
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Footer = webConfigs;
            return View(info);
        }
    }
}