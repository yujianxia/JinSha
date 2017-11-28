using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Services.BaseServices;
using System;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?InformationID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// InformationAPI
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class InformationController : BaseController<InformationController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private IHostingEnvironment Environment { get; }
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        public InformationController(IConfiguration config, IHostingEnvironment env)
        {
            _configuration = config;
            Environment = env;
        }
        /// <summary>
        /// 获取所有Information
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllInformation()
        {
            try
            {
                //var name = User.Identity.Name;
                InformationService userService = new InformationService(Services);
                var data = userService.GeyInformation_All();
                if (data?.Count > 0)
                {
                    //var newdata = data.Select(e => new { e.Id, e.Name });
                    var newdata = data.Select(e => new { e.Title, e.Name });

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询链接列表成功！"
                    });

                    return Json(new { code = 1, message = "数据获取成功！", data = newdata });
                }
                return Json(new { code = 2, message = "暂未有数据！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 4, message = "数据获取失败！", data = e.Message });
            }
        }

        /// <summary>
        /// 新增Information
        /// </summary>
        /// <param name="Information">Information信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult AddInformation([FromBody]Information Information)
        {
            var isresult = false;
            var message = "添加失败";
            try
            {
                if (string.IsNullOrEmpty(Information.UrlAddress) && !Information.UrlAddress.Contains("http://"))
                {
                    Information.UrlAddress = "http://" + Information.UrlAddress;
                }
                Information.Id = Guid.NewGuid();
                if (Information.Title == "Panoramic Visual Tour")
                {
                    Information.Name = "true";
                }
                InformationService brandService = new InformationService(Services);
                Information.ReleaseMan = User.Identity.Name;
                var result = brandService.AddInformation(Information);
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
            return Json(new { result = isresult, message, id = Information.Id });
        }

        /// <summary>
        /// 修改Information信息
        /// </summary>
        /// <param name="Information">Information信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateInformation([FromBody]Information Information)
        {
             var isresult = false;
            var message = "修改失败！";
            try
            {
                if (string.IsNullOrEmpty(Information.UrlAddress) && !Information.UrlAddress.Contains("http://"))
                {
                    Information.UrlAddress = "http://" + Information.UrlAddress;
                }
                InformationService brandService = new InformationService(Services);
                Information.ReleaseMan = User.Identity.Name;
                if (Information.Title == "Panoramic Visual Tour")
                {
                    Information.Name = "true";
                }
                var result = brandService.ModifyInformation(Information);
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
        /// 修改Information置顶状态
        /// </summary>
        /// <param name="id">Resource id</param>
        /// <param name="istop">Resource istop</param>
        /// <returns></returns>
        [HttpPost("[action]/{id}/{istop}")]
        public JsonResult UpdateInformationIstop(Guid id, Boolean istop)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                var loginusername = User.Identity.Name;
                InformationService brandService = new InformationService(Services);
                var Resource = brandService.GetInformationById(id);

                Resource.IsTop = istop;

                var result = brandService.ModifyInformation(Resource);
                isresult = result.State == OperationState.Success;
                message = result.Message;


                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改资讯置顶状态成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 删除Information信息
        /// </summary>
        /// <param name="id">Information id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public JsonResult DeleteInformation(Guid id)
        {
            var isresult = false;
            var message = "删除失败！";
            try
            {
                InformationService brandService = new InformationService(Services);

                var brand = brandService.GetInformationById(id);
                if (brand != null)
                {
                    var informationidvali = Context.InformationEnglish.Where(x => x.InformationId.Contains(id.ToString())).ToList();
                    if (informationidvali.Count() != 0)
                    {
                        foreach (var item in informationidvali)
                        {
                            var filelist = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<string>>(item.InformationId);
                            filelist.Remove(id.ToString());
                            string aa = Newtonsoft.Json.JsonConvert.SerializeObject(filelist);
                            aa = aa.Replace("\\", "");
                            item.InformationId = aa;
                            brandService.ModifyInformation(item);
                        }
                    }

                    var result = brandService.DelInformation(id);
                    isresult = result.State == OperationState.Success;
                    message = result.Message;
                    //清空文件夹
                    var filepath = Environment.WebRootPath;  //文件存储路径
                    filepath = Path.Combine(filepath, "upload\\Information\\" + id);
                    if (Directory.Exists(filepath))
                    {
                        Directory.Delete(filepath, true);
                    }

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
        /// 根据id查询Information详细信息
        /// </summary>
        /// <param name="id">Information id</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public JsonResult GetInformation(Guid id)
        {
            try
            {
                InformationAllService brandService = new InformationAllService(Services);
                var data = brandService.GetInformationAllById(id);
                System.Collections.Generic.List<FileList> filelist = new System.Collections.Generic.List<FileList>();
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Information\\" + data.Id);
                if (Directory.Exists(filepath))
                {

                    DirectoryInfo theFolder = new DirectoryInfo(filepath);
                    FileInfo[] Files = theFolder.GetFiles();
                    foreach (FileInfo NextFolder in Files)
                    {
                        FileList iil = new FileList
                        {
                            FileName = NextFolder.Name,
                            FileUrl = "/upload/Information/" + data.Id + "/" + NextFolder.Name
                        };
                        filelist.Add(iil);
                    }

                }
                //返回上级ID
                ColunmsService colunmsService = new ColunmsService(Services);
                System.Collections.Generic.List<Guid> toplevel = new System.Collections.Generic.List<Guid>();
                GetColunmsTopLevel(colunmsService, data.ColumnId, ref toplevel);

                return Json(new { data, filelist, toplevel });
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据id,文件名称删除Information文件
        /// </summary>
        /// <param name="id">Information id</param>
        /// <param name="filename">删除的文件名字</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}/{filename}")]
        public JsonResult GetInformation(Guid id, string filename)
        {
            var isresult = false;
            var message = "未找到文件！";
            try
            {
                InformationService brandService = new InformationService(Services);
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Information\\" + id);
                if (Directory.Exists(filepath))
                {
                    DirectoryInfo theFolder = new DirectoryInfo(filepath);
                    FileInfo[] Files = theFolder.GetFiles();
                    foreach (FileInfo NextFolder in Files)
                    {
                        if (NextFolder.Name.Equals(filename))
                        {
                            NextFolder.Delete();
                            isresult = true;
                            message = "删除成功";
                            break;
                        }
                    }

                }

            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 查询太阳节活动列表
        /// </summary>
        /// <param name="id">Columnid</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public JsonResult GetSunInformation()
        {
            try
            {
                //var name = User.Identity.Name;
                var data = Context.InformationEnglishAll.Where(x=>x.ColumName=="Information").ToList();
                if (data?.Count > 0)
                {
                    //var newdata = data.Select(e => new { e.Id, e.Name });
                    var newdata = data.Select(e => new { e.Id, e.Title });

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询太阳节活动成功！"
                    });

                    return Json(new { code = 1, message = "数据获取成功！", data = newdata });
                }
                return Json(new { code = 2, message = "暂未有数据！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 4, message = "数据获取失败！", data = e.Message });
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
