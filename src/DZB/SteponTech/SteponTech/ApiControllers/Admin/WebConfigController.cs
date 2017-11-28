using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Data.CommonModels;
using SteponTech.Services.BaseServices;
using SteponTech.Services.CommonService;
using System;
using System.IO;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwWebConfig/?WebConfigID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// 网站配置API
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class WebConfigController : BaseController<WebConfigController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private IHostingEnvironment Environment { get; }
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WebConfigController(IConfiguration config)
        {
            _configuration = config;
        }
        /// <summary>
        /// 获取所有WebConfig
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllWebConfig()
        {
            try
            {
                //var name = User.Identity.Name;
                WebConfigService userService = new WebConfigService(Services);
                var data = userService.GeyWebConfig_All();
                if (data?.Count > 0)
                {
                    data.FirstOrDefault().Watermark = "/upload/Foot/waterMark.jpg";
                    data.FirstOrDefault().Wechat = "/upload/Foot/weChat.jpg";

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询网站配置成功！"
                    });

                    return Json(new { code = 1, message = "数据获取成功！", data = data });
                }
                else
                {
                    WebConfigService brandService = new WebConfigService(Services);
                    var result = brandService.AddWebConfig(new WebConfig { Id = Guid.NewGuid(), WebName = "金沙遗址博物馆官网", CompanyName = "金沙遗址博物馆" });

                    data = userService.GeyWebConfig_All();
                    if (data?.Count > 0)
                    {
                        //var newdata = data.Select(e => new { e.Id, e.Name });
                        var loginusername = User.Identity.Name;
                        Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                        SysLog.AddLog(new Data.CommonModel.SystemLog
                        {
                            UserName = loginusername,
                            OpreationMode = "用户：" + loginusername + "查询网站配置成功！"
                        });

                        return Json(new { code = 1, message = "数据获取成功！", data = data });
                    }
                }
                return Json(new { code = 4, message = "数据获取失败！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 4, message = "数据获取失败！", data = e.Message });
            }
        }

        /// <summary>
        /// 修改WebConfig信息
        /// </summary>
        /// <param name="WebConfig">WebConfig信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateWebConfig([FromBody]WebConfig WebConfig)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                WebConfigService brandService = new WebConfigService(Services);
                if (!string.IsNullOrEmpty(WebConfig.ClosedDay))
                {
                    WebConfig.ClosedDay = WebConfig.ClosedDay.Replace('，', ',');
                }
                var result = brandService.ModifyWebConfig(WebConfig);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改网站配置成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }
    }
}

