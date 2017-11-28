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
    /// 志愿者
    /// </summary>
    public class VolunteerController : BaseController<VolunteerController, SteponContext>
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [VolFilter]
        public IActionResult Index()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //志愿者风采-讲解组
            ViewBag.VolunTalk = Context.InformationAll.Where(x => x.ColumName == "志愿者风采" && x.Intro == "讲解组").ToList();
            //志愿者风采-翻译组
            ViewBag.VolunTrans = Context.InformationAll.Where(x => x.ColumName == "志愿者风采" && x.Intro == "翻译组").ToList();
            //志愿者风采-摄影组
            ViewBag.VolunPhoto = Context.InformationAll.Where(x => x.ColumName == "志愿者风采" && x.Intro == "摄影组").ToList();
            //服务之星
            ViewBag.ServStar = Context.InformationAll.Where(x => x.ColumName == "服务之星").ToList();

            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");
            
            return View();
        }
        /// <summary>
        /// 活动详情
        /// </summary>
        /// <returns></returns>
        [VolFilter]
        public IActionResult Intro(string id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            ViewBag.Id = id;
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");
            return View();
        }

    }
}