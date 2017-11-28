using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Utils.ImageControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 典藏藏品
    /// </summary>
    public class CollectionController : BaseController<CollectionController, SteponContext>
    {
        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public CollectionController(IHostingEnvironment env)
        {
            Environment = env;
        }

        /// <summary>
        /// 典藏珍品首页
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

            //Banner
            ViewBag.Banner = Context.BannerEnglishView.FirstOrDefault(x => x.ModelName == "COLLECTIONS");
            //典藏珍品
            var cols= Context.ColunmsEnglishView.Where(x => x.Name == "Collections" || x.Name == "Gold" || x.Name == "Bronze" || x.Name == "Jade"
                    || x.Name == "Stone" || x.Name == "Pottery" || x.Name == "Lacquer").ToList();

            ViewBag.Colect = cols.Find(e => e.Name == "Collections");
            ViewBag.GoldCol = cols.Find(e => e.Name == "Gold");
            ViewBag.BronzeCol = cols.Find(e => e.Name == "Bronze");
            ViewBag.JadeCol = cols.Find(e => e.Name == "Jade");
            ViewBag.StoneCol = cols.Find(e => e.Name == "Stone");
            ViewBag.PotteryCol = cols.Find(e => e.Name == "Pottery");
            ViewBag.LacquerCol = cols.Find(e => e.Name == "Lacquer");



            var infos = Context.InformationEnglishAll.Where(x => x.ColumName == "Collections" || x.ColumName == "Gold" || x.ColumName == "Bronze" || x.ColumName == "Jade"
                   || x.ColumName == "Stone" || x.ColumName == "Pottery" || x.ColumName == "Lacquer").ToList();


            //金器
            ViewBag.Gold = infos.Where(e => e.ColumName == "Gold").ToList();
            //青铜器
            ViewBag.Bronze = infos.Where(e => e.ColumName == "Bronze").ToList();
            //玉器
            ViewBag.Jade = infos.Where(e => e.ColumName == "Jade").ToList();
            //石器
            ViewBag.Stone = infos.Where(e => e.ColumName == "Stone").ToList();
            //陶器
            ViewBag.Pottery = infos.Where(e => e.ColumName == "Pottery").ToList();
            //漆木器
            ViewBag.Lacquer = infos.Where(e => e.ColumName == "Lacquer").ToList();


            //获取壁纸Information
            ViewBag.Wallpapers = Context.InformationEnglishAll.Where(x => x.ColumName == "Wallpapers").OrderByDescending(e=>e.CreationDate).Take(9).ToList();

            //获取新增栏目
            var newcol = Context.ColunmsEnglishView.Where(x => x.ModelName == "COLLECTIONS" && x.IsNew == true).ToList();
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
        /// 更多壁纸
        /// </summary>
        /// <returns></returns>
        public IActionResult Background()
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();
            ViewBag.Wallpapers = Context.InformationEnglishAll.Where(x => x.ColumName == "Wallpapers").OrderByDescending(e => e.CreationDate).Take(9).ToList();
            return View();
        }  
        /// <summary>
        /// 壁纸详情
        /// </summary>
        /// <returns></returns>
        public IActionResult Wallpaper(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();
            ViewBag.Wallpapers = Context.InformationEnglishAll.FirstOrDefault(x => x.Id==id);
            return View();
        }

        /// <summary>
        /// 壁纸下载
        /// </summary>
        /// <returns></returns>
        public IActionResult WallpaperDownload(int width,int height, Guid id)
        {
            var paper= Context.InformationEnglishAll.FirstOrDefault(x => x.Id == id);
            var pathstring = Path.Combine(Environment.WebRootPath, "upload", "information", id.ToString());
            var img = new ImageControl();
            var byt= img.resizeImage(paper.Photo, pathstring, width, height);
            FileContentResult fs = File(byt, "application/octet-stream", "wallpaper.jpg");
            return fs;
        }
    }
}