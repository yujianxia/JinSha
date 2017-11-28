
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using System.IO;
using SteponTech.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 艺术金沙
    /// </summary>
    public class CreationController : BaseController<CreationController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public CreationController(IHostingEnvironment env)
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
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            //banner
            ViewBag.Banner = Context.BannerView.FirstOrDefault(e => e.ModelName == "艺术金沙");
            //获取文化创作下的板块名称
            var artsModelList = Context.ColunmsView.Where(x => x.colunmsname == "文化创作" && x.ModelName == "艺术金沙").ToList();
            var artslist = new List<InformationAll>();
            foreach (var art in artsModelList)
            {
                var info = Context.InformationAll.Where(x => x.ColumName == art.Name && x.ModelName == "艺术金沙").FirstOrDefault();
                if (info != null)
                {
                    artslist.Add(info);
                }
            }
            ViewBag.ArtsList = artslist;
            ViewBag.Arts = Context.InformationAll.Where(x => x.ColumName == "文化创作" && x.ModelName == "艺术金沙");





            ViewBag.Creativity = Context.InformationAll.Where(x => x.ColumName == "文创产品").FirstOrDefault();

            //获取新增栏目
            var newcol = Context.ColunmsView.Where(x => x.ModelName == "艺术金沙" && x.IsNew == true && x.colunmsname != "文化创作").ToList();
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
        /// 资讯分类
        /// </summary>
        /// <returns></returns>
        public IActionResult IntroType(Guid id)
        {
            var information = Context.InformationAll.Find(id);
            if (information.ColumName == "文化创作")
            {
                return Redirect("Intro?id=" + information.Id + "");
            }
            else
            {
                return Redirect("IntroList?id=" + information.ColumnId + "");
            }

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
        /// 获取纪念品
        /// </summary>
        /// <param name="st">开始数据的下标</param>
        /// <param name="num">个数</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetSouvenir(int st, int num)
        {
            var res = Context.InformationAll.Where(e => e.ColumName == "热卖纪念品").OrderByDescending(e => e.CreationDate).Skip(st).Take(num).ToList();

            var data = res.Select(e => new { e.Title, e.UrlAddress, Pic = "/upload/Information/" + e.Id.ToString() + "/" + e.Photo }).ToList();
            return Json(data);
        }




    }
}