using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using System.Collections.Generic;
using SteponTech.Data.BaseModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SteponTech.Utils
{
    public class RelatedLink : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public RelatedLink(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        public Dictionary<string, string> GetLink(string informationid)
        {
            List<string> informations = JsonConvert.DeserializeObject<List<string>>(informationid);
            var dic = new Dictionary<string, string>();
            foreach (var item in informations)
            {
                var info = _context.InformationAll.Find(new Guid(item));
                var link = "";
                switch (info.ColumName)
                {
                    case "金沙快讯":
                        link += "/News/Notice?id=";
                        break;
                    case "文创作品":
                    case "文化创作":
                        link += "/Creation/Intro?id=";
                        break;
                    case "巡回展览":
                    case "特展":
                        link += "/Exhibition/Special?id=";
                        break;
                    //events
                    case "金沙太阳节":
                        if (!String.IsNullOrEmpty(info.SunId))
                        {
                            link += "/Culture/SunFestival?id=";
                        }
                        else
                        {
                            link += "/Culture/SunDetail?id=";
                        }
                        break;
                    case "讲座预告":
                        link += "/Culture/LectureDetail?id=";
                        break;
                    case "国际文化交流":
                        if (info.Title == "首尔灯节")
                        {
                            link += "/Culture/IntroList?id=";
                        }
                        else
                        {
                            link += "/Culture/InternationalDetail?id=";
                        }
                        break;
                    case "金沙十年":
                    case "国际博物馆日":
                    case "文化遗产日":
                    case "系列文化活动":
                        link += "/Culture/ActivityDetail?id=";
                        break;
                    //collection
                    case "壁纸":
                        link += "/Collection/Wallpaper?id=";
                        break;

                    //visit
                    case "票务服务":
                    case "讲解服务":
                    case "餐饮服务":
                        link += "/VisitGuide/Service?id=";
                        break;
                    case "活动资讯":
                        link += "/Culture/SunDetail?id=";break;
                        //about
                        //空

                }
                if (!string.IsNullOrEmpty(link))
                {
                    link += info.Id.ToString();
                }
                dic.Add(link, info.Title);
            }
            return dic;
        }




        

        [HttpGet("[action]")]
        public Dictionary<string, string> GetLink3(string informationid)
        {
            string[] informations = informationid.Split(',');
            var dic = new Dictionary<string, string>();
            foreach (var item in informations)
            {
                var info = _context.InformationAll.Find(new Guid(item));
                var link = "";
                switch (info.ColumName)
                {
                    case "金沙公告":
                    case "金沙快讯":
                        link += "/News/Notice?id=";
                        break;
                    case "文创作品":
                    case "文化创作":
                        link += "/Creation/Intro?id=";
                        break;
                    case "陈列馆":
                        link += "/Exhibition/Display?id=";
                        break;
                    case "巡回展览":
                    case "特展":
                        link += "/Exhibition/Special?id=";
                        break;
                    //events
                    case "金沙太阳节":
                        if (!String.IsNullOrEmpty(info.SunId))
                        {
                            link += "/Culture/SunFestival?id=";
                        }
                        else
                        {
                            link += "/Culture/SunDetail?id=";
                        }
                        break;
                    case "讲座预告":
                        link += "/Culture/LectureDetail?id=";
                        break;
                    case "国际文化交流":
                        if (info.Title == "首尔灯节")
                        {
                            link += "/Culture/IntroList?id=";
                        }
                        else
                        {
                            link += "/Culture/InternationalDetail?id=";
                        }
                        break;
                    case "金沙十年":
                    case "国际博物馆日":
                    case "文化遗产日":
                    case "系列文化活动":
                        link += "/Culture/ActivityDetail?id=";
                        break;
                    //collection
                    case "壁纸":
                        link += "/Collection/Wallpaper?id=";
                        break;

                    //visit
                    case "票务服务":
                    case "讲解服务":
                    case "餐饮服务":
                        link += "/VisitGuide/Service?id=";
                        break;
                    case "活动资讯":
                        link += "/Culture/SunDetail?id="; break;
                        //about
                        //空

                }
                if (!string.IsNullOrEmpty(link))
                {
                    link += info.Id.ToString();
                }
                dic.Add(link, info.Title + "*" + info.CreationDate.ToString("yyyy-MM-dd"));
            }
            return dic;
        }
    }
}
