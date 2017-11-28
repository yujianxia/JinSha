using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SteponTech.Services.BaseServices;
using Microsoft.Extensions.Logging;
using SteponTech.Data.BaseModels;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.CommonModels;
using SteponTech.Services.CommonService;
using SteponTech.Utils;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 关于金沙
    /// </summary>
    public class AboutController : BaseController<AboutController, SteponContext>
    {
        /// <summary>
        /// 关于金沙首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Intosands()
        {
            var heads = new List<Models>();
            var webConfigs = new WebConfig();
            var banner = new BannerView();
            var colunms = new List<Colunms>();
            var informations = new List<Information>();
            var visitColunms = new List<Colunms>();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var modelsService = new ModelsService(Services);
                heads = modelsService.GetAllIsShowModels(); //母版页头数据
                var model = heads.FirstOrDefault(e => e.ModelName == "ABOUT");
                var visitModel = heads.FirstOrDefault(e => e.ModelName == "VISIT");
                var bannerService = new BannerService(Services);
                banner = bannerService.GetBannerViewByModelName(model.Id);
                var colunmsService = new ColunmsService(Services);
                colunms = colunmsService.GetColunmsByModelId(model.Id);
                visitColunms = colunmsService.GetColunmsByModelId(visitModel.Id);
                if (colunms?.Count > 0)
                {
                    var colunmsId = colunms.Select(e => e.Id).ToList();
                    var informationService = new InformationService(Services);
                    informations = informationService.GetInformationByColumnIds(colunmsId);
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Head = heads;
            ViewBag.Footer = webConfigs;
            ViewBag.Banner = banner;
            ViewBag.Colunms = colunms;
            ViewBag.Informations = informations;
            ViewBag.VisitColunms = visitColunms;
            return View();
        }
        /// <summary>
        /// 金沙简介 
        /// </summary>
        /// <returns></returns>
        public IActionResult Introduce()
        {
            var info = new InformationAll();
            var heads = new List<Models>();
            var webConfigs = new WebConfig();
            var visitColunms = new List<Colunms>();
            var relatedLinks = new Dictionary<string, string>();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var modelsService = new ModelsService(Services);
                heads = modelsService.GetAllIsShowModels(); //母版页头数据
                var visitModel = heads.FirstOrDefault(e => e.ModelName == "VISIT");
                var colunmsService = new ColunmsService(Services);
                visitColunms = colunmsService.GetColunmsByModelId(visitModel.Id);
                var informationService = new InformationService(Services);
                info = informationService.GetInformationAllByColumName("About Us");
                if (!string.IsNullOrEmpty(info?.InformationId))
                {
                    var relatedLink = new RelatedLink(Services);
                    relatedLinks = relatedLink.GetLink(info.InformationId);
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Head = heads;
            ViewBag.Footer = webConfigs;
            ViewBag.VisitColunms = visitColunms;
            ViewBag.RelatedLinks = relatedLinks;
            return View(info);
        }
    }
}