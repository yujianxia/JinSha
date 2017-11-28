using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.ApiControllers.Admin;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Utils;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.Controllers
{
    /// <summary>
    /// 金沙资讯
    /// </summary>
    public class NewsController : BaseController<InformationController, SteponContext>
    {
        /// <summary>
        /// 首页
        /// </summary>
        // GET: /<controller>/
        public IActionResult Index()
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            //绑定banner
            ViewBag.Banner = Context.BannerEnglishView.FirstOrDefault(e => e.ModelName == "NEWS");

            //获取新增栏目
            var newcol = Context.ColunmsEnglishView.Where(x => x.ModelName == "NEWS" && x.IsNew == true).ToList();
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
        /// 金沙快讯
        /// </summary>
        /// <returns></returns>
        public IActionResult News(Guid id)
        {
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            InformationAll ifm = Context.InformationEnglishAll.Find(id);
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
            //母版页头数据
            ViewBag.Head = Context.ModelsEnglish.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfigEnglish.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsEnglishView.Where(x => x.Name == "Opening Hours" || x.Name == "Getting Here" || x.Name == "Services" || x.Name == "Visitor Regulations").ToList();

            InformationAll ifm = Context.InformationEnglishAll.Find(id);
            RelatedLink rl = new RelatedLink(Services);
            if (!String.IsNullOrEmpty(ifm.InformationId))
            {
                ViewBag.Aside = rl.GetLink(ifm.InformationId);
            }
            return View(ifm);
        }
    }
}
