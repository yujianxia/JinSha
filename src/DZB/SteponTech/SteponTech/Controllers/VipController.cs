using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Filter;
using Microsoft.AspNetCore.Http;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 会员
    /// </summary>
    public class VipController : BaseController<VipController, SteponContext>
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public IActionResult Index()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Login(int? type)
        {
            if (type == 1)
            {
                ViewBag.Type = "如需成为志愿者请先进行会员登录！";
            }
            if (HttpContext.Session.GetString("loginsession") != null)
            {
                //重定向
                return Redirect("Index");
            }
            else
            {
                //是否登录总线
                ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
                //母版页头数据
                ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
                //母版页footer数据
                ViewBag.Footer = Context.WebConfig.FirstOrDefault();
                //页脚跳转信息
                ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");
                return View();
            }
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public IActionResult Sign()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");
            return View();
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <returns></returns>
        public IActionResult FindPWD()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");
            return View();
        }

        /// <summary>
        /// 须知
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public IActionResult Know()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");
            return View();
        }

        /// <summary>
        /// 我的活动详情
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public IActionResult MyActitvtyDetail(int id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            ViewBag.Id = id;
            return View();
        }
    }
}