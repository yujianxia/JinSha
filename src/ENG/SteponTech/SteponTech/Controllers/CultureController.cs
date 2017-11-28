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

            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();

            //banner
            ViewBag.Banner = Context.BannerEnglishView.Where(e => e.ModelName == "EVENTS").OrderByDescending(e=>e.LastUpdate).FirstOrDefault();
            //顶级colunm
            ViewBag.Colunms = Context.ColunmsEnglishView.Where(e => e.ModelName == "EVENTS" && e.ColunmsId == new Guid("00000000-0000-0000-0000-000000000000")).OrderBy(x => x.Sorting).ToList();



           var infos = Context.InformationEnglishAll.Where(e => e.ColumName == "Jinsha Sun Festival"|| e.ColumName == "About Jinsha Sun Festival" || e.ColumName == "International Programs"  || e.ColumName == "Seoul Lantern Festival" || e.ColumName == "Night of Jinsha" ).ToList();

            //太阳节
            var sunfei = infos.Where(e => e.ColumName == "Jinsha Sun Festival").OrderByDescending(e => e.CreationDate).ToList();

            ViewBag.SunFes = sunfei;

            var nian = new List<string>();
            var year = new List<SunYear>();
            foreach (var sun in sunfei)
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
            var aboutsun = infos.FirstOrDefault(e => e.ColumName == "About Jinsha Sun Festival");
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

      

            //国际文化交流
            //获取文化创作下的板块名称
            var artsModelList = Context.ColunmsEnglishView.Where(x => x.colunmsname == "International Programs" && x.ModelName== "EVENTS").ToList();
            var artslist = new List<InformationAll>();
            foreach (var art in artsModelList)
            {
                var info = Context.InformationEnglishAll.Where(x => x.ColumName == art.Name && x.ModelName == "EVENTS").OrderByDescending(x => x.LastUpdate).FirstOrDefault();
                if (info != null)
                {
                    artslist.Add(info);
                }
            }
            ViewBag.InterNationList = artslist;
            //ViewBag.ArtsList = Context.InformationAll.Where(x => x.ColumName == "文创作品").OrderByDescending(x => x.CreationDate).FirstOrDefault();
            ViewBag.InterNation = Context.InformationEnglishAll.Where(x => x.ColumName == "International Programs" && x.ModelName == "EVENTS");


            //ViewBag.InterNation = infos.Where(e => e.ColumName == "International Programs").ToList();
            //var list = new List<InformationAll>();
            //list.Add(infos.FirstOrDefault(e => e.ColumName == "Seoul Lantern Festival"));
            //list.Add(infos.FirstOrDefault(e => e.ColumName == "Night of Jinsha"));
            //ViewBag.InterNationList = list;



            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            //获取新增栏目
            var newcol = Context.ColunmsEnglishView.Where(x => x.ModelName == "EVENTS" && x.IsNew == true && x.colunmsname != "International Programs").ToList();
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
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();
            var ids = Guid.Parse(id);
            var sun = Context.InformationEnglishAll.FirstOrDefault(e => e.Id == ids);
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


            //太阳节资讯



            var dic = new List<InformationAll>();
            dic.Add(sun);
            if (!string.IsNullOrEmpty(sun.InformationId))
            {

                //太阳节资讯
                List<string> informations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(sun.InformationId);
                foreach (var item in informations)
                {
                    dic.Add(Context.InformationEnglishAll.Find(new Guid(item)));
                }
            }

            ViewBag.News = dic;
            //var idagg = sun.SunId.Split(',');
            //var sunlist = new List<InformationAll>();
            //    foreach (var i in idagg)
            //{
            //    var gis = Guid.Parse(i);
            //    var info=Context.InformationEnglishAll.FirstOrDefault(e => e.Id == gis);
            //    if (info != null)
            //    {
            //        sunlist.Add(info);
            //    }
            //}

            return View();
        }

        /// <summary>
        /// 太阳节资讯
        /// </summary>
        /// <returns></returns>
        public IActionResult SunDetail(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();
            //太阳节资讯
            var info = Context.InformationEnglishAll.FirstOrDefault(e => e.Id == id);
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
        /// 文化活动详情
        /// </summary>
        /// <returns></returns>
        public IActionResult ActivityDetail(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();
            var info = Context.InformationEnglishAll.FirstOrDefault(e => e.Id == id);
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
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            var information = Context.InformationEnglishAll.Find(id);
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
            var information = Context.InformationEnglishAll.Find(id);
            if (information.ColumName == "International Programs")
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
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public IActionResult Intro(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            var information = Context.InformationEnglishAll.Find(id);
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


    }
}