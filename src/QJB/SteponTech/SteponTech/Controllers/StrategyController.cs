using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Data.CommonModels;
using SteponTech.Services.BaseServices;
using SteponTech.Services.CommonService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 金沙攻略
    /// </summary>
    public class StrategyController : BaseController<StrategyController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public StrategyController(IHostingEnvironment env)
        {
            Environment = env;
        }

        /// <summary>
        /// 认识金沙
        /// </summary>
        /// <returns></returns>
        public IActionResult Intro()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();

            var webConfigs = new WebConfig();
            var introInfo = new InformationYoung();  //金沙简介
            var landmarkInfo = new InformationYoung();//金沙地标
            var introImg = new List<string>(); //金沙简介图集
            var landmarkImg = new List<string>(); //金沙地标图集
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var colunmsService = new ColunmsService(Services);
                var animationColunm = colunmsService.GetColunmsYoungByName("认识金沙");
                if (animationColunm != null)
                {
                    var informationService = new InformationService(Services);
                    var informations = informationService.GetInformationByColumnId(animationColunm.Id);
                    if (informations?.Count > 0)
                    {
                        var webRoot = Environment.WebRootPath;
                        introInfo = informations.FirstOrDefault(e => e.Title == "金沙简介");
                        if (introInfo != null)
                        {
                            var dirPath = Path.Combine(webRoot, "upload", "Information", introInfo.Id.ToString());
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
                                            introImg.Add(fileInfo.Name);
                                        }
                                    }
                                }
                            }
                        }
                        landmarkInfo = informations.FirstOrDefault(e => e.Title == "金沙地标");
                        if (landmarkInfo != null)
                        {
                            var dirPath = Path.Combine(webRoot, "upload", "Information", landmarkInfo.Id.ToString());
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
                                            landmarkImg.Add(fileInfo.Name);
                                        }
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
            ViewBag.Footer = webConfigs;
            ViewBag.IntroInfo = introInfo;
            ViewBag.LandmarkInfo = landmarkInfo;
            ViewBag.IntroImg = introImg;
            ViewBag.LandmarkImg = landmarkImg;
            return View();
        }
        /// <summary>
        /// 动画金沙
        /// </summary>
        /// <returns></returns>
        public IActionResult Animation()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();

            var webConfigs = new WebConfig();
            var animationInfo = new List<InformationYoungView>();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var informationService = new InformationService(Services);
                animationInfo = informationService.GetAllInfoViewByColumName("动画金沙");
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Footer = webConfigs;
            ViewBag.AnimationInfo = animationInfo;
            return View();
        }
        /// <summary>
        /// 趣问金沙
        /// </summary>
        /// <returns></returns>
        public IActionResult Question()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();

            var webConfigs = new WebConfig();
            var questionInfo = new InformationYoungView();
            var columId = "";
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var informationService = new InformationService(Services);
                questionInfo = informationService.GetInfoViewByColumName("趣问金沙");
                var colunmsService = new ColunmsService(Services);
                var colunms = colunmsService.GetColunmsYoungByName("小金的问题");
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
            ViewBag.QuestionInfo = questionInfo;
            ViewBag.ColumId = columId;
            return View();
        }
        /// <summary>
        /// 趣问金沙详情
        /// </summary>
        /// <param name="id">问题Id</param>
        /// <returns></returns>
        public IActionResult QuestionDetails(string id)
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();

            var webConfigs = new WebConfig();
            var questionInfo = new InformationYoung();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                if (!string.IsNullOrEmpty(id))
                {
                    var informationService = new InformationService(Services);
                    questionInfo = informationService.GetInformationById(Guid.Parse(id));
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Footer = webConfigs;
            return View(questionInfo);
        }
        /// <summary>
        /// 萌娃讲金沙
        /// </summary>
        /// <returns></returns>
        public IActionResult ChildrenTalk()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();

            var webConfigs = new WebConfig();
            var videoColumId = "";
            var musicColumId = "";
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var colunmsService = new ColunmsService(Services);
                var videoColunm = colunmsService.GetColunmsYoungByName("视频萌娃");
                if (videoColunm != null)
                {
                    videoColumId = videoColunm.Id.ToString();
                }
                var musicColunm = colunmsService.GetColunmsYoungByName("音频萌娃");
                if (musicColunm != null)
                {
                    musicColumId = musicColunm.Id.ToString();
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Footer = webConfigs;
            ViewBag.VideoColumId = videoColumId;
            ViewBag.MusicColumId = musicColumId;
            return View();
        }
        /// <summary>
        /// 金沙宝典（参观指南）
        /// </summary>
        /// <returns></returns>
        public IActionResult ValuableBook()
        {
            var webConfigs = new WebConfig();
            var infomations = new List<InformationYoungView>();
            var bookInfo = new InformationYoungView();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var informationService = new InformationService(Services);
                infomations = informationService.GetAllInfoViewByColumName("参观指南");
                bookInfo = informationService.GetInfoViewByColumName("金沙宝典");
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Footer = webConfigs;
            ViewBag.Infomations = infomations;
            ViewBag.BookInfo = bookInfo;

            return View();
        }
        /// <summary>
        /// 金沙宝典（导览地图）
        /// </summary>
        /// <returns></returns>
        public IActionResult ValuableBookMap()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();

            var webConfigs = new WebConfig();
            var bookInfo = new InformationYoungView();
            try
            {
                var webConfigService = new WebConfigService(Services);
                webConfigs = webConfigService.GetWebConfig();
                var informationService = new InformationService(Services);
                bookInfo = informationService.GetInfoViewByColumName("金沙宝典");
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            ViewBag.Footer = webConfigs;
            ViewBag.BookInfo = bookInfo;
            return View();
        }
    }
}