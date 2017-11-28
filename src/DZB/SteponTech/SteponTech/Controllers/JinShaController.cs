using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Utils;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart;
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Model.Config;
using SteponTech.Utils.DHSH.Utils;
using SteponTech.Utils.DHSH;
using SteponTech.Utils.DHSH.Model;
using static SteponTech.Controllers.CollectionController;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 金沙主控制器
    /// </summary>
    public class JinShaController : BaseController<JinShaController, SteponContext>
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
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {
                //是否登录总线
                ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
                //母版页头数据
                ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
                //母版页footer数据
                ViewBag.Footer = Context.WebConfig.FirstOrDefault();
                //页脚跳转信息
                ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");
                //Banner
                ViewBag.Banner = Context.BannerView.Where(x => x.ModelName == "首页"&&x.IsShow==true);
                ViewBag.FirstExhibition = Context.InformationAll.Where(x => x.ColumName == "特展").FirstOrDefault();
                ViewBag.JinShaSite = Context.InformationAll.Where(x => x.ColumName == "金沙简介").FirstOrDefault();
                ViewBag.OnlineBooking = Context.InformationAll.Where(x => x.ColumName == "预约服务").FirstOrDefault();
                ViewBag.Guide = Context.InformationAll.Where(x => x.ColumName == "展览导览").Where(e => e.ModelName == "首页").FirstOrDefault();
                ViewBag.RelicsHall = Context.InformationAll.Where(x => x.ColumName == "遗迹馆").FirstOrDefault();
                ViewBag.ExhibitionHall = Context.InformationAll.Where(x => x.ColumName == "陈列馆").FirstOrDefault();


                //典藏珍品

                var res = WenwuList("1", "6");
                //解析数据列表 
                var wenwulist = new List<WenWu>();
                foreach (var data in res)
                {
                    wenwulist.Add(new WenWu()
                    {
                        SinNo = data.column[0].value,
                        Name = data.column[1].value,
                        Type = data.column[2].value,
                        Year = data.column[3].value,
                        Character = data.column[4].value,
                        Complete = data.column[5].value,
                        Size = data.column[6].value,
                        Weight = data.column[7].value,
                        Source = data.column[8].value,
                        Level = data.column[9].value,
                        Pic = data.column[10].value,
                        TDPic = data.column[11].value,
                        TDObj = data.column[12].value,
                    });
                }
                ViewBag.Collections = wenwulist.Take(6).ToList();
                //ViewBag.Collections = Context.InformationAll.Where(x => x.ColumName == "典藏精品").Where(e => e.ModelName == "首页").Take(6).OrderByDescending(e => e.CreationDate).ToList();
                
                ViewBag.CulturalLandscape = Context.InformationAll.Where(x => x.ColumName == "文化景观").FirstOrDefault();
                return View();
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");

            }




         



            return View();

        }





        /// <summary>
        /// 新增详情页面
        /// </summary>
        public IActionResult NewDetail(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            var info = Context.InformationAll.FirstOrDefault(e => e.Id == id);
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
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            BannerView bv = Context.BannerView.Find(id);
            ViewBag.Banner = bv;

            RelatedLink rl = new RelatedLink(Services);
            if (!String.IsNullOrEmpty(bv.ReturnUrl))
            {
                ViewBag.Aside = rl.GetLink(bv.ReturnUrl);
            }

            return View();
        }

        /// <summary>
        /// 搜索列表页面
        /// </summary>
        public IActionResult SearchResults(string searchstring)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            ViewBag.Search = searchstring;

            return View();
        }


        /// <summary>
        /// 获取精品馆藏文物
        /// </summary>
        /// <param name="pageIndex">起始页 最低为1</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<RDataRow> WenwuList(string pageIndex, string pageSize)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                //if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败
                if (!flag) return null;//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("WenWu"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Utils.DHSH.Model.Part.Post.SmallPostPart.Parameter> {
                     new Utils.DHSH.Model.Part.Post.SmallPostPart.Parameter{ name="pageIndex",value="0"},
                      new Utils.DHSH.Model.Part.Post.SmallPostPart.Parameter{ name="pageNum",value="6"}
                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    //return Json(new { code = 3, message = "操作失败", data = error.message });//获取数据失败
                    return null;//获取数据失败
                }
                else
                {

                    //return Json(new { code = 1, message = "查询数据成功！", data = data.body.dataTable.dataRow });//获取数据失败 
                    return data.body.dataTable.dataRow;//获取数据失败 

                }


            }
            catch (Exception e)
            {
                //return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
                return null;//获取数据失败
            }
        }
    }
}