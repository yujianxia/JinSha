using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Data.CommonModels;
using SteponTech.Services.BaseServices;
using SteponTech.Services.CommonService;
using System;
using System.Collections.Generic;
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
    public class PermissionController : BaseController<PermissionController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public dynamic GetAllPermission()
        {
            var data = new System.Collections.Queue();
            int code;
            string message;
            try
            {
                var permissionService = new PermissionService(Services);
                var allPermission = permissionService.GetAllPermissions();
                if (allPermission?.Count > 0)
                {
                    var firstLayer = allPermission.Where(e => string.IsNullOrEmpty(e.ParentMark)).ToList();
                    foreach (var permission in firstLayer)
                    {
                        var markInfo = permission.Mark; //第一层权限的标识
                        var parentQueue = new System.Collections.Queue();
                        var nextLayer = allPermission.Where(e => e.ParentMark == markInfo).ToList();
                        foreach (var nextPer in nextLayer)
                        {
                            var childrenQueue = new System.Collections.Queue();
                            RecursiveFind(childrenQueue, allPermission, nextPer.Mark);
                            if (childrenQueue.Count > 0)
                            {
                                parentQueue.Enqueue(new { id = nextPer.Id, label = nextPer.Name, mark = nextPer.Mark, children = childrenQueue });
                            }
                            else
                            {
                                parentQueue.Enqueue(new { id = nextPer.Id, label = nextPer.Name, mark = nextPer.Mark });
                            }
                        }
                        data.Enqueue(new { id = permission.Id, label = permission.Name, mark = permission.Mark, children = parentQueue });
                    }
                    code = 1;
                    message = "获取数据成功！";
                }
                else
                {
                    code = 2;
                    message = "未找到数据！";
                };
            }
            catch (Exception exception)
            {
                code = 4;
                message = "获取数据失败,请稍后重试！";
            }
            return new { code, message, data };
        }

        /// <summary>
        /// 获取所有Mapping
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllMapping()
        {
            try
            {
                //var name = User.Identity.Name;
                MappingService userService = new MappingService(Services);
                var data = userService.GeyMapping_All();
                if (data?.Count > 0)
                {
                    //var newdata = data.Select(e => new { e.Id, e.Name });
                    var newdata = data.Select(e => new { e.Id, e.Role });

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询权限映射列表成功！"
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
        /// 新增Mapping
        /// </summary>
        /// <param name="Mapping">Mapping信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult AddMapping([FromBody]Mapping Mapping)
        {
            var isresult = false;
            var message = "添加失败";
            try
            {
                Mapping.Id = Guid.NewGuid().ToString();
                MappingService brandService = new MappingService(Services);
                var result = brandService.AddMapping(Mapping);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "添加权限成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message, id = Mapping.Id });
        }

        /// <summary>
        /// 修改Mapping信息
        /// </summary>
        /// <param name="Mapping">Mapping信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateMapping([FromBody]Mapping Mapping)
        {
             var isresult = false;
            var message = "修改失败！";
            try
            {
                MappingService brandService = new MappingService(Services);
                var result = brandService.ModifyMapping(Mapping);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改权限映射表成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 删除Mapping信息
        /// </summary>
        /// <param name="id">Mapping id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public JsonResult DeleteMapping(string id)
        {
            var isresult = false;
            var message = "删除失败！";
            try
            {
                MappingService brandService = new MappingService(Services);

                var brand = brandService.GetMappingById(id);
                if (brand != null)
                {
                    var result = brandService.DelMapping(id);
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
        /// 根据Role查询User详细信息
        /// </summary>
        /// <param name="Role">Mapping Role</param>
        /// <returns></returns>
        [HttpGet("[action]/{Role}")]
        public JsonResult GetMapping(string Role)
        {
            try
            {
                MappingService brandService = new MappingService(Services);
                var data = brandService.GetUserByMark(Role);

                return Json(data);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 根据Userid查询Mapping详细信息
        /// </summary>
        /// <param name="id">Userid</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public JsonResult GetPermissionByUserid(string id)
        {
            int code = 2;
            string massage = "获取数据失败";
            try
            {

                MappingService brandService = new MappingService(Services);
                var data = brandService.GetUserByUserid(id);
                code = 1;
                massage = "获取数据成功";
                return Json(new { code, massage, data });
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 根据Role查询Mapping详细信息
        /// </summary>
        /// <param name="Role">权限名称</param>
        /// <returns></returns>
        [HttpGet("[action]/{Role}")]
        public JsonResult GetPermissionByRole(string Role)
        {
            try
            {

                var data = new System.Collections.Queue();
                MappingService brandService = new MappingService(Services);
                var datauserid = brandService.GetUserByRole(Role);
                if (datauserid?.Count > 0)
                {
                    var firstLayer = datauserid.Where(e => string.IsNullOrEmpty(e.ParentMark)).ToList();
                    foreach (var permission in firstLayer)
                    {
                        var markInfo = permission.Mark; //第一层权限的标识
                        var parentQueue = new System.Collections.Queue();
                        var nextLayer = datauserid.Where(e => e.ParentMark == markInfo).ToList();
                        foreach (var nextPer in nextLayer)
                        {
                            var childrenQueue = new System.Collections.Queue();
                            RecursiveFind(childrenQueue, datauserid, nextPer.Mark);
                            if (childrenQueue.Count > 0)
                            {
                                parentQueue.Enqueue(new { id = nextPer.Id, label = nextPer.Name, mark = nextPer.Mark, children = childrenQueue });
                            }
                            else
                            {
                                parentQueue.Enqueue(new { id = nextPer.Id, label = nextPer.Name, mark = nextPer.Mark });
                            }
                        }
                        data.Enqueue(new { id = permission.Id, label = permission.Name, mark = permission.Mark, Permission = parentQueue });
                    }
                }
                else
                {
                };


                return Json(data);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        #region 递归
        /// <summary>
        /// 递归查找权限
        /// </summary>
        /// <param name="childrenQueue">权限树</param>
        /// <param name="allPermissions">所有权限</param>
        /// <param name="parentMark">父级权限标识</param>
        /// <returns></returns>
        private void RecursiveFind(System.Collections.Queue childrenQueue, List<Permission> allPermissions, string parentMark)
        {
            var nextLayer = allPermissions.Where(e => e.ParentMark == parentMark).ToList();
            if (nextLayer?.Count > 0)
            {
                foreach (var nextPer in nextLayer)
                {
                    var nextQueue = new System.Collections.Queue();
                    RecursiveFind(nextQueue, allPermissions, nextPer.Mark);
                    if (nextQueue.Count > 0)
                    {
                        childrenQueue.Enqueue(new { id = nextPer.Id, label = nextPer.Name, mark = nextPer.Mark, children = nextQueue });
                    }
                    else
                    {
                        childrenQueue.Enqueue(new { id = nextPer.Id, label = nextPer.Name, mark = nextPer.Mark });
                    }
                }
            }
        }
        #endregion
    }
}
