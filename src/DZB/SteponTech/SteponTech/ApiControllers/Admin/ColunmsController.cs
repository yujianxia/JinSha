using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Stepon.EntityFrameworkCore;
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
    /// ColunmsAPI
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class ColunmsController : BaseController<ColunmsController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 获取所有Colunms
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllColunms()
        {
            try
            {
                //var name = User.Identity.Name;
                ColunmsService userService = new ColunmsService(Services);
                var dataall = userService.GeyColunms_All();
                var data = new System.Collections.Queue();
                if (dataall?.Count > 0)
                {
                    //var newdata = data.Select(e => new { e.Id, e.Name });
                    //var newdata = data.Select(e => new { e.Name,e.colunmsname });

                    var firstLayer = dataall.Where(e => e.ColunmsId == new Guid("00000000-0000-0000-0000-000000000000")).ToList();
                    firstLayer = firstLayer.OrderBy(x => x.Sorting).ToList();
                    foreach (var permission in firstLayer)
                    {
                        var markInfo = permission.Id; //第一层权限的标识
                        var parentQueue = new System.Collections.Queue();
                        var nextLayer = dataall.Where(e => e.ColunmsId == markInfo).ToList();
                        nextLayer = nextLayer.OrderBy(x => x.Sorting).ToList();
                        foreach (var nextPer in nextLayer)
                        {
                            var childrenQueue = new System.Collections.Queue();
                            RecursiveFind(childrenQueue, dataall, nextPer.Id, 3);
                            if (childrenQueue.Count > 0)
                            {
                                parentQueue.Enqueue(new { value = nextPer.Id, label = nextPer.Name, children = childrenQueue, module = nextPer.ModelName, level = 2, describe = nextPer.ColDescribe });
                            }
                            else
                            {
                                parentQueue.Enqueue(new { value = nextPer.Id, label = nextPer.Name, module = nextPer.ModelName, level = 2, describe = nextPer.ColDescribe });
                            }
                        }
                        data.Enqueue(new { value = permission.Id, label = permission.Name, children = parentQueue, module = permission.ModelName, level = 1, describe = permission.ColDescribe });
                    }




                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询栏目列表成功！"
                    });

                    return Json(new { code = 1, message = "数据获取成功！", data = data });
                }
                return Json(new { code = 4, message = "数据获取失败！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 5, message = "数据获取失败！", data = e.Message });
            }
        }
        /// <summary>
        /// 根据ModelId获取Colums
        /// </summary>
        /// <param name="ModelId">ModelId</param>
        /// <returns></returns>
        [HttpGet("[action]/{ModelId}")]
        public JsonResult GetModelIdColunms(Guid ModelId)
        {
            try
            {
                //var name = User.Identity.Name;
                ColunmsService userService = new ColunmsService(Services);
                var dataall = userService.GeyColunms_All().Where(x => x.ModelId == ModelId).ToList();
                var data = new System.Collections.Queue();
                if (dataall?.Count > 0)
                {
                    //var newdata = data.Select(e => new { e.Id, e.Name });
                    //var newdata = data.Select(e => new { e.Name,e.colunmsname });

                    var firstLayer = dataall.Where(e => e.ColunmsId == new Guid("00000000-0000-0000-0000-000000000000")).ToList();
                    firstLayer = firstLayer.OrderBy(x => x.Sorting).ToList();
                    foreach (var permission in firstLayer)
                    {
                        var markInfo = permission.Id; //第一层权限的标识
                        var parentQueue = new System.Collections.Queue();
                        var nextLayer = dataall.Where(e => e.ColunmsId == markInfo).ToList();
                        nextLayer = nextLayer.OrderBy(x => x.Sorting).ToList();
                        foreach (var nextPer in nextLayer)
                        {
                            var childrenQueue = new System.Collections.Queue();
                            RecursiveFind(childrenQueue, dataall, nextPer.Id, 3);
                            if (childrenQueue.Count > 0)
                            {
                                parentQueue.Enqueue(new { value = nextPer.Id, label = nextPer.Name, children = childrenQueue, module = nextPer.ModelName, level = 2 });
                            }
                            else
                            {
                                parentQueue.Enqueue(new { value = nextPer.Id, label = nextPer.Name, module = nextPer.ModelName, level = 2 });
                            }
                        }
                        data.Enqueue(new { value = permission.Id, label = permission.Name, children = parentQueue, module = permission.ModelName, level = 1 });
                    }




                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询栏目列表成功！"
                    });

                    return Json(new { code = 1, message = "数据获取成功！", data = data });
                }
                return Json(new { code = 4, message = "数据获取失败！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 5, message = "数据获取失败！", data = e.Message });
            }
        }

        #region 递归
        /// <summary>
        /// 递归查找权限
        /// </summary>
        /// <param name="childrenQueue">权限树</param>
        /// <param name="allPermissions">所有权限</param>
        /// <param name="parentMark">父级权限标识</param>
        /// <param name="level">第几级</param>
        /// <returns></returns>
        private void RecursiveFind(System.Collections.Queue childrenQueue, System.Collections.Generic.List<ColunmsView> allPermissions, Guid parentMark, int level)
        {
            var nextLayer = allPermissions.Where(e => e.ColunmsId == parentMark).ToList();
            nextLayer = nextLayer.OrderBy(x => x.Sorting).ToList();
            if (nextLayer?.Count > 0)
            {
                foreach (var nextPer in nextLayer)
                {
                    var nextQueue = new System.Collections.Queue();
                    RecursiveFind(nextQueue, allPermissions, nextPer.Id, level + 1);
                    if (nextQueue.Count > 0)
                    {
                        childrenQueue.Enqueue(new { value = nextPer.Id, label = nextPer.Name, children = nextQueue, module = nextPer.ModelName, level = level, describe = nextPer.ColDescribe });
                    }
                    else
                    {
                        childrenQueue.Enqueue(new { value = nextPer.Id, label = nextPer.Name, module = nextPer.ModelName, level = level, describe = nextPer.ColDescribe });
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// 新增Colunms
        /// </summary>
        /// <param name="Colunms">Colunms信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult AddColunms([FromBody]Colunms Colunms)
        {
            var isresult = false;
            var message = "添加失败";
            try
            {
                Colunms.Id = Guid.NewGuid();
                Colunms.IsNew = true;
                ColunmsService brandService = new ColunmsService(Services);
                if (Colunms.ColunmsId == null)
                {
                    Colunms.ColunmsId = new Guid("00000000-0000-0000-0000-000000000000");
                }
                if (Colunms.ColunmsId == new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    int count = brandService.GeyColunms_All().Where(x => x.ModelId == Colunms.ModelId).Count();
                    Colunms.Sorting = count + 1;
                }
                else
                {
                    int count = brandService.GeyColunms_All().Where(x => x.ColunmsId == Colunms.ColunmsId).Count();
                    Colunms.Sorting = count + 1;
                }
                var result = brandService.AddColunms(Colunms);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "添加栏目成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message, id = Colunms.Id });
        }

        /// <summary>
        /// 修改Colunms信息
        /// </summary>
        /// <param name="Colunms">Colunms信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateColunms([FromBody]Colunms Colunms)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                ColunmsService brandService = new ColunmsService(Services);
                var result = brandService.ModifyColunms(Colunms);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改栏目成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 删除Colunms信息
        /// </summary>
        /// <param name="id">Colunms id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult DeleteColunms(Guid id)
        {
            var isresult = false;
            var message = "删除失败！";
            try
            {
                ColunmsService brandService = new ColunmsService(Services);

                var brand = brandService.GetColunmsById(id);

                if (brand != null)
                {
                    var result = brandService.DelColunms(id);
                    isresult = result.State == OperationState.Success;
                    message = result.Message;

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "删除栏目成功！"
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
        /// 改变排序
        /// </summary>
        /// <param name="id">Colunms id</param>
        /// <param name="sort">sort</param>
        /// <returns></returns>
        [HttpPut("{id}/{sort}")]
        public JsonResult UpdateSort(Guid id, int sort)
        {
            try
            {
                ColunmsService brandService = new ColunmsService(Services);
                var colunm = brandService.GetColunmsById(id);
                int oldcount = colunm.Sorting;
                if (brandService.ModifySort(colunm.ColunmsId, oldcount, sort).State == OperationState.Success)
                {
                    colunm.Sorting = sort;
                    var result = Context.Update(colunm, true) > 0;
                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "修改栏目序列成功！"
                    });
                    return Json(new { result = true, message="操作成功" });
                }
                return Json(new { result = false, message = "操作失败" });
            }
            catch (Exception e)
            {
                return Json(new { result = false, message=e.Message });

            }

        }

        /// <summary>
        /// 根据板块和名称查询Colunms详细信息
        /// </summary>
        /// <param name="SearchColunms">查询栏目实体</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult GetSearchColunms([FromBody]SearchColunms SearchColunms)
        {
            try
            {
                //var name = User.Identity.Name;
                ColunmsService userService = new ColunmsService(Services);
                var dataall = userService.GeyColunms_All();
                if (SearchColunms.ModelId != new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    dataall = dataall.Where(x => x.ModelId == SearchColunms.ModelId).ToList();
                }
                if (SearchColunms.ColName != null)
                {
                    dataall = dataall.Where(x => x.Name.Contains(SearchColunms.ColName)).ToList();
                }
                var data = new System.Collections.Queue();
                if (dataall?.Count > 0)
                {
                    //var newdata = data.Select(e => new { e.Id, e.Name });
                    //var newdata = data.Select(e => new { e.Name,e.colunmsname });

                    var firstLayer = dataall.Where(e => e.ColunmsId == new Guid("00000000-0000-0000-0000-000000000000")).ToList();
                    firstLayer = firstLayer.OrderBy(x => x.Sorting).ToList();
                    foreach (var permission in firstLayer)
                    {
                        var markInfo = permission.Id; //第一层权限的标识
                        var parentQueue = new System.Collections.Queue();
                        var nextLayer = dataall.Where(e => e.ColunmsId == markInfo).ToList();
                        nextLayer = nextLayer.OrderBy(x => x.Sorting).ToList();
                        foreach (var nextPer in nextLayer)
                        {
                            var childrenQueue = new System.Collections.Queue();
                            RecursiveFind(childrenQueue, dataall, nextPer.Id, 3);
                            if (childrenQueue.Count > 0)
                            {
                                parentQueue.Enqueue(new { value = nextPer.Id, label = nextPer.Name, children = childrenQueue, module = nextPer.ModelName, level = 2, describe = nextPer.ColDescribe });
                            }
                            else
                            {
                                parentQueue.Enqueue(new { value = nextPer.Id, label = nextPer.Name, module = nextPer.ModelName, level = 2, describe = nextPer.ColDescribe });
                            }
                        }
                        data.Enqueue(new { value = permission.Id, label = permission.Name, children = parentQueue, module = permission.ModelName, level = 1, describe = permission.ColDescribe });
                    }




                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询栏目列表成功！"
                    });

                    return Json(new { code = 1, message = "数据获取成功！", data = data });
                }
                return Json(new { code = 4, message = "数据获取失败！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 5, message = "数据获取失败！", data = e.Message });
            }

        }

        /// <summary>
        /// 根据id查询Colunms详细信息
        /// </summary>
        /// <param name="id">Colunms id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetColunms(Guid id)
        {
            try
            {
                ColunmsService brandService = new ColunmsService(Services);
                var data = brandService.GetColunmsById(id);
                //返回上级ID
                System.Collections.Generic.List<Guid> toplevel = new System.Collections.Generic.List<Guid>();
                GetColunmsTopLevel(brandService, data.ColunmsId, ref toplevel);

                return Json(new { data, toplevel });
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 根据id查询Colunms上级信息
        /// </summary>
        /// <param name="brandService">数据库实体</param>
        /// <param name="id">id</param>
        /// <param name="toplevel">级数</param>
        /// <returns></returns>
        private void GetColunmsTopLevel(ColunmsService brandService, Guid id, ref System.Collections.Generic.List<Guid> toplevel)
        {
            if (id != new Guid("00000000-0000-0000-0000-000000000000"))
            {
                toplevel.Add(id);
                var data = brandService.GetColunmsById(id);
                GetColunmsTopLevel(brandService, data.ColunmsId, ref toplevel);
            }
        }
    }
}
