using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stepon.Data;
using Stepon.Data.PgSql;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
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
    [Produces("application/json")]
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
                    var dara = Context.InformationEnglishAll.Where(x => x.ColumName == "Announcements" || x.ColumName == "News" || x.ColumName == "Arts" || x.ColumName == "Cultural Creation" || x.ColumName == "Exhibition Hall" || x.ColumName == "Exhibition for Hire" || x.ColumName == "Featured Exhibition" || x.ColumName == "Jinsha Sun Festival" || x.ColumName == "Upcoming lectures" || x.ColumName == "International Programs" || x.ColumName == "Ten-year Jinsha" || x.ColumName == "International Museum Day" || x.ColumName == "National Cultural Heritage Day" || x.ColumName == "Cultural Events" || x.ColumName == "Wallpapers" || x.ColumName == "Ticketing" || x.ColumName == "Interpreter" || x.ColumName == "Food"|| x.ColumName == "Information").Where(e => e.Title.Contains(input)).OrderByDescending(e => e.LastUpdate).ToList();
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
    }
}