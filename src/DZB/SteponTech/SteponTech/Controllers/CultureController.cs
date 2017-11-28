using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Utils.DHSH;
using SteponTech.Utils.DHSH.Model;
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Model.Config;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using SteponTech.Utils;
using SteponTech.Data.BaseModels;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 文化活动
    /// </summary>
    public class CultureController : BaseController<CultureController, SteponContext>
    {

        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public CultureController(IHostingEnvironment env)
        {
            Environment = env;
        }

        /// <summary>
        /// 文化活动首页
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
            ViewBag.Banner = Context.BannerView.Where(e => e.ModelName == "文化活动").FirstOrDefault();
            //顶级colunm
            ViewBag.Colunms = Context.ColunmsView.Where(e => e.ModelName == "文化活动" && e.ColunmsId == new Guid("00000000-0000-0000-0000-000000000000")).OrderBy(x => x.Sorting).ToList();

            
            var infos = Context.InformationAll.Where(e => e.ColumName == "金沙太阳节"|| e.ColumName == "关于金沙太阳节"
            || e.ColumName == "讲坛预告"|| e.ColumName == "关于金沙讲坛"|| e.ColumName == "国际文化交流").ToList();


            //太阳节
            var sunfei= infos.Where(e => e.ColumName == "金沙太阳节").ToList();
            ViewBag.SunFes = sunfei;

            var nian = new List<string>();
            var year = new List<SunYear>();
            foreach(var sun in sunfei)
            {
                if (!string.IsNullOrEmpty(sun.InformationTime))
                {
                    if (!nian.Contains(sun.InformationTime.Substring(0, 4)))
                    {
                        nian.Add(sun.InformationTime.Substring(0, 4));
                        year.Add(new SunYear { Year = sun.InformationTime.Substring(0, 4), Id = sun.Id });
                    }
                }
            }
            ViewBag.SunYear = year.OrderByDescending(e => e.Year).ToList();

            //关于太阳节
            var aboutsun = infos.FirstOrDefault(e => e.ColumName == "关于金沙太阳节");
            ViewBag.AboutSunFes = aboutsun;
            //图片列表
            var path = Path.Combine(Environment.WebRootPath, "upload", "Information", aboutsun.Id.ToString());
            var files = Directory.GetFiles(path);
            var newfile = new List<string>();
            foreach (var f in files)
            {
                //排除视频
                var ex = Path.GetExtension(f);
                if (ex != ".mp4" || ex != ".ogg")
                {
                    newfile.Add(Path.GetFileName(f));

                }
            }
            ViewBag.AboutSunFesPic = newfile;

            //讲坛
            ViewBag.LecturePre = infos.Where(e => e.ColumName == "讲坛预告").OrderByDescending(e => e.CreationDate).Take(2).ToList();
            ViewBag.LectureAbout = infos.FirstOrDefault(e => e.ColumName == "关于金沙讲坛");

            //国际文化交流
            //ViewBag.InterNation = infos.Where(e => e.ColumName == "国际文化交流").ToList();
            //var list = new List<InformationAll>();
            //list.Add(infos.FirstOrDefault(e => e.ColumName == "首尔灯节"));
            //list.Add(infos.FirstOrDefault(e => e.ColumName == "金沙之夜"));
            //ViewBag.InterNationList = list;

            //获取文化创作下的板块名称
            var artsModelList = Context.ColunmsView.Where(x => x.colunmsname == "国际文化交流" && x.ModelName == "文化活动").ToList();
            var artslist = new List<InformationAll>();
            foreach (var art in artsModelList)
            {
                var info = Context.InformationAll.Where(x => x.ColumName == art.Name && x.ModelName == "文化活动").FirstOrDefault();
                if (info != null)
                {
                    artslist.Add(info);
                }
            }
            ViewBag.InterNationList = artslist;
            //ViewBag.ArtsList = Context.InformationAll.Where(x => x.ColumName == "文创作品").OrderByDescending(x => x.CreationDate).FirstOrDefault();
            ViewBag.InterNation = Context.InformationAll.Where(x => x.ColumName == "国际文化交流" && x.ModelName == "文化活动");


            //获取新增栏目
            var newcol = Context.ColunmsView.Where(x => x.ModelName == "文化活动" && x.IsNew == true && x.colunmsname != "国际文化交流").ToList();
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

        public class SunYear
        {
            public string Year { get; set; }
            public Guid Id { get; set; }
        }

        /// <summary>
        /// 太阳节
        /// </summary>
        /// <returns></returns>
        public IActionResult SunFestival(string id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            var ids = Guid.Parse(id);
            var sun = Context.InformationAll.FirstOrDefault(e => e.Id == ids);
            ViewBag.Info = sun;

            //图片集
            //图片列表
            var path = Path.Combine(Environment.WebRootPath, "upload", "Information", sun.Id.ToString());
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                var newfile = new List<string>();
                var newvideo = new List<string>();
                foreach (var f in files)
                {
                    //排除视频
                    var ex = Path.GetExtension(f);
                    if (!String.Equals(ex, ".mp4") && !String.Equals(ex, ".ogg"))
                    {
                        newfile.Add(Path.GetFileName(f));
                    }
                    else
                    {
                        newvideo.Add(Path.GetFileName(f));
                    }
                }
                ViewBag.SunPic = newfile;
                ViewBag.SunVideo = newvideo;
            }
            var dic = new List<InformationAll>();
            dic.Add(sun);
            if (!string.IsNullOrEmpty(sun.InformationId))
            {

                //太阳节资讯
                List<string> informations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(sun.InformationId);
                foreach (var item in informations)
                {
                    dic.Add(Context.InformationAll.Find(new Guid(item)));
                }
            }

            ViewBag.News = dic;
            //var idagg = sun.SunId.Split(',');
            //var sunlist = new List<InformationAll>();
            //foreach (var i in idagg)
            //{
            //    var gis = Guid.Parse(i);
            //    var info = Context.InformationAll.FirstOrDefault(e => e.Id == gis);
            //    if (info != null)
            //    {
            //        sunlist.Add(info);
            //    }
            //}
            //ViewBag.News = sunlist;


            ////相关链接
            //if (!String.IsNullOrEmpty(sun.InformationId))
            //{
            //    RelatedLink rl = new RelatedLink(Services);
            //    ViewBag.Link = rl.GetLink(sun.InformationId);
            //}

            return View();
        }

        /// <summary>
        /// 太阳节资讯
        /// </summary>
        /// <returns></returns>
        public IActionResult SunDetail(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            //太阳节资讯
            var info = Context.InformationAll.FirstOrDefault(e => e.Id == id);
            ViewBag.News = info;

            //相关链接
            if (!String.IsNullOrEmpty(info.InformationId))
            {
                RelatedLink rl = new RelatedLink(Services);
                ViewBag.Link = rl.GetLink(info.InformationId);
            }

            return View();
        }

        /// <summary>
        /// 关于讲坛
        /// </summary>
        /// <returns></returns>
        public IActionResult LectureIntro()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            var info = Context.InformationAll.FirstOrDefault(e => e.ColumName == "关于金沙讲坛");
            ViewBag.Info = info;

            //相关链接
            if (!String.IsNullOrEmpty(info.InformationId))
            {
                RelatedLink rl = new RelatedLink(Services);
                ViewBag.Link = rl.GetLink(info.InformationId);
            }
            return View();
        }
        /// <summary>
        /// 讲坛预告
        /// </summary>
        /// <returns></returns>
        public IActionResult LectureNotice()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            var infos = Context.InformationAll.Where(e => e.ColumName == "讲坛预告").ToList();
            //预告

            var notice = new List<InformationAll>();
            var re = new List<InformationAll>();
            foreach (var info in infos)
            {
                var tmp = DateTime.Parse(info.InformationTime);
                if (tmp > DateTime.Now)
                {
                    info.InformationTime = tmp.ToString("HH:mm MMMMMMMMMMM dd(dddddddddd) ", new CultureInfo("en-US"));
                    notice.Add(info);
                }
                else
                {
                    info.InformationTime = tmp.ToString("HH:mm MMMMMMMMMMM dd(dddddddddd) ", new CultureInfo("en-US"));
                    re.Add(info);
                 }
            }
            ViewBag.Notice = notice;

            //回顾
            ViewBag.Review = re;
            return View();
        }
        /// <summary>
        /// 讲坛详情
        /// </summary>
        /// <returns></returns>
        public IActionResult LectureDetail(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            var info = Context.InformationAll.FirstOrDefault(e => e.Id == id);
            ViewBag.Info = info;

            //相关链接
            if (!String.IsNullOrEmpty(info.InformationId))
            {
                RelatedLink rl = new RelatedLink(Services);
                ViewBag.Link = rl.GetLink(info.InformationId);
            }
            return View();
        }

        /// <summary>
        /// 文化活动详情
        /// </summary>
        /// <returns></returns>
        public IActionResult ActivityDetail(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            var info = Context.InformationAll.FirstOrDefault(e => e.Id == id);
            ViewBag.Info = info;
            //相关链接
            if (!String.IsNullOrEmpty(info.InformationId))
            {
                RelatedLink rl = new RelatedLink(Services);
                ViewBag.Link = rl.GetLink(info.InformationId);
            }
            List<string> filelist = new List<string>();
            var filepath = Environment.WebRootPath;  //文件存储路径
            filepath = Path.Combine(filepath, "upload\\Information\\" + info.Id);
            if (Directory.Exists(filepath))
            {
                DirectoryInfo theFolder = new DirectoryInfo(filepath);
                FileInfo[] Files = theFolder.GetFiles();
                foreach (FileInfo NextFolder in Files)
                {
                    string ex = NextFolder.Extension.ToLower();
                    if (ex == ".jpg" || ex == ".png" || ex == ".bmp" || ex == ".gif" || ex == ".jpeg")
                    {
                        if (NextFolder.Name != info.Photo)
                        {
                            string iil = "/upload/Information/" + info.Id + "/" + NextFolder.Name;
                            filelist.Add(iil);
                        }
                    }
                }
            }
            ViewBag.ImgList = filelist;
            return View();
        }
        /// <summary>
        /// 国际文化活动详情
        /// </summary>
        /// <returns></returns>
        public IActionResult InternationalDetail(Guid id)
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
        /// 资讯分类
        /// </summary>
        /// <returns></returns>
        public IActionResult IntroType(Guid id)
        {
            var information = Context.InformationAll.Find(id);
            if (information.ColumName == "国际文化交流")
            {
                return Redirect("Intro?id=" + information.Id + "");
            }
            else
            {
                return Redirect("IntroList?id=" + information.ColumnId + "");
            }

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
        /// 我要参与
        /// </summary>
        /// <returns></returns>
        public IActionResult Participate()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");


            return View();
        }

        /// <summary>
        /// 我要参与活动详情
        /// </summary>
        /// <returns></returns>
        public IActionResult ActivityInfo(int id)
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
    }
}