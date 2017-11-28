using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using Microsoft.Extensions.Logging;
using SteponTech.Data.CommonModels;
using System;
using System.Collections.Generic;
using SteponTech.Services.CommonService;
using SteponTech.Services.BaseServices;
using System.IO;
using System.Linq;
using SteponTech.Utils;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Index()
        {
            var heads = new List<Models>(); //母版页头数据
            var webConfigs = new WebConfig(); //母版页footer数据
            var banner = new BannerView();
            var colunms = new List<Colunms>();
            var informations = new List<Information>();
            var hereColunms = new List<Colunms>(); //交通信息栏目
            var serviceColunms = new List<Colunms>(); //服务信息栏目
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var modelsService = new ModelsService(Services);
                heads = modelsService.GetAllIsShowModels(); //母版页头数据
                var model = heads.FirstOrDefault(e => e.ModelName == "参观指南");
                var bannerService = new BannerService(Services);
                banner = bannerService.GetBannerViewByModelName(model.Id);
                var colunmsService = new ColunmsService(Services);
                var allColunms = colunmsService.GetAllColunmses();
                if (allColunms?.Count > 0)
                {
                    colunms = allColunms.Where(e => e.ModelId == model.Id && e.ColunmsId == Guid.Empty).OrderBy(e => e.Sorting).ToList(); 
                    if (colunms?.Count > 0)
                    {
                        var allColunmsId = new List<Guid>();
                        var colunmsId = colunms.Select(e => e.Id).ToList();
                        allColunmsId.AddRange(colunmsId);
                        var here = colunms.FirstOrDefault(e => e.Name == "交通信息");
                        if (here != null)
                        {
                            hereColunms = allColunms.Where(e => e.ColunmsId == here.Id).OrderBy(e => e.Sorting).ToList();
                            if (hereColunms?.Count > 0)
                            {
                                var hereColunmsId = hereColunms.Select(e => e.Id).ToList();
                                allColunmsId.AddRange(hereColunmsId);
                            }
                        }
                        var servicesInfo = colunms.FirstOrDefault(e => e.Name == "服务信息");
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
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            ViewBag.Head = heads;
            ViewBag.Footer = webConfigs;
            ViewBag.Banner = banner;
            ViewBag.Colunms = colunms;
            ViewBag.Informations = informations;
            ViewBag.HereColunms = hereColunms;
            ViewBag.ServiceColunms = serviceColunms;








            //获取新增栏目  
            var newcol = Context.ColunmsView.Where(x => x.ModelName == "参观指南" && x.IsNew == true).ToList();
            if (newcol.Count > 0)
            {
                var newinfo = new List<InformationAll>();
                foreach (var col in newcol)
                {
                    var info = Context.InformationAll.Where(x => x.ColumName == col.Name).ToList();
                    if (info != null)
                    {
                        newinfo.AddRange(info);
                    }

                }
                if (newinfo.Count > 0)
                {
                    ViewBag.NewCol = newinfo;
                }

            }








            return View();
        }



        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public IActionResult Intro(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            var information = Context.InformationAll.Find(id);
            RelatedLink rl = new RelatedLink(Services);
            if (!String.IsNullOrEmpty(information.InformationId))
            {
                ViewBag.Aside = rl.GetLink(information.InformationId);
            }

            List<string> filelist = new List<string>();
            var filepath = Environment.WebRootPath;  //文件存储路径
            filepath = Path.Combine(filepath, "upload\\Information\\" + information.Id);
            if (Directory.Exists(filepath))
            {
                DirectoryInfo theFolder = new DirectoryInfo(filepath);
                FileInfo[] Files = theFolder.GetFiles();
                foreach (FileInfo NextFolder in Files)
                {
                    string ex = NextFolder.Extension.ToLower();
                    if (ex == ".jpg" || ex == ".png" || ex == ".bmp" || ex == ".gif" || ex == ".jpeg")
                    {
                        if (NextFolder.Name != information.Photo)
                        {
                            string iil = "/upload/Information/" + information.Id + "/" + NextFolder.Name;
                            filelist.Add(iil);
                        }
                    }
                }
            }
            ViewBag.ImgList = filelist;

            return View(information);
        }

        /// <summary>
        /// 详情列表
        /// </summary>
        /// <returns></returns>
        public IActionResult IntroList(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 服务信息
        /// </summary>
        /// <param name="id">服务信息Id</param>
        /// <returns></returns>
        public IActionResult Service(string id)
        {
            var info = new Information();
            var heads = new List<Models>(); //母版页头数据
            var webConfigs = new WebConfig(); //母版页footer数据
            var imgs = new List<string>(); //图片名称
            var visitColunms = new List<Colunms>();
            var relatedLinks = new Dictionary<string, string>();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var modelsService = new ModelsService(Services);
                heads = modelsService.GetAllIsShowModels(); //母版页头数据
                var visitModel = heads.FirstOrDefault(e => e.ModelName == "参观指南");
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
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
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
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            InformationAll ifm = Context.InformationAll.Find(id);
            RelatedLink rl = new RelatedLink(Services);
            if (!String.IsNullOrEmpty(ifm.InformationId))
            {
                ViewBag.Aside = rl.GetLink(ifm.InformationId);
            }
            return View(ifm);
        }
    }
}