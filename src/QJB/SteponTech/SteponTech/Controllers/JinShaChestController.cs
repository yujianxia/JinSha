using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Services.CommonService;
using SteponTech.Data.BaseModels;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 金沙百宝箱
    /// </summary>
    public class JinShaChestController : BaseController<StrategyController, SteponContext>
    {
        /// <summary>
        /// 金沙百宝箱
        /// </summary>
        public IActionResult Index()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 金沙学习手册
        /// </summary>
        public IActionResult JinShaStudy()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 金沙学习手册详情
        /// </summary>
        public IActionResult JinShaStudyDeatil(Guid id)
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();

            InformationYoungView ifm = Context.InformationYoungView.Find(id);
            return View(ifm);
        }

        /// <summary>
        /// 金沙教学资源
        /// </summary>
        public IActionResult JinShaResource()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// 分页
        /// </summary>
        public JsonResult PageSelect(int page, int pagesize)
        {
            var data = Context.InformationYoungView.OrderByDescending(x => x.CreationDate).Skip(page * pagesize).Take(pagesize).ToList();

            return Json(data);
        }
    }
}