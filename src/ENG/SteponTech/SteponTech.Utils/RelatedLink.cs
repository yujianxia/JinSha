using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using System.Collections.Generic;
using SteponTech.Data.BaseModels;
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
                var info = _context.InformationEnglishAll.Find(new Guid(item));
                var link = "";
                switch (info.ColumName)
                {
                    case "News":
                        link += "/News/Notice?id=";
                        break;
                    case "Arts":
                    case "Cultural Creation":
                        link += "/Creation/Intro?id=";
                        break;
                    case "Exhibition for Hire":
                    case "Featured Exhibition":
                        link += "/Exhibition/Special?id";
                        break;
                    //events
                    case "Jinsha Sun Festival":
                        if (!String.IsNullOrEmpty(info.SunId))
                        {
                            link += "/Culture/SunFestival?id=";
                        }
                        else
                        {
                            link += "/Culture/SunDetail?id=";
                        }
                        break;
                    case "Upcoming lectures":
                        link += "/Culture/LectureDetail?id=";
                        break;
                    case "International Programs":
                        if (info.Title == "Seoul Lantern Festiva")
                        {
                            link += "/Culture/IntroList?id=";
                        }
                        else
                        {
                            link += "/Culture/InternationalDetail?id=";
                        }
                        break;
                    case "Ten-year Jinsha":
                    case "International Museum Day":
                    case "National Cultural Heritage Day":
                    case "Cultural Events":
                        link += "/Culture/ActivityDetail?id=";
                        break;
                    //collection
                    case "Wallpapers":
                        link += "/Collection/Wallpaper?id=";
                        break;

                    //visit
                    case "Ticketing":
                    case "Interpreter":
                    case "Food":
                        link += "/VisitGuide/ServiceInfo?id=";
                        break;

                    case "Information":
                        link += "/Culture/SunDetail?id=";
                        break;
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


       

        public Dictionary<string, string> GetLink3(string informationid)
        {
            string[] informations = informationid.Split(',');
            var dic = new Dictionary<string, string>();
            foreach (var item in informations)
            {
                var info = _context.InformationEnglishAll.Find(new Guid(item));
                var link = "";
                switch (info.ColumName)
                {
                    case "Announcements":
                    case "News":
                        link += "/News/Notice?id=";
                        break;
                    case "Arts":
                    case "Cultural Creation":
                        link += "/Creation/Intro?id=";
                        break;
                    case "Exhibition Hall":
                        link += "/Exhibition/Display?id=";
                        break;
                    case "Exhibition for Hire":
                    case "Featured Exhibition":
                        link += "/Exhibition/Special?id=";
                        break;
                    //events
                    case "Jinsha Sun Festival":
                        if (!String.IsNullOrEmpty(info.SunId))
                        {
                            link += "/Culture/SunFestival?id=";
                        }
                        else
                        {
                            link += "/Culture/SunDetail?id=";
                        }
                        break;
                    case "Upcoming lectures":
                        link += "/Culture/LectureDetail?id=";
                        break;
                    case "International Programs":
                        if (info.Title == "Seoul Lantern Festiva")
                        {
                            link += "/Culture/IntroList?id=";
                        }
                        else
                        {
                            link += "/Culture/InternationalDetail?id=";
                        }
                        break;
                    case "Ten-year Jinsha":
                    case "International Museum Day":
                    case "National Cultural Heritage Day":
                    case "Cultural Events":
                        link += "/Culture/ActivityDetail?id=";
                        break;
                    //collection
                    case "Wallpapers":
                        link += "/Collection/Wallpaper?id=";
                        break;

                    //visit
                    case "Ticketing":
                    case "Interpreter":
                    case "Food":
                        link += "/VisitGuide/ServiceInfo?id=";
                        break;
		    case "Information":
			link += "/Culture/SunDetail?id=";
                        break;
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
