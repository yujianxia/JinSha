using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stepon.Data;
using Stepon.Data.PgSql;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Services.CommonService;
using SteponTech.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteponTech.ApiControllers
{
    /// <summary>
    /// 通用数据搜索API
    /// </summary>
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class SearchController : BaseController<SearchController, SteponContext>
    {
        /// <summary>
        /// 全局内容模糊搜搜
        /// </summary>
        /// <param name="input">输入内容</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpPost("{input},{page}")]
        public JsonResult Search(string input, int page)
        {
            var code = 2;
            var massage = "获取数据失败！";
            var count = 0;
            var dic = new Dictionary<string, string>();
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    var dara = Context.InformationAll.Where(x => x.ColumName == "金沙快讯" || x.ColumName == "文化创作" || x.ColumName == "文创作品" || x.ColumName == "陈列馆" || x.ColumName == "巡回展览" || x.ColumName == "特展" || x.ColumName == "金沙太阳节" || x.ColumName == "讲座预告" || x.ColumName == "国际文化交流" || x.ColumName == "金沙十年" || x.ColumName == "国际博物馆日" || x.ColumName == "文化遗产日" || x.ColumName == "系列文化活动" || x.ColumName == "壁纸" || x.ColumName == "票务服务" || x.ColumName == "讲解服务" || x.ColumName == "餐饮服务" || x.ColumName == "金沙公告" || x.ColumName == "活动资讯").Where(e => e.Title.Contains(input)).OrderByDescending(e => e.LastUpdate).ToList();
                    count = dara.Count;
                    var infos = dara.Skip(page * 20).Take(20).ToList();

                    RelatedLink lin = new RelatedLink(Services);


                    if (infos.Count != 0)
                    {
                        if (infos.Count < 2)
                        {
                            dic = lin.GetLink3(infos[0].Id.ToString());
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            for (var i = 0; i < infos.Count - 1; i++)
                            {
                                sb.Append(infos[i].Id.ToString());
                                sb.Append(",");
                            }
                            sb.Append(infos[infos.Count - 1].Id.ToString());

                            dic = lin.GetLink3(sb.ToString());
                        }
                    }
                    code = 1;
                    massage = "数据获取成功！";
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            var data = dic.ToList();
            return Json(new { code, massage, count, data });

        }

        /// <summary>
        /// 统计信息查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult SearchStatistics()
        {
            var code = 2;
            var massage = "数据获取失败！请重试！";
            var data = new RequestStatisticalByAll();
            try
            {
                int weeknow = Convert.ToInt32(DateTime.Now.Date.DayOfWeek);
                DateTime endtime = DateTime.Now.Date.AddDays(-(weeknow));
                DateTime startime = endtime.AddDays(-6);
                endtime = endtime.AddHours(23).AddMinutes(59).AddSeconds(59);
                RequestStatisticalService rss = new RequestStatisticalService(Services);
                data = rss.SelectRequestStatisticalBy(startime, endtime);
                code = 1;
                massage = "获取成功！";
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            return Json(new { code, massage, data });

        }
        /// <summary>
        /// 资讯信息查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{infIndex}/{infSize}/{annIndex}/{annSize}")]
        public string SearchInformation(int infIndex, int infSize, int annIndex, int annSize)
        {
            var code = 2;
            var massage = "数据获取失败！请重试！";
            var ip = Utils.DHSH.Utils.ConfigHelper.GetValue("ServerIp");
            var imgaddress = Utils.DHSH.Utils.ConfigHelper.GetValue("ServerImgAddress");
            var InfoListUrl = ip + "/News/Index";
            var InfList = new List<InformationUrl>();
            var AnnList = new List<InformationUrl>();
            try
            {
                var inforamationall = Context.InformationAll.Where(x => x.ColumName == "金沙快讯" || x.ColumName == "金沙公告");
                var inforamation = inforamationall.Where(x => x.ColumName == "金沙快讯").OrderByDescending(e => e.LastUpdate).Skip((infIndex - 1) * infSize).Take(infSize).ToList();
                var announcement = inforamationall.Where(x => x.ColumName == "金沙公告").OrderByDescending(e => e.LastUpdate).Skip((annIndex - 1) * annSize).Take(annSize).ToList();
                foreach (var item in inforamation)
                {
                    InfList.Add(new InformationUrl
                    {
                        Id = item.Id,
                        CreationDate = item.CreationDate,
                        LastUpdate = item.LastUpdate,
                        Intro = item.Intro,
                        Title = item.Title,
                        Content = item.Content,
                        Photo = ip + imgaddress + item.Id + "/" + item.Photo,
                        ColumName = item.ColumName,
                        ModelName = item.ModelName,
                        NewsDetailUrl = ip + "/News/News?id=" + item.Id
                    });
                }

                foreach (var item in announcement)
                {
                    AnnList.Add(new InformationUrl
                    {
                        Id = item.Id,
                        CreationDate = item.CreationDate,
                        LastUpdate = item.LastUpdate,
                        Intro = item.Intro,
                        Title = item.Title,
                        Content = item.Content,
                        Photo = ip + imgaddress + item.Id + "/" + item.Photo,
                        ColumName = item.ColumName,
                        ModelName = item.ModelName,
                        NewsDetailUrl = ip + "/News/News?id=" + item.Id
                    });
                }

                code = 1;
                massage = "获取成功！";
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            var ssss = new { code, massage, InfoListUrl, InfList, AnnList };
            var asaadasd = Newtonsoft.Json.JsonConvert.SerializeObject(ssss);
            return RestSharp.Extensions.MonoHttp.HttpUtility.UrlEncode(asaadasd);
        }
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }
        /// <summary>
        /// 返回资讯实体
        /// </summary>
        public class InformationUrl : Stepon.EntityFrameworkCore.BaseEntity
        {
            /// <summary>
            /// 资讯介绍
            /// </summary>
            public System.String Intro { get; set; }

            /// <summary>
            /// 资讯标题
            /// </summary>
            public System.String Title { get; set; }

            /// <summary>
            /// 资讯内容
            /// </summary>
            public System.String Content { get; set; }

            /// <summary>
            /// 展示图片地址
            /// </summary>
            public System.String Photo { get; set; }

            /// <summary>
            /// 栏目名称
            /// </summary>
            public System.String ColumName { get; set; }

            /// <summary>
            /// 板块名称
            /// </summary>
            public System.String ModelName { get; set; }

            /// <summary>
            /// 详情链接地址
            /// </summary>
            public System.String NewsDetailUrl { get; set; }
        }
    }
}