
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwResource/?ResourceID=397860

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npoi.Core.HSSF.UserModel;
using Npoi.Core.SS.UserModel;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Services.BaseServices;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// 资源API
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class ResourceController : BaseController<ResourceController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public ResourceController(IHostingEnvironment env, IConfiguration config)
        {
            Environment = env;
            _configuration = config;
        }
        /// <summary>
        /// 获取所有Resource
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllResource()
        {
            try
            {
                //var name = User.Identity.Name;
                ResourceService userService = new ResourceService(Services);
                var data = userService.GeyResource_All();
                if (data?.Count > 0)
                {
                    var datatop = data.Where(x => x.IsUp == true).ToList();
                    var datanotop = data.Where(x => x.IsUp == false).ToList();
                    datatop.AddRange(datanotop);



                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询资源列表成功！"
                    });

                    return Json(new { code = 1, message = "数据获取成功！", data = datatop });
                }
                return Json(new { code = 4, message = "数据获取失败！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 4, message = "数据获取失败！", data = e.Message });
            }
        }

        /// <summary>
        /// 新增Resource
        /// </summary>
        /// <param name="Resource">Resource信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<dynamic> AddResource([FromBody]Resource Resource)
        {
            var isresult = false;
            var message = "添加失败";
            try
            {
                Resource.Id = Guid.NewGuid();
                var loginusername = User.Identity.Name;

                Resource.UploadMan = loginusername;
                Resource.State = "未审核";

                ResourceService brandService = new ResourceService(Services);
                var result = brandService.AddResource(Resource);
                isresult = result.State == OperationState.Success;
                message = result.Message;


                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "添加资源成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { result = isresult, message, id = Resource.Id });
        }

        /// <summary>
        /// 修改Resource信息
        /// </summary>
        /// <param name="Resource">Resource信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateResource([FromBody]Resource Resource)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                ResourceService brandService = new ResourceService(Services);
                var result = brandService.ModifyResource(Resource);
                isresult = result.State == OperationState.Success;
                message = result.Message;

                var loginusername = User.Identity.Name;
                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改资源成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 修改Resource状态
        /// </summary>
        /// <param name="id">Resource id</param>
        /// <param name="state">Resource state</param>
        /// <returns></returns>
        [HttpPost("[action]/{id}/{state}")]
        public JsonResult UpdateResourceState(Guid id, string state)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                var loginusername = User.Identity.Name;
                ResourceService brandService = new ResourceService(Services);
                var Resource = brandService.GetResourceById(id);
                Resource.CheckMan = loginusername;
                Resource.State = state;
                var result = brandService.ModifyResource(Resource);
                isresult = result.State == OperationState.Success;
                message = result.Message;


                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改资源状态成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 修改Resource置顶状态
        /// </summary>
        /// <param name="id">Resource id</param>
        /// <param name="istop">Resource istop</param>
        /// <returns></returns>
        [HttpPost("[action]/{id}/{istop}")]
        public JsonResult UpdateResourceIstop(Guid id, Boolean istop)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                var loginusername = User.Identity.Name;
                ResourceService brandService = new ResourceService(Services);
                var Resource = brandService.GetResourceById(id);

                Resource.IsUp = istop;

                var result = brandService.ModifyResource(Resource);
                isresult = result.State == OperationState.Success;
                message = result.Message;


                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                SysLog.AddLog(new Data.CommonModel.SystemLog
                {
                    UserName = loginusername,
                    OpreationMode = "用户：" + loginusername + "修改资源置顶状态成功！"
                });
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 删除Resource信息
        /// </summary>
        /// <param name="id">Resource id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public JsonResult DeleteResource(Guid id)
        {
            var isresult = false;
            var message = "删除失败！";
            try
            {
                ResourceService brandService = new ResourceService(Services);

                var brand = brandService.GetResourceById(id);
                if (brand != null)
                {
                    var result = brandService.DelResource(id);
                    isresult = result.State == OperationState.Success;
                    message = result.Message;

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "删除资源成功！"
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
        /// 一键备份
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetBackupResource()
        {
            try
            {
                var sourceaddress = Environment.WebRootPath;  //文件存储路径
                ResourceService brandService = new ResourceService(Services);
                var resources = brandService.GeyResource_All();
                if (resources.Count > 0)
                {
                    if (Directory.Exists(sourceaddress + "\\upload\\Backup"))
                    {
                        Directory.Delete(sourceaddress + "\\upload\\Backup", true);
                    }
                    Directory.CreateDirectory(sourceaddress + "\\upload\\Backup");
                    Directory.CreateDirectory(sourceaddress + "\\upload\\Backup\\File");


                    CopyDirectory(sourceaddress + "\\upload\\Resource", sourceaddress + "\\upload\\Backup\\File");


                    Write(resources, sourceaddress);

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "备份资源成功！"
                    });
                    return Json(new { result = true, message = "备份成功" });
                }
                else
                {
                    return Json(new { result = false, message = "暂Nothing数据" });
                }
            }
            catch (Exception e)
            {
                return Json(new { result = false, message = "备份失败" });
            }
        }

        /// <summary>
        /// 一键还原
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetRestoreResource()
        {
            try
            {
                var sourceaddress = Environment.WebRootPath;  //文件存储路径
                string[] files = Directory.GetFiles(sourceaddress + "\\upload\\Backup\\", "*.txt");
                if (files.Length > 0)
                {
                    ResourceService brandService = new ResourceService(Services);
                    //清空源文件夹与数据库
                    if (System.IO.File.Exists(sourceaddress + "\\upload\\Resource"))
                    {
                        Directory.Delete(sourceaddress + "\\upload\\Resource", true);
                    }
                    Directory.CreateDirectory(sourceaddress + "\\upload\\Resource");
                    CopyDirectory(sourceaddress + "\\upload\\Backup\\File", sourceaddress + "\\upload\\Resource");
                    brandService.ClearResource();
                    using (System.IO.StreamReader sr = System.IO.File.OpenText(files[0]))
                    {
                        string str;
                        while ((str = sr.ReadLine()) != null)
                        {
                            brandService.ImportResource(str);
                        }
                    }
                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "还原资源成功！"
                    });
                    return Json(new { result = true, message = "还原成功" });
                }
                else
                {
                    return Json(new { result = false, message = "未找到备份数据" });
                }

            }
            catch (Exception e)
            {
                return Json(new { result = false, message = "还原失败" });
            }

        }

        private void Write(System.Collections.Generic.List<Resource> Resources, string sourceaddress)
        {
            try
            {
                FileStream fs1 = new FileStream(sourceaddress + "\\upload\\Backup\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                string result = string.Empty;
                foreach (var item in Resources)
                {
                    result += "INSERT INTO \"Resource\" VALUES ('" + item.Id + "', '" + item.CreationDate + "', '" + item.LastUpdate + "', '" + item.UploadMan + "', '" + item.CheckMan + "', '" + item.State + "', '" + item.Title + "', '" + item.Content + "', '" + item.Flie + "', " + item.IsUp + ", '" + item.Mark + "');\r\n";
                }
                sw.WriteLine(result.Substring(0, result.Length - 2));//开始写入值
                sw.Dispose();
                fs1.Dispose();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 备份方法
        /// </summary>、
        /// <param name="srcPath">源地址</param>
        /// <param name="destPath">备份地址</param>
        /// <returns></returns>
        private void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        System.IO.File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据id查询Resource详细信息
        /// </summary>
        /// <param name="id">Resource id</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public JsonResult GetResource(Guid id)
        {
            try
            {
                ResourceService brandService = new ResourceService(Services);
                var data = brandService.GetResourceById(id);

                System.Collections.Generic.List<FileList> filelist = new System.Collections.Generic.List<FileList>();
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Resource\\" + data.Id);
                if (Directory.Exists(filepath))
                {
                    
                    DirectoryInfo theFolder = new DirectoryInfo(filepath);
                    FileInfo[] Files = theFolder.GetFiles();
                    foreach (FileInfo NextFolder in Files)
                    {
                        FileList iil = new FileList
                        {
                            FileName = NextFolder.Name,
                            FileUrl = "/upload/Resource/" + data.Id + "/" + NextFolder.Name
                        };
                        filelist.Add(iil);
                    }

                }
                return Json(new
                {
                    data,
                    filelist
                });
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public string DownloadModelFile()
        {
            try
            {
                var filepath = System.IO.Path.Combine("/upload/Resource", "资源模板.xlsx");

                return filepath;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public string ExportResource(string id)
        {
            try
            {
                var loginusername = User.Identity.Name;
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Resource");
                var filepath1 = Path.Combine(filepath + "\\导出文件", loginusername);
                ResourceService brandService = new ResourceService(Services);
                if (!string.IsNullOrEmpty(id))
                {
                    var idlist = id.Split(',').ToList();
                    if (idlist?.Count > 0)
                    {

                        //判断是否有 导出文件 目录
                        if (Directory.Exists(filepath1))
                        {
                            System.IO.Directory.Delete(filepath1, true);
                        }
                        Directory.CreateDirectory(filepath1);

                        //HSSF可以读取xls格式的Excel文件
                        IWorkbook workbook = new HSSFWorkbook();
                        //XSSF可以读取xlsx格式的Excel文件
                        //IWorkbook workbook = new XSSFWorkbook();

                        //Excel文件至少要有一个工作表sheet
                        ISheet sheet = workbook.CreateSheet("工作表");
                        IRow row0 = sheet.CreateRow(0);
                        ICell cell0 = row0.CreateCell(0);
                        cell0.SetCellValue("标题");
                        ICell cell1 = row0.CreateCell(1);
                        cell1.SetCellValue("内容");
                        ICell cell2 = row0.CreateCell(2);
                        cell2.SetCellValue("是否置顶");
                        ICell cell3 = row0.CreateCell(3);
                        cell3.SetCellValue("备注");

                        for (int i = 0; i < idlist.Count; i++)
                        {
                            var data = brandService.GetResourceById(Guid.Parse(idlist[i]));
                            IRow row = sheet.CreateRow(i + 1); //i表示了创建行的索引，从0开始
                                                               //创建单元格
                            for (int j = 0; j < 4; j++)
                            {
                                ICell cell = row.CreateCell(j);  //同时这个函数还有第二个重载，可以指定单元格存放数据的类型
                                if (j == 0)
                                {
                                    cell.SetCellValue(data.Title);
                                }
                                else if (j == 1)
                                {
                                    cell.SetCellValue(data.Content);
                                }
                                else if (j == 2)
                                {
                                    if (data.IsUp == true)
                                    {
                                        cell.SetCellValue("是");
                                    }
                                    else
                                    {
                                        cell.SetCellValue("否");
                                    }

                                }
                                else
                                {
                                    cell.SetCellValue(data.Mark);
                                }
                            }


                            //判断该资源是否有文件存在 如果有则复制到导出文件目录
                            if (Directory.Exists(Path.Combine(filepath, data.Id.ToString())))
                            {
                                Directory.CreateDirectory(Path.Combine(filepath1, data.Title));

                                string[] files = Directory.GetFiles(Path.Combine(filepath, data.Id.ToString()));
                                foreach (string formFileName in files)
                                {
                                    string fileName = Path.GetFileName(formFileName);
                                    string toFileName = Path.Combine((Path.Combine(filepath1, data.Title)), fileName);
                                    System.IO.File.Copy(formFileName, toFileName);
                                }

                            }

                        }
                        //表格制作完成后，保存
                        //创建一个文件流对象
                        string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                        var excelpath = Path.Combine(filepath1, filename);
                        using (FileStream fs = new FileStream(excelpath, FileMode.OpenOrCreate))
                        {
                            workbook.Write(fs);
                            //关闭对象
                            workbook.Close();
                        }
                        if (System.IO.File.Exists(Path.Combine(filepath + "\\导出文件", loginusername + ".zip")))
                        {
                            System.IO.File.Delete(Path.Combine(filepath + "\\导出文件", loginusername + ".zip"));
                        }
                        System.IO.Compression.ZipFile.CreateFromDirectory(filepath1, Path.Combine(filepath + "\\导出文件", loginusername + ".zip"));
                        Directory.Delete(filepath1, true);
                    }
                }
                var urlpath = "/upload/Resource/导出文件/" + loginusername + ".zip";
                return urlpath;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 根据id,Resource
        /// </summary>
        /// <param name="id">Resource id</param>
        /// <param name="filename">删除的文件名字</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}/{filename}")]
        public JsonResult DeleteResourceFile(Guid id, string filename)
        {
            var isresult = false;
            var message = "未找到文件！";
            try
            {
                InformationService brandService = new InformationService(Services);
                var filepath = Environment.WebRootPath;  //文件存储路径
                filepath = Path.Combine(filepath, "upload\\Resource\\" + id);
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
    }
}
