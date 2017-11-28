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
    /// 会员系统
    /// </summary>
    public class MemberController : BaseController<MemberController, SteponContext>
    {
        private IHostingEnvironment Environment { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public MemberController(IHostingEnvironment env)
        {
            Environment = env;
        }
        /// <summary>
        /// 会员登录
        /// </summary>
        public IActionResult Login()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
        /// <summary>
        /// 忘记密码
        /// </summary>
        public IActionResult ForgetPassword()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
        /// <summary>
        /// 注册
        /// </summary>
        public IActionResult Registration()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
        /// <summary>
        /// 注册须知
        /// </summary>
        public IActionResult VIPInstructions()
        {
            //页脚
            ViewBag.Infomations = Context.InformationYoungView.Where(e => e.ColumName == "参观指南").OrderByDescending(e => e.CreationDate).ToList();
            //页脚
            var webConfigService = new WebConfigService(Services);
            ViewBag.Footer = webConfigService.GetWebConfig();
            return View();
        }
        
    }
}