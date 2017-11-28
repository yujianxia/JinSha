using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using Microsoft.Extensions.Logging;
using SteponTech.Services.BaseServices;
using Microsoft.AspNetCore.Hosting;
using SteponTech.Data.CommonModels;
using SteponTech.Services.CommonService;
using SteponTech.Utils;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 参观指南
    /// </summary>
    public class VisitGuideController : BaseController<VisitGuideController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public VisitGuideController(IHostingEnvironment env)
        {
            Environment = env;
        }

        /// <summary>
        /// 参观指南首页
        /// </summary>
        /// <returns></returns>
        public IActionResult VisitGuide()
        {
            var heads = new List<Models>(); //母版页头数据
            var webConfigs = new WebConfig(); //母版页footer数据
            var banner = new BannerView();
            var colunms = new List<Colunms>();
            var informations = new List<Information>();
            var hereColunms = new List<Colunms>(); //交通信息栏目
            var serviceColunms=new List<Colunms>(); //服务信息栏目
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var modelsService = new ModelsService(Services);
                heads = modelsService.GetAllIsShowModels(); //母版页头数据
                var model = heads.FirstOrDefault(e => e.ModelName == "VISIT");
                var bannerService = new BannerService(Services);
                banner = bannerService.GetBannerViewByModelName(model.Id);
                var colunmsService = new ColunmsService(Services);
                var allColunms = colunmsService.GetAllColunmses();
                if (allColunms?.Count > 0)
                {
                    colunms = allColunms.Where(e => e.ModelId == model.Id && e.ColunmsId == Guid.Empty).OrderBy(e => e.Sorting).ToList(); ;
                    if (colunms?.Count > 0)
                    {
                        var allColunmsId = new List<Guid>();
                        var colunmsId = colunms.Select(e => e.Id).ToList();
                        allColunmsId.AddRange(colunmsId);
                        var here = colunms.FirstOrDefault(e => e.Name == "Getting Here");
                        if (here != null)
                        {
                            hereColunms = allColunms.Where(e => e.ColunmsId == here.Id).OrderBy(e => e.Sorting).ToList();
                            if (hereColunms?.Count > 0)
                            {
                                var hereColunmsId = hereColunms.Select(e => e.Id).ToList();
                                allColunmsId.AddRange(hereColunmsId);
                            }
                        }
                        var servicesInfo= colunms.FirstOrDefault(e => e.Name == "Services");
                        if (servicesInfo != null)
                        {
                            serviceColunms = allColunms.Where(e => e.ColunmsId == servicesInfo.Id).OrderBy(e => e.Sorting).ToList();
                            if (serviceColunms?.Count > 0)
                            {
                                var serviceColunmsId = serviceColunms.Select(e => e.Id).ToList();
                                allColunmsId.AddRange(serviceColunmsId);
                            }
                        }
                        var informationService = new InformationService(Services);
                        informations = informationService.GetInformationByColumnIds(allColunmsId);
                    }
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
            ViewBag.HereColunms = hereColunms;
            ViewBag.ServiceColunms = serviceColunms;
            return View();
        }
        /// <summary>
        /// 服务信息
        /// </summary>
        /// <param name="id">服务信息Id</param>
        /// <returns></returns>
        public IActionResult ServiceInfo(string id)
        {
            var info = new Information();
            var heads = new List<Models>(); //母版页头数据
            var webConfigs = new WebConfig(); //母版页footer数据
            var imgs = new List<string>(); //图片名称
            var visitColunms = new List<Colunms>();
            var relatedLinks = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(id))
            {
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
                    info = informationService.GetInformationById(Guid.Parse(id));
                    if (info != null)
                    {
                        if (!string.IsNullOrEmpty(info.InformationId))
                        {
                            var relatedLink = new RelatedLink(Services);
                            relatedLinks = relatedLink.GetLink(info.InformationId);
                        }
                        var dirPath = Path.Combine(Environment.WebRootPath, "upload", "Information", id);
                        if (Directory.Exists(dirPath))
                        {
                            var files = Directory.GetFiles(dirPath);
                            if (files?.Length > 0)
                            {
                                foreach (var file in files)
                                {
                                    var fileInfo = new FileInfo(file);
                                    var extension = fileInfo.Extension.ToLower();
                                    if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif")
                                    {
                                        if (fileInfo.Name != info.Photo)
                                        {
                                            imgs.Add(fileInfo.Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.LogInformation(e.Message + "\r\n");
                }
            }
            ViewBag.Head = heads;
            ViewBag.Footer = webConfigs;
            ViewBag.Imgs = imgs;
            ViewBag.VisitColunms = visitColunms;
            ViewBag.RelatedLinks = relatedLinks;
            return View(info);
        }


        /// <summary>
        /// 预约详情
        /// </summary>
        /// <returns></returns>
        public IActionResult Notice(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            InformationAll ifm = Context.InformationEnglishAll.Find(id);
            RelatedLink rl = new RelatedLink(Services);
            if (!String.IsNullOrEmpty(ifm.InformationId))
            {
                ViewBag.Aside = rl.GetLink(ifm.InformationId);
            }
            return View(ifm);
        }
    }
}