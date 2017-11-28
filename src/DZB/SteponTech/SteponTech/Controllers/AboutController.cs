using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using SteponTech.Data.CommonModels;
using SteponTech.Services.CommonService;
using SteponTech.Utils;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 关于金沙
    /// </summary>
    public class AboutController : BaseController<AboutController, SteponContext>
    {

        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public AboutController(IHostingEnvironment env)
        {
            Environment = env;
        }



        /// <summary>
        /// 关于金沙首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
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
                var model = heads.FirstOrDefault(e => e.ModelName == "关于金沙");
                var visitModel = heads.FirstOrDefault(e => e.ModelName == "参观指南");
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









            //获取新增栏目  
            var newcol = Context.ColunmsView.Where(x => x.ModelName == "关于金沙" && x.IsNew == true).ToList();
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













            ViewBag.Head = heads;
            ViewBag.Footer = webConfigs;
            ViewBag.Banner = banner;
            ViewBag.Colunms = colunms;
            ViewBag.Informations = informations;
            ViewBag.VisitColunms = visitColunms;
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
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
        /// 金沙简介
        /// </summary>
        /// <returns></returns>
        public IActionResult Into()
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
                var visitModel = heads.FirstOrDefault(e => e.ModelName == "参观指南");
                var colunmsService = new ColunmsService(Services);
                visitColunms = colunmsService.GetColunmsByModelId(visitModel.Id);
                var informationService = new InformationService(Services);
                info = informationService.GetInformationAllByColumName("金沙简介");
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
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            return View(info);
        }
    }
}