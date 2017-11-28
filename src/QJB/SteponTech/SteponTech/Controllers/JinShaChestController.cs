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
    /// ��ɳ�ٱ���
    /// </summary>
    public class JinShaChestController : BaseController<StrategyController, SteponContext>
    {
        /// <summary>
        /// ��ɳ�ٱ���
        /// </summary>
        public IActionResult Index()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// ��ɳѧϰ�ֲ�
        /// </summary>
        public IActionResult JinShaStudy()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// ��ɳѧϰ�ֲ�����
        /// </summary>
        public IActionResult JinShaStudyDeatil(Guid id)
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();

            InformationYoungView ifm = Context.InformationYoungView.Find(id);
            return View(ifm);
        }

        /// <summary>
        /// ��ɳ��ѧ��Դ
        /// </summary>
        public IActionResult JinShaResource()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// ��ҳ
        /// </summary>
        public JsonResult PageSelect(int page, int pagesize)
        {
            var data = Context.InformationYoungView.OrderByDescending(x => x.CreationDate).Skip(page * pagesize).Take(pagesize).ToList();

            return Json(data);
        }
    }
}