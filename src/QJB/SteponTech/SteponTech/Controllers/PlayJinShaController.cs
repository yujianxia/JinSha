using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using Microsoft.AspNetCore.Hosting;
using SteponTech.Data.BaseModels;
using System.IO;
using SteponTech.Services.CommonService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.Controllers
{
    /// <summary>
    /// 玩转金沙
    /// </summary>
    public class PlayJinShaController : BaseController<PlayJinShaController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public PlayJinShaController(IHostingEnvironment env)
        {
            Environment = env;
        }

        /// <summary>
        /// 青少年体验区
        /// </summary>
        public IActionResult YoungExperience()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            ViewBag.YoungExperience = Context.InformationYoungView.Where(x => x.ColumName == "青少年教育体验区").OrderByDescending(x => x.CreationDate).FirstOrDefault();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 多彩金沙
        /// </summary>
        public IActionResult PolychromeJinSha()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 活动报名
        /// </summary>
        public IActionResult Enrollment()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 活动报名
        /// </summary>
        public IActionResult EnrollmentDetali(int id)
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 小金历险记
        /// </summary>
        public IActionResult JinAdventures()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 活动花絮
        /// </summary>
        public IActionResult EZine()
        {
            try
            {
                InformationYoungView ifm = Context.InformationYoungView.Where(x => x.ColumName == "活动花絮").OrderByDescending(x => x.CreationDate).FirstOrDefault();
                List<string> filelist = new List<string>();
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Information\\" + ifm.Id);
                if (Directory.Exists(filepath))
                {
                    DirectoryInfo theFolder = new DirectoryInfo(filepath);
                    FileInfo[] Files = theFolder.GetFiles();
                    foreach (FileInfo NextFolder in Files)
                    {
                        if (NextFolder.Extension == ".jpg" || NextFolder.Extension == ".png" || NextFolder.Extension == ".bmp" || NextFolder.Extension == ".gif" || NextFolder.Extension == ".jpeg")
                        {
                            if (NextFolder.Name != ifm.Photo)
                            {
                                string iil = "/upload/Information/" + ifm.Id + "/" + NextFolder.Name;
                                filelist.Add(iil);
                            }
                        }
                    }
                }
                ViewBag.ImgList = filelist;
                //页脚
                ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
                //页脚
                var webConfigService = new WebConfigService(Services);
                ViewBag.Footer = webConfigService.GetWebConfig();
            }
            catch
            {

            }
            return View();
        }

        /// <summary>
        /// 多彩金沙详情
        /// </summary>
        public IActionResult PolychromeDetail(Guid id)
        {
            InformationYoungView iyv = Context.InformationYoungView.Find(id);

            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View(iyv);
        }
    }
}
