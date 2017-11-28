using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Services.BaseServices;
using SteponTech.Services.CommonService;
using System;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// RequestStatisticalAPI
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class RequestStatisticalController : BaseController<RequestStatisticalController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        /// <summary>
        /// 新增RequestStatistical
        /// </summary>
        /// <param name="RequestStatistical">RequestStatistical信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult AddRequestStatistical([FromBody]RequestStatistical RequestStatistical)
        {
            var isresult = false;
            var message = "添加失败";
            try
            {
                RequestStatistical.Id = Guid.NewGuid();
                var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                if (ip == "::1")
                {
                    ip = "127.0.0.1";
                }
                RequestStatistical.RequestIp = ip;

                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage response = httpClient.GetAsync("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip).Result;
                String resultweqweqwe = response.Content.ReadAsStringAsync().Result;
                var converResult = (RequestIpContent)Newtonsoft.Json.JsonConvert.DeserializeObject<RequestIpContent>(resultweqweqwe);
                if (converResult.data["city"].ToString() == "")
                {
                    RequestStatistical.RequestCountry = converResult.data["country"].ToString();
                }
                else
                {
                    RequestStatistical.RequestCountry = converResult.data["city"].ToString();
                }
                //return converResult;

                //JsonConvert.s
                RequestStatisticalService brandService = new RequestStatisticalService(Services);
                var result = brandService.AddRequestStatistical(RequestStatistical);
                isresult = result.State == OperationState.Success;
                message = result.Message;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message, id = RequestStatistical.Id });
        }
        /// <summary>
        /// 流量查询统计
        /// </summary>
        /// <param name="StartTime">起始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        [HttpGet("[action]/{StartTime}/{EndTime}")]
        public JsonResult SelectRequestStatistical(DateTime StartTime, DateTime EndTime)
        {
            var isresult = false;
            var message = "查询失败";
            try
            {
                if (StartTime < EndTime)
                {
                    RequestStatisticalService brandService = new RequestStatisticalService(Services);
                    var RequestStatisticalList = brandService.SelectRequestStatisticalBy(StartTime, EndTime);


                    return Json(new { result = true, message = "", RequestStatisticalList });
                }
                else
                {
                    return Json(new { result = false, message = "日期格式填写错误" });
                }


            }
            catch (Exception e)
            {
                return Json(new { result = false, message = e.Message });
            }

        }

    }
}
