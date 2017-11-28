using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using Microsoft.AspNetCore.Hosting;
using SteponTech.Services.CommonService;
using SteponTech.Filter;

namespace SteponTech.Controllers
{
    /// <summary>
    /// ��Ա
    /// </summary>
    /// <returns></returns>
    [LoginFilter]
    public class VIPController : BaseController<VIPController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="env"></param>
        public VIPController(IHostingEnvironment env)
        {
            Environment = env;
        }

        /// <summary>
        /// ��Ա�³�
        /// </summary>
        /// <returns></returns>
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
        /// �ҵĻ
        /// </summary>
        /// <returns></returns>
        public IActionResult MyActivity()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// �ҵĻ���
        /// </summary>
        /// <returns></returns>
        public IActionResult MyIntegral()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// �ҵĶһ�
        /// </summary>
        /// <returns></returns>
        public IActionResult MyExchange()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }

        /// <summary>
        /// �ҵĶһ�
        /// </summary>
        /// <returns></returns>
        public IActionResult MyInformation()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
    }
}