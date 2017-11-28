
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
using Microsoft.AspNetCore.Hosting;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 金沙文创
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
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            //banner
            ViewBag.Banner = Context.BannerEnglishView.FirstOrDefault(e => e.ModelName == "CREATIVITY");


            //获取文化创作下的板块名称
            var artsModelList = Context.ColunmsEnglishView.Where(x => x.colunmsname == "Arts" && x.ModelName == "CREATIVITY").ToList();
            var artslist = new List<InformationAll>();
            foreach (var art in artsModelList)
            {
                var info = Context.InformationEnglishAll.Where(x => x.ColumName == art.Name && x.ModelName == "CREATIVITY").OrderByDescending(x => x.LastUpdate).FirstOrDefault();
                if (info != null)
                {
                    artslist.Add(info);
                }
            }
            ViewBag.ArtsList = artslist;
            //ViewBag.ArtsList = Context.InformationAll.Where(x => x.ColumName == "文创作品").OrderByDescending(x => x.CreationDate).FirstOrDefault();
            ViewBag.Arts = Context.InformationEnglishAll.Where(x => x.ColumName == "Arts" && x.ModelName == "CREATIVITY");



            //List<InformationAll> inf = new List<InformationAll>();
            //var arts = Context.ColunmsEnglishView.Where(x => x.colunmsname == "Arts").ToList();
            //foreach (var item in arts)
            //{
            //    var information = Context.InformationEnglishAll.Where(x => x.ColumName == item.Name).OrderByDescending(x => x.CreationDate).FirstOrDefault();
            //    inf.Add(information);
            //}
            //ViewBag.ArtsList = inf;
            //ViewBag.Arts = Context.InformationEnglishAll.Where(x => x.ColumName == "Arts");



            ViewBag.Creativity = Context.InformationEnglishAll.Where(x => x.ColumName == "Creativity").FirstOrDefault();

            //获取新增栏目
            var newcol = Context.ColunmsEnglishView.Where(x => x.ModelName == "CREATIVITY" && x.IsNew == true && x.colunmsname != "Arts").ToList();
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
        /// 资讯分类
        /// </summary>
        /// <returns></returns>
        public IActionResult IntroType(Guid id)
        {
            var information = Context.InformationEnglishAll.Find(id);
            if (information.ColumName == "Arts")
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
        /// 获取纪念品
        /// </summary>
        /// <param name="st">开始数据的下标</param>
        /// <param name="num">个数</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetSouvenir(int st, int num)
        {
            var res = Context.InformationEnglishAll.Where(e => e.ColumName == "Bestsellers").OrderByDescending(e => e.CreationDate).Skip(st).Take(num).ToList();

            var data = res.Select(e => new { e.Title, e.UrlAddress, Pic = "/upload/Information/" + e.Id.ToString() + "/" + e.Photo }).ToList();
            return Json(data);
        }




    }
}