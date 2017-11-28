using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Services.BaseServices;
using System;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// LinkAPI
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class LinkController : BaseController<LinkController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 获取所有Link
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllLink()
        {
            try
            {
                //var name = User.Identity.Name;
                LinkService userService = new LinkService(Services);
                var data = userService.GeyLink_All();
                if (data?.Count > 0)
                {
                    //var newdata = data.Select(e => new { e.Id, e.Name });
                    var newdata = data.Select(e => new {e.Id, e.Name, e.Url, e.LinkType,e.IsShow });

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询链接列表成功！"
                    });

                    return Json(new { code = 1, message = "数据获取成功！", data = newdata });
                }
                return Json(new { code = 4, message = "数据获取失败！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 4, message = "数据获取失败！", data = e.Message });
            }
        }

        /// <summary>
        /// 新增Link
        /// </summary>
        /// <param name="Link">Link信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult AddLink([FromBody]Link Link)
        {
            var isresult = false;
            var message = "添加失败";
            try
            {
                if (!string.IsNullOrEmpty(Link.Url) && !Link.Url.Contains("http://"))
                {
                    Link.Url = "http://" + Link.Url;
                }

                Link.Id = Guid.NewGuid();
                LinkService brandService = new LinkService(Services);
                var result = brandService.AddLink(Link);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "添加链接成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message, id = Link.Id });
        }

        /// <summary>
        /// 修改Link信息
        /// </summary>
        /// <param name="Link">Link信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateLink([FromBody]Link Link)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                if (!string.IsNullOrEmpty(Link.Url) && !Link.Url.Contains("http://"))
                {
                    Link.Url = "http://" + Link.Url;
                }

                LinkService brandService = new LinkService(Services);
                var result = brandService.ModifyLink(Link);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改链接成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 删除Link信息
        /// </summary>
        /// <param name="id">Link id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult DeleteLink(Guid id)
        {
            var isresult = false;
            var message = "删除失败！";
            try
            {
                LinkService brandService = new LinkService(Services);

                var brand = brandService.GetLinkById(id);
                if (brand != null)
                {
                    var result = brandService.DelLink(id);
                    isresult = result.State == OperationState.Success;
                    message = result.Message;

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "删除链接成功！"
                    });
                }
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 根据id查询Link详细信息
        /// </summary>
        /// <param name="id">Link id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetLink(Guid id)
        {
            try
            {
                LinkService brandService = new LinkService(Services);
                var data = brandService.GetLinkById(id);

                return Json(data);
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}

