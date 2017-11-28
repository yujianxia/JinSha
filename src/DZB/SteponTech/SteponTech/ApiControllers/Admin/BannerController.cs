using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    /// BannerAPI
    /// </summary>
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class BannerController : BaseController<BannerController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private IHostingEnvironment Environment { get; }
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        public BannerController(IConfiguration config, IHostingEnvironment env)
        {
            _configuration = config;
            Environment = env;
        }
        /// <summary>
        /// 获取所有Banner
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllBanner()
        {
            try
            {
                BannerService userService = new BannerService(Services);
                var data = userService.GeyBanner_All();
                if (data?.Count > 0)
                {
                    var newdata = data.Select(e => new { e.Id, e.Name });

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询Banner成功！"
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
        /// 新增Banner
        /// </summary>
        /// <param name="banner">Banner信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult AddBanner([FromBody]Banner banner)
        {
            var isresult = false;
            var message = "添加失败";
            try
            {
                banner.Id = Guid.NewGuid();
                BannerService brandService = new BannerService(Services);
                var result = brandService.AddBrand(banner);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "新增Banner成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message, id = banner.Id });
        }

        /// <summary>
        /// 文件名称删除Banner文件
        /// </summary>
        /// <param name="filename">删除的文件名字</param>
        /// <returns></returns>
        [HttpDelete("[action]/{filename}")]
        public JsonResult DeleteBannerFile(string filename)
        {
            var isresult = false;
            var message = "未找到文件！";
            try
            {
                BannerService brandService = new BannerService(Services);
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Banner\\" + filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                    isresult = true;
                    message = "成功删除!";
                }

            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 修改Banner信息
        /// </summary>
        /// <param name="banner">Banner信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateBanner([FromBody]Banner banner)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {

                BannerService brandService = new BannerService(Services);
                //取出旧图片
                string oldfilename = brandService.GetBrandById(banner.Id).FileName;
                //如果有改动
                if (oldfilename != banner.FileName)
                {
                    //旧图片是否存在
                    if (System.IO.File.Exists(Path.Combine(Environment.WebRootPath, "upload", "banner", oldfilename)))
                    {
                        //删除
                        System.IO.File.Delete(Path.Combine(Environment.WebRootPath, "upload", "banner", oldfilename));
                    }

                }



                var result = brandService.ModifyBrand(banner);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改Banner成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 删除Banner信息
        /// </summary>
        /// <param name="id">Banner id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public JsonResult DeleteBanner(Guid id)
        {
            var isresult = false;
            var message = "删除失败！";
            try
            {
                BannerService brandService = new BannerService(Services);

                var brand = brandService.GetBrandById(id);
                if (brand != null)
                {
                    //删除对应的图片
                    string oldfilename = brand.FileName;
                    //旧图片是否存在
                    if (System.IO.File.Exists(Path.Combine(Environment.WebRootPath, "upload", "banner", oldfilename)))
                    {
                        //删除
                        System.IO.File.Delete(Path.Combine(Environment.WebRootPath, "upload", "banner", oldfilename));
                    }

                    var result = brandService.DelBrand(id);
                    isresult = result.State == OperationState.Success;
                    message = result.Message;

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "删除Banner成功！"
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
        /// 根据id查询Banner详细信息
        /// </summary>
        /// <param name="id">Banner id</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public JsonResult GetBanner(Guid id)
        {
            try
            {
                BannerService brandService = new BannerService(Services);
                var data = brandService.GetBrandById(id);
                System.Collections.Generic.List<FileList> filelist = new System.Collections.Generic.List<FileList>();
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Banner\\" + data.Id);
                if (Directory.Exists(filepath))
                {

                    var weburl = _configuration.GetSection("Message:WebUrl").Value;

                    DirectoryInfo theFolder = new DirectoryInfo(filepath);
                    FileInfo[] Files = theFolder.GetFiles();
                    foreach (FileInfo NextFolder in Files)
                    {
                        FileList iil = new FileList
                        {
                            FileName = NextFolder.Name,
                            FileUrl = weburl + "/upload/Banner/" + data.Id + "/" + NextFolder.Name
                        };
                        filelist.Add(iil);
                    }

                }
                return Json(data);
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}