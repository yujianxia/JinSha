using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using Microsoft.Extensions.Logging;
using SteponTech.Services.BaseServices;

namespace SteponTech.ApiControllers
{
    /// <summary>
    /// 获取资讯详情
    /// </summary>
    [Produces("application/json")]
    [Route("api/GetInfo")]
    public class GetInfoController : BaseController<GetInfoController, SteponContext>
    {
        /// <summary>
        /// 根据资讯Id查询资讯详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public JsonResult GetDetails(string id)
        {
            var title = "";
            var intro = "";
            var photo = "";
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var informationService = new InformationService(Services);
                    var info = informationService.GetInformationById(Guid.Parse(id));
                    if (info != null)
                    {
                        title = info.Title;
                        intro = info.Intro;
                        photo = info.Photo;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.Message + "\r\n");
            }
            return Json(new { Title = title, Intro = intro, Photo = photo });
        }
    }
}