
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Utils;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 金沙资讯
    /// </summary>
    public class NewsController : BaseController<NewsController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public NewsController(IHostingEnvironment env)
        {
            Environment = env;
        }


        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知") ;

            //获取新增栏目  
            var newcol = Context.ColunmsView.Where(x => x.ModelName == "金沙资讯" && x.IsNew == true).ToList();
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

            //绑定banner
            ViewBag.Banner = Context.BannerView.FirstOrDefault(e => e.ModelName == "金沙资讯");
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
        /// 金沙快讯
        /// </summary>
        /// <returns></returns>
        public IActionResult News(Guid id)
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

        /// <summary>
        /// 金沙公告
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