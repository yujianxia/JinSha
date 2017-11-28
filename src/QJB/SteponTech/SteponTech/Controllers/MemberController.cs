using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using Microsoft.AspNetCore.Hosting;
using SteponTech.Services.CommonService;

namespace SteponTech.Controllers
{
    /// <summary>
    /// ��Աϵͳ
    /// </summary>
    public class MemberController : BaseController<MemberController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="env"></param>
        public MemberController(IHostingEnvironment env)
        {
            Environment = env;
        }
        /// <summary>
        /// ��Ա��¼
        /// </summary>
        public IActionResult Login()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
        /// <summary>
        /// ��������
        /// </summary>
        public IActionResult ForgetPassword()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
        /// <summary>
        /// ע��
        /// </summary>
        public IActionResult Registration()
        {
            //ҳ��
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "�ι�ָ��").OrderByDescending(e => e.CreationDate).ToList();
            //ҳ��
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
        /// <summary>
        /// ע����֪
        /// </summary>
        public IActionResult VIPInstructions()
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