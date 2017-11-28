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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// ModelsAPI
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class ModelsController : BaseController<ModelsController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 获取所有Models
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllModels()
        {
            try
            {
                //var name = User.Identity.Name;
                ModelsService userService = new ModelsService(Services);
                var data = userService.GeyModels_All();
                if (data?.Count > 0)
                {
                    //var newdata = data.Select(e => new { e.Id, e.Name });
                    var newdata = data.Select(e => new { e.Id, e.ModelName, e.IsShow });

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询板块成功！"
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
        /// 新增Models
        /// </summary>
        /// <param name="models">Models信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult AddModels([FromBody]Models models)
        {
            var isresult = false;
            var message = "添加失败";
            try
            {
                models.Id = Guid.NewGuid();
                ModelsService brandService = new ModelsService(Services);
                var result = brandService.AddModels(models);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "添加板块成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message, id = models.Id });
        }

        /// <summary>
        /// 修改Models信息
        /// </summary>
        /// <param name="models">Models信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateModels([FromBody]Models models)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                ModelsService brandService = new ModelsService(Services);
                var result = brandService.ModifyModels(models);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改板块成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 删除Models信息
        /// </summary>
        /// <param name="id">Models id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult DeleteModels(Guid id)
        {
            var isresult = false;
            var message = "删除失败！";
            try
            {
                ModelsService brandService = new ModelsService(Services);

                var brand = brandService.GetModelsById(id);
                if (brand != null)
                {
                    var result = brandService.DelModels(id);
                    isresult = result.State == OperationState.Success;
                    message = result.Message;

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "删除板块成功！"
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
        /// 根据id查询Models详细信息
        /// </summary>
        /// <param name="id">Models id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetModels(Guid id)
        {
            try
            {
                ModelsService brandService = new ModelsService(Services);
                var data = brandService.GetModelsById(id);

                return Json(data);
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}