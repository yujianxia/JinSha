using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SteponTech.Data;
using Stepon.Mvc.Controllers;
using Microsoft.AspNetCore.Hosting;
using SteponTech.Data.BaseModels;
using System.IO;
using SteponTech.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft;
using Newtonsoft.Json;
using SteponTech.Services.CommonService;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 展览导览
    /// </summary>
    public class ExhibitionController : BaseController<ExhibitionController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public ExhibitionController(IHostingEnvironment env)
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

            //绑定banner
            ViewBag.Banner = Context.BannerView.FirstOrDefault(e => e.ModelName == "展览导览");
            //绑定colunm
            ViewBag.Colunms = Context.ColunmsView.Where(e => e.ModelName == "展览导览" && e.ColunmsId == new Guid("00000000-0000-0000-0000-000000000000")).OrderBy(x => x.Sorting).ToList();

            //遗迹馆
            ViewBag.Ruinsmuseum = Context.InformationAll.FirstOrDefault(e => e.ColumName == "遗迹馆");
            //陈列馆
            var Gallery = Context.InformationAll.Where(e => e.ColumName == "陈列馆").Take(6).ToList();
            ViewBag.Gallery13 = Gallery.Take(3).ToList();
            ViewBag.Gallery46 = Gallery.Skip(3).Take(3).ToList();
            //文化景观
            ViewBag.CulturalLandscape = Context.InformationAll.Where(e => e.ColumName == "文化景观").Take(5).ToList();
            //获取展览推介
            ViewBag.Promotion = Context.InformationAll.FirstOrDefault(e => e.ColumName == "展览推介");
            //获取参展概况
            ViewBag.Profiles = Context.InformationAll.FirstOrDefault(e => e.ColumName == "参展概况");

            //获取新增栏目
            var newcol = Context.ColunmsView.Where(x => x.ModelName == "展览导览" && x.IsNew == true).ToList();
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
        /// 陈列馆
        /// </summary>
        /// <returns></returns>
        public IActionResult Display(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            InformationAll ifm = new InformationAll();
            if (id != null)
            {
                ifm = Context.InformationAll.Find(id);
                RelatedLink rl = new RelatedLink(Services);
                if (!String.IsNullOrEmpty(ifm.InformationId))
                {
                    ViewBag.Aside = rl.GetLink(ifm.InformationId);
                }
            }
            try
            {
                List<string> filelist = new List<string>();
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Information\\" + ifm.Id);
                if (Directory.Exists(filepath))
                {
                    DirectoryInfo theFolder = new DirectoryInfo(filepath);
                    FileInfo[] Files = theFolder.GetFiles();
                    foreach (FileInfo NextFolder in Files)
                    {
                        string ex = NextFolder.Extension.ToLower();
                        if (ex == ".jpg" || ex == ".png" || ex == ".bmp" || ex == ".gif" || ex == ".jpeg")
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
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            return View(ifm);
        }
        /// <summary>
        /// 遗迹馆
        /// </summary>
        /// <returns></returns>
        public IActionResult Ruins()
        {
            try
            {
                //是否登录总线
                ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
                //母版页头数据
                ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
                //母版页footer数据
                ViewBag.Footer = Context.WebConfig.FirstOrDefault();
                //页脚跳转信息
                ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

                ViewBag.UrlAddress = ViewBag.Ruinsmuseum = Context.InformationAll.FirstOrDefault(e => e.ColumName == "遗迹馆").UrlAddress;
                //找到遗迹馆的子项目
                var ruins = Context.ColunmsView.Where(e => e.ModelName == "展览导览" && e.colunmsname == "遗迹馆").OrderBy(x => x.Sorting).ToList();
                ViewBag.Colunms = ruins;
                //找到遗迹馆下的子项
                foreach (var item in ruins)
                {
                    InformationAll ifall = Context.InformationAll.Where(x => x.ColumnId == item.Id).FirstOrDefault();

                    Guid id = new Guid();
                    if (item.Name == "金沙祭祀")
                        id = Context.InformationAll.Where(x => x.ColumnId == item.ColunmsId).FirstOrDefault().Id;
                    else
                        id = ifall.Id;

                    List<string> filelist = new List<string>();
                    var filepath = Environment.WebRootPath;  //文件存储路径
                    filepath = Path.Combine(filepath, "upload\\Information\\" + id);
                    if (Directory.Exists(filepath))
                    {
                        DirectoryInfo theFolder = new DirectoryInfo(filepath);
                        FileInfo[] Files = theFolder.GetFiles();
                        foreach (FileInfo NextFolder in Files)
                        {
                            string ex = NextFolder.Extension.ToLower();
                            if (ex == ".jpg" || ex == ".png" || ex == ".bmp" || ex == ".gif" || ex == ".jpeg")
                            {
                                if (NextFolder.Name != ifall.Photo)
                                {
                                    string iil = "/upload/Information/" + id + "/" + NextFolder.Name;
                                    filelist.Add(iil);
                                }
                            }
                        }
                    }

                    if (item.Name == "金沙祭祀")
                    {

                        ViewBag.Sacrifice = ifall;
                        ViewBag.SacriImg = filelist;
                    }
                    else if (item.Name == "遗迹馆介绍")
                    {

                        ViewBag.IntroImg = filelist;
                        ViewBag.Introduce = ifall;
                        //其他遗迹介绍
                        ViewBag.IntroduceList = Context.InformationAll.Where(x => x.ColumName == "其他遗迹介绍").ToList();
                    }
                    else if (item.Name == "金沙考古")
                    {
                        ViewBag.Archaeology= Context.InformationAll.Where(x => x.ColumName == "金沙考古").FirstOrDefault();
                    }


                    InformationAll ifm = Context.InformationAll.Where(x => x.ColumName == "遗迹馆").FirstOrDefault();
                    RelatedLink rl = new RelatedLink(Services);
                    if (!String.IsNullOrEmpty(ifm.InformationId))
                    {
                        ViewBag.Aside = rl.GetLink(ifm.InformationId);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }


            return View();
        }

        /// <summary>
        /// 特展
        /// </summary>
        /// <returns></returns>
        public IActionResult Special(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            InformationAll ifm = new InformationAll();
            if (id != null)
            {
                ifm = Context.InformationAll.Find(id);
                RelatedLink rl = new RelatedLink(Services);
                if (!String.IsNullOrEmpty(ifm.InformationId))
                {
                    ViewBag.Aside = rl.GetLink(ifm.InformationId);
                }
            }
            try
            {
                if (ifm.ColumName == "特展")
                {
                    if (ifm.ZipCode != null)
                    {
                        var filelist= JsonConvert.DeserializeObject<List<SpecialClass>>(ifm.ZipCode);
                        ViewBag.SpecialPhotoCount = filelist.Count;
                        ViewBag.SpecialPhoto = filelist.Take(12).ToList();
                    }
                }
                else
                {
                    List<string> filelist = new List<string>();
                    var filepath = Environment.WebRootPath;  //文件存储路径
                    filepath = Path.Combine(filepath, "upload\\Information\\" + ifm.Id);
                    if (Directory.Exists(filepath))
                    {
                        DirectoryInfo theFolder = new DirectoryInfo(filepath);
                        FileInfo[] Files = theFolder.GetFiles();
                        foreach (FileInfo NextFolder in Files)
                        {
                            string ex = NextFolder.Extension.ToLower();
                            if (ex == ".jpg" || ex == ".png" || ex == ".bmp" || ex == ".gif" || ex == ".jpeg")
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
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            return View(ifm);
        }
        /// <summary>
        /// 照片墙下一张
        /// </summary>
        /// 页码
        /// 页数
        /// id
        /// <returns></returns>
        public JsonResult PhotoLastAndNext(Guid id,int index)
        {
            try
            {
                InformationAll ifm = Context.InformationAll.Find(id);
                var filelist = JsonConvert.DeserializeObject<List<SpecialClass>>(ifm.ZipCode);
                var dataall = filelist.ToList();
                var data = dataall[index];
                return Json(new { data });
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 照片墙翻页
        /// </summary>
        /// 页码
        /// 页数
        /// id
        /// <returns></returns>
        public JsonResult PhotoPage(int pageIndex, int pageSize, Guid id)
        {
            InformationAll ifm = Context.InformationAll.Find(id);
            var filelist = JsonConvert.DeserializeObject<List<SpecialClass>>(ifm.ZipCode);
            var data = filelist.Skip(pageIndex * pageSize).Take(pageSize);
            return Json(new { data });
        }
    }
}