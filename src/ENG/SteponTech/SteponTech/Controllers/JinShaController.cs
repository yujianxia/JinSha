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

namespace SteponTech.Controllers
{
    /// <summary>
    /// 金沙首页
    /// </summary>
    public class JinShaController : BaseController<ExhibitionController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public JinShaController(IHostingEnvironment env)
        {
            Environment = env;
        }
        /// <summary>
        /// 主页
        /// </summary>
        public IActionResult Index()
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            ViewBag.Banner = Context.BannerEnglishView.Where(x => x.ModelName == "HOME" && x.IsShow == true);
            ViewBag.FirstExhibition = Context.InformationEnglishAll.Where(x => x.ColumName == "Featured Exhibition").OrderByDescending(x=>x.CreationDate).FirstOrDefault();
            ViewBag.JinShaSite = Context.InformationEnglishAll.Where(x => x.ColumName == "About Us").FirstOrDefault();
            ViewBag.Collections = Context.InformationEnglishAll.Where(x => x.ColumName == "COLLECTIONS").Where(e => e.ModelName == "HOME").Take(6).OrderByDescending(e => e.CreationDate).ToList();


            var guidelist= Context.InformationEnglishAll.Where(x => x.ColumName == "GUIDE" && x.ModelName == "HOME").OrderByDescending(e => e.CreationDate).ToList();
            ViewBag.GuidesEX = guidelist.FirstOrDefault(x=>x.Name == "true");
            ViewBag.RelicsHall = guidelist.FirstOrDefault(x => x.Title == "Relics Hall");
            ViewBag.ExhibitionHall = guidelist.FirstOrDefault(x => x.Title == "Exhibition Hall");
            ViewBag.CulturalLandscape = guidelist.FirstOrDefault(x => x.Title == "Cultural Landscape");

            ViewBag.OnlineBooking = Context.InformationEnglishAll.Where(x => x.ColumName == "Online Booking").FirstOrDefault();
            //获取新增栏目
            var newcol = Context.ColunmsEnglishView.Where(x => x.ModelName == "HOME" && x.IsNew == true).ToList();
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
        /// 搜索列表页面
        /// </summary>
        public IActionResult SearchResults(string searchstring)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            ViewBag.Search = searchstring;

            return View();
        }

        /// <summary>
        /// 新增详情页面
        /// </summary>
        public IActionResult NewDetail(Guid id)
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
                var ser = new RelatedLink(Services);
                ViewBag.Link = ser.GetLink(info.InformationId);
            }
            return View();
        }

        /// <summary>
        /// banner详情页面
        /// </summary>
        public IActionResult BannerDetail(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();
            BannerView bv = Context.BannerEnglishView.Find(id);
            ViewBag.Banner = bv;

            RelatedLink rl = new RelatedLink(Services);
            if (!String.IsNullOrEmpty(bv.ReturnUrl))
            {
                ViewBag.Aside = rl.GetLink(bv.ReturnUrl);
            }

            return View();
        }

    }
}