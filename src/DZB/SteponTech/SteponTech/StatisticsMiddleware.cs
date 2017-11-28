using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using System.Collections.Generic;
using Dapper;
using SteponTech.Data.BaseModels;
using Stepon.EntityFrameworkCore;
using SteponTech.Data.CommonModels;
using SteponTech.Data.TrunkSystem;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SteponTech
{
    public class StatisticsMiddleware : ServiceContract<SteponContext>
    {
        private readonly RequestDelegate next;
        private readonly SteponContext _context;
        public StatisticsMiddleware(IServiceProvider services, RequestDelegate next) : base(services)
        {
            _context = services.GetService<SteponContext>();
            this.next = next;
        }
        public async Task Invoke(HttpContext Context)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    AysncPublic(Context);
                });


                await next(Context);
            }
            catch (Exception e)
            {
                await next(Context);
            }

        }
        private static object o = new object();
        public void AysncPublic(HttpContext Context)
        {
            if (!Context.Request.Path.ToString().Contains("/api") && !Context.Request.Path.ToString().Contains("."))
            {
                var title = ResultModelName(Context.Request.Path);
                if (!string.IsNullOrEmpty(title))
                {
                    RequestStatistical rs = new RequestStatistical();
                    rs.Id = Guid.NewGuid();
                    var ip = Context.Connection.RemoteIpAddress.ToString();
                    if (ip == "::1")
                    {
                        ip = "127.0.0.1";
                    }
                    rs.RequestIp = ip;
                    rs.RequestUrl = Context.Request.Host + Context.Request.Path;
                    rs.UrlName = title;
                    var dt1 = DateTime.Now;
                    System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                    System.Net.Http.HttpResponseMessage response = httpClient.GetAsync("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip).Result;
                    String resultweqweqwe = response.Content.ReadAsStringAsync().Result;
                    var converResult = (RequestIpContent)Newtonsoft.Json.JsonConvert.DeserializeObject<RequestIpContent>(resultweqweqwe);
                    if (converResult.data["city"].ToString() == "")
                    {
                        rs.RequestCountry = converResult.data["country"].ToString();
                    }
                    else
                    {
                        rs.RequestCountry = converResult.data["city"].ToString();
                    }

                    var dt = DateTime.Now - dt1;
                    //await Task.Factory.StartNew(() =>
                    //{
                    //    _context.Create(rs, true, true);
                    //});
                    lock (o)
                    {
                        _context.Create(rs, true, true);
                    }
                }
            }
        }
        /// <summary>
        /// 返回页面名称方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string ResultModelName(string url)
        {
            url = url.ToLower();
            if (url == "/" || url.Contains("/jinsha/index"))
            {
                return "首页";
            }
            else if (url.Contains("/visitguide/index") || url == "/visitguide")
            {
                return "参观指南";
            }
            else if (url.Contains("/visitguide/service"))
            {
                return "服务信息";
            }
            else if (url.Contains("/about/index") || url == "/about")
            {
                return "关于金沙";
            }
            else if (url.Contains("/about/into"))
            {
                return "金沙简介";
            }
            else if (url.Contains("/exhibition/index") || url == "/exhibition")
            {
                return "展览导览";
            }
            else if (url.Contains("/exhibition/ruins"))
            {
                return "遗迹馆";
            }
            else if (url.Contains("/exhibition/display"))
            {
                return "陈列馆";
            }
            else if (url.Contains("/exhibition/special"))
            {
                return "展览详情";
            }
            else if (url.Contains("/news/index"))
            {
                return "金沙资讯";
            }
            else if (url.Contains("/news/news"))
            {
                return "金沙快讯";
            }
            else if (url.Contains("/news/notice"))
            {
                return "金沙公告";
            }
            else if (url.Contains("/creation/index"))
            {
                return "艺术金沙";
            }
            else if (url.Contains("/creation/introlist"))
            {
                return "文创列表";
            }
            else if (url.Contains("/creation/intro"))
            {
                return "文创作品";
            }
            else if (url.Contains("/collection/index"))
            {
                return "典藏珍品";
            }
            else if (url.Contains("/collection/wallpaper"))
            {
                return "精美桌面";
            }
            else if (url.Contains("/culture/index"))
            {
                return "文化活动";
            }
            else if (url.Contains("/culture/sunfestival"))
            {
                return "金沙太阳节";
            }
            else if (url.Contains("/culture/lecturedetail"))
            {
                return "金沙讲坛";
            }
            else if (url.Contains("/culture/introList"))
            {
                return "首尔灯节";
            }
            else if (url.Contains("/culture/internationaldetail"))
            {
                return "国际文化交流";
            }
            else if (url.Contains("/culture/activitydetail"))
            {
                return "系列文化活动";
            }
            else if (url.Contains("/culture/participate"))
            {
                return "我要参与";
            }
            else if (url.Contains("/culture/activityinfo"))
            {
                return "活动详情";
            }
            else if (url.Contains("/vip"))
            {
                return "金沙会员";
            }
            else if (url.Contains("/colunteer"))
            {
                return "志愿者";
            }
            else
            {
                return null;
            }
        }
    }
}
