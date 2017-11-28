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
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            //绑定banner
            ViewBag.Banner = Context.BannerEnglishView.FirstOrDefault(e => e.ModelName == "GUIDE");
            //绑定colunm
            ViewBag.Colunms = Context.ColunmsEnglishView.Where(e => e.ModelName == "GUIDE" && e.ColunmsId == new Guid("00000000-0000-0000-0000-000000000000")).OrderBy(x => x.Sorting).ToList();

            //遗迹馆
            ViewBag.Ruinsmuseum = Context.InformationEnglishAll.FirstOrDefault(e => e.ColumName == "Relics Hall");
            //陈列馆
            var Gallery = Context.InformationEnglishAll.Where(e => e.ColumName == "Exhibition Hall").OrderBy(x => x.CreationDate).Take(6).ToList();
            ViewBag.Gallery13 = Gallery.Take(3).ToList();
            ViewBag.Gallery46 = Gallery.Skip(3).Take(3).ToList();
            //文化景观
            ViewBag.CulturalLandscape = Context.InformationEnglishAll.Where(e => e.ColumName == "Cultural Landscape").OrderByDescending(x => x.CreationDate).Take(5).ToList();
            //获取展览推介
            ViewBag.Promotion = Context.InformationEnglishAll.FirstOrDefault(e => e.ColumName == "The exhibition profiles");
            //获取参展概况
            ViewBag.Profiles = Context.InformationEnglishAll.FirstOrDefault(e => e.ColumName == "Exhibition promotion");

            //获取新增栏目
            var newcol = Context.ColunmsEnglishView.Where(x => x.ModelName == "GUIDE" && x.IsNew == true).ToList();
            if (newcol.Count > 0)
            {
                var newinfo = new List<InformationAll>();
                foreach (var col in newcol)
                {
                    var info = Context.InformationEnglishAll.FirstOrDefault(x => x.ColumName == col.Name);
                    if (info != null)
                    {
                        newinfo.Add(info);
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
        /// 陈列馆
        /// </summary>
        /// <returns></returns>
        public IActionResult Display(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            InformationAll ifm = new InformationAll();
            if (id != null)
            {
                ifm = Context.InformationEnglishAll.Find(id);
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
            catch
            {

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
                //母版页头数据
                ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
                //母版页footer数据
                ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
                //页脚跳转信息
                ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

                ViewBag.UrlAddress = ViewBag.Ruinsmuseum = Context.InformationEnglishAll.FirstOrDefault(e => e.ColumName == "Relics Hall").UrlAddress;
                //找到遗迹馆的子项目
                var ruins = Context.ColunmsEnglishView.Where(e => e.ModelName == "GUIDE" && e.colunmsname == "Relics Hall" && e.ColunmsId != Guid.Empty).OrderBy(x => x.Sorting).ToList();
                ViewBag.Colunms = ruins;

                //遗迹馆的相关链接
                InformationAll ifm = Context.InformationEnglishAll.Where(e => e.ColumName == "Relics Hall").FirstOrDefault();
                RelatedLink rl = new RelatedLink(Services);
                if (!String.IsNullOrEmpty(ifm.InformationId))
                {
                    ViewBag.Aside = rl.GetLink(ifm.InformationId);
                }


                //找到遗迹馆下的子项
                foreach (var item in ruins)
                {
                    InformationAll ifall = Context.InformationEnglishAll.Where(x => x.ColumnId == item.Id).FirstOrDefault();
                    Guid id = new Guid();
                    if (item.Name == "Sacrifice of Jinsha")
                        id = Context.InformationEnglishAll.Where(x => x.ColumnId == item.ColunmsId).FirstOrDefault().Id;
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

                    if (item.Name == "Sacrifice of Jinsha")
                    {

                        ViewBag.Sacrifice = ifall;
                        ViewBag.SacriImg = filelist;
                    }
                    else if (item.Name == "Introdution to Relics Hall")
                    {

                        ViewBag.IntroImg = filelist;
                        ViewBag.Introduce = ifall;
                        //其他遗迹介绍
                        ViewBag.IntroduceList = Context.InformationEnglishAll.Where(x => x.ColumName == "Other relics").ToList();
                    }
                    else if (item.Name == "Archaeological Map")
                    {
                        ViewBag.Archaeology = Context.InformationEnglishAll.Where(x => x.ColumName == "Archaeological Map").FirstOrDefault();
                    }
                }
            }
            catch
            {
            }


            return View();
        }

        /// <summary>
        /// 特展
        /// </summary>
        /// <returns></returns>
        public IActionResult Special(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            InformationAll ifm = new InformationAll();
            if (id != null)
            {
                ifm = Context.InformationEnglishAll.Find(id);
                RelatedLink rl = new RelatedLink(Services);
                if (!String.IsNullOrEmpty(ifm.InformationId))
                {
                    ViewBag.Aside = rl.GetLink(ifm.InformationId);
                }
            }
            try
            {
                if (ifm.ColumName == "Featured Exhibition")
                {
                    if (ifm.ZipCode != null)
                    {
                        var filelist = JsonConvert.DeserializeObject<List<SpecialClass>>(ifm.ZipCode);
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
            catch
            {

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
        public JsonResult PhotoLastAndNext(Guid id, int index)
        {
            try
            {
                InformationAll ifm = Context.InformationEnglishAll.Find(id);
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
            InformationAll ifm = Context.InformationEnglishAll.Find(id);
            var filelist = JsonConvert.DeserializeObject<List<SpecialClass>>(ifm.ZipCode);
            var data = filelist.Skip(pageIndex * pageSize).Take(pageSize);
            return Json(new { data });
        }
    }
}