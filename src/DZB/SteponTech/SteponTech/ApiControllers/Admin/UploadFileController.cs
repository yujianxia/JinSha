/*************************************
 * 作者：xqx
 * 时间：2017/6/26
 * 操作：建立
 * ***********************************/
using System.IO;
using System.Net;
using SteponTech.Services;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using SteponTech.Services.BaseServices;
using SteponTech.Data.BaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;
using Npoi.Core.HSSF.UserModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// 上传文件API
    /// </summary>
    [Produces("application/json")]
    [Route("api/UploadFile")]
    [Authorize]
    public class UploadFileController : BaseController<UploadFileController, SteponContext, ApplicationUser, IdentityRole>
    {
        private IHostingEnvironment Environment { get; }

        public UploadFileController(IHostingEnvironment env)
        {
            Environment = env;
        }


        #region 上传
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<dynamic> UploadFileAsync()
        {
            var result = new OperationResult();

            if (Request.Form.Files.Count > 0)
            {

                IFormFile postFile = Request.Form.Files[0];
                if (postFile != null)
                {
                    if (postFile.Length != 0)
                    {
                        var filepath = Environment.WebRootPath;  //文件存储路径
                        filepath = Path.Combine(filepath, "upload");
                        var fileName = postFile.FileName; //获取名字
                        var directory = Request.Form["Directory"]; //获取文件夹名字
                        var key = Request.Form["key"];
                        byte[] buffer = new byte[1024 * 1024 * 20];
                        switch (key)
                        {
                            case "Resource":
                                foreach (var post in Request.Form.Files)
                                {
                                    if (post.FileName != null)
                                    {
                                        if (!Directory.Exists(filepath))
                                        {
                                            Directory.CreateDirectory(filepath);
                                        }
                                        filepath = Path.Combine(filepath, key);
                                        if (!Directory.Exists(filepath))
                                        {
                                            Directory.CreateDirectory(filepath);
                                        }
                                        if (!string.IsNullOrEmpty(directory))
                                        {
                                            filepath = Path.Combine(filepath, directory);
                                            if (!Directory.Exists(filepath))
                                            {
                                                Directory.CreateDirectory(filepath);
                                            }
                                        }
                                        filepath = Path.Combine(filepath, post.FileName);
                                        //postFile.SaveAs(filepath);//保存文件
                                        var file = new FileStream(filepath, FileMode.OpenOrCreate);
                                        var stre = postFile.OpenReadStream();
                                        int read = 0;
                                        do
                                        {
                                            read = stre.Read(buffer, 0, 1024 * 1024 * 20);
                                            file.Write(buffer, 0, read);

                                        } while (read == 1024 * 1024 * 20);
                                        file.Dispose();
                                        stre.Dispose();
                                    }
                                }
                                break;
                            case "Information":
                                foreach (var post in Request.Form.Files)
                                {
                                    if (post.FileName != null)
                                    {
                                        if (!Directory.Exists(filepath))
                                        {
                                            Directory.CreateDirectory(filepath);
                                        }
                                        filepath = Path.Combine(filepath, key);
                                        if (!Directory.Exists(filepath))
                                        {
                                            Directory.CreateDirectory(filepath);
                                        }
                                        if (!string.IsNullOrEmpty(directory))
                                        {
                                            filepath = Path.Combine(filepath, directory);
                                            if (!Directory.Exists(filepath))
                                            {
                                                Directory.CreateDirectory(filepath);
                                            }
                                        }
                                        filepath = Path.Combine(filepath, post.FileName);
                                        //postFile.SaveAs(filepath);//保存文件
                                        var file = new FileStream(filepath, FileMode.OpenOrCreate);
                                        var stre = postFile.OpenReadStream();
                                        int read = 0;
                                        do
                                        {
                                            read = stre.Read(buffer, 0, 1024 * 1024 * 20);
                                            file.Write(buffer, 0, read);

                                        } while (read == 1024 * 1024 * 20);
                                        file.Dispose();
                                        stre.Dispose();
                                    }
                                }
                                break;
                            case "HeadPortrait":
                                if (postFile.FileName != null)
                                {
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                    filepath = Path.Combine(filepath, key);
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                    var filename = directory + ".jpg";
                                    filepath = Path.Combine(filepath, filename);
                                    var file = new FileStream(filepath, FileMode.OpenOrCreate);
                                    var stre = postFile.OpenReadStream();
                                    int read;
                                    do
                                    {
                                        read = stre.Read(buffer, 0, 1024 * 1024 * 20);
                                        file.Write(buffer, 0, read);

                                    } while (read == 1024 * 1024 * 20);
                                    file.Dispose();
                                    stre.Dispose();

                                }
                                break;
                            case "Foot":
                                if (postFile.FileName != null)
                                {
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                    filepath = Path.Combine(filepath, key);
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                    //if (!string.IsNullOrEmpty(directory))
                                    //{
                                    //    filepath = Path.Combine(filepath, directory);
                                    //    if (!Directory.Exists(filepath))
                                    //    {
                                    //        Directory.CreateDirectory(filepath);
                                    //    }
                                    //}
                                    var filename = directory + ".jpg";
                                    filepath = Path.Combine(filepath, filename);
                                    var file = new FileStream(filepath, FileMode.OpenOrCreate);
                                    var stre = postFile.OpenReadStream();
                                    int read;
                                    do
                                    {
                                        read = stre.Read(buffer, 0, 1024 * 1024 * 20);
                                        file.Write(buffer, 0, read);

                                    } while (read == 1024 * 1024 * 20);
                                    file.Dispose();
                                    stre.Dispose();

                                }
                                break;
                            case "Banner":
                                if (postFile.FileName != null)
                                {
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                    filepath = Path.Combine(filepath, key);
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                    filepath = Path.Combine(filepath, postFile.FileName);
                                    var file = new FileStream(filepath, FileMode.OpenOrCreate);
                                    var stre = postFile.OpenReadStream();
                                    int read;
                                    do
                                    {
                                        read = stre.Read(buffer, 0, 1024 * 1024 * 20);
                                        file.Write(buffer, 0, read);

                                    } while (read == 1024 * 1024 * 20);
                                    file.Dispose();
                                    stre.Dispose();

                                }
                                break;
                            default:
                                if (fileName != null)
                                {
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                    filepath = Path.Combine(filepath, key);
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                    if (!string.IsNullOrEmpty(directory))
                                    {
                                        filepath = Path.Combine(filepath, directory);
                                        if (!Directory.Exists(filepath))
                                        {
                                            Directory.CreateDirectory(filepath);
                                        }
                                    }
                                    //fileName = directory + ".jpg";
                                    filepath = Path.Combine(filepath, fileName);
                                    //postFile.SaveAs(filepath);//保存文件
                                    var file = new FileStream(filepath, FileMode.OpenOrCreate);
                                    var stre = postFile.OpenReadStream();
                                    int read = 0;
                                    do
                                    {
                                        read = stre.Read(buffer, 0, 1024 * 1024 * 20);
                                        file.Write(buffer, 0, read);

                                    } while (read == 1024 * 1024 * 20);
                                    file.Dispose();
                                    stre.Dispose();

                                }
                                break;

                        }
                    }
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region 导入Excel
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public ActionResult UploadFileImportExcel()
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile postFile = Request.Form.Files[0];
                    if (postFile != null)
                    {
                        if (postFile.Length != 0)
                        {
                            var filepath = Environment.WebRootPath;  //文件存储路径
                            filepath = Path.Combine(filepath, "upload");
                            var fileName = postFile.FileName; //获取名字
                            var directory = Request.Form["Directory"]; //获取文件夹名字
                            var key = Request.Form["key"];
                            byte[] buffer = new byte[1024 * 1024 * 20];
                            if (postFile.FileName != null)
                            {
                                if (!Directory.Exists(filepath))
                                {
                                    Directory.CreateDirectory(filepath);
                                }
                                filepath = Path.Combine(filepath, key);
                                if (!Directory.Exists(filepath))
                                {
                                    Directory.CreateDirectory(filepath);
                                }
                                if (!string.IsNullOrEmpty(directory))
                                {
                                    filepath = Path.Combine(filepath, directory);
                                    if (!Directory.Exists(filepath))
                                    {
                                        Directory.CreateDirectory(filepath);
                                    }
                                }
                                filepath = Path.Combine(filepath, postFile.FileName);
                                var file = new FileStream(filepath, FileMode.OpenOrCreate);
                                var stre = postFile.OpenReadStream();
                                int read;
                                do
                                {
                                    read = stre.Read(buffer, 0, 1024 * 1024 * 20);
                                    file.Write(buffer, 0, read);

                                } while (read == 1024 * 1024 * 20);
                                file.Dispose();
                                stre.Dispose();






                                IWorkbook workbook = null;  //新建IWorkbook对象 
                                FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                                if (filepath.IndexOf(".xlsx") > 0) // 2007版本  
                                {
                                    workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                                }
                                else if (filepath.IndexOf(".xls") > 0) // 2003版本  
                                {
                                    workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                                }
                                ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
                                IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
                                for (int i = 0; i < sheet.LastRowNum; i++)  //对工作表每一行  
                                {
                                    if (i != 0)
                                    {
                                        Resource r = new Resource();
                                        r.Id = Guid.NewGuid();
                                        var loginusername = User.Identity.Name;
                                        r.UploadMan = loginusername;
                                        r.State = "未审核";

                                        row = sheet.GetRow(i);   //row读入第i行数据  
                                        if (row != null)
                                        {
                                            for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
                                            {
                                                string cellValue = row.GetCell(j).ToString(); //获取i行j列数据  
                                                if (j == 0)
                                                {
                                                    r.Title = cellValue;
                                                }
                                                else if (j == 1)
                                                {
                                                    r.Content = cellValue;
                                                }
                                                else if (j == 2)
                                                {
                                                    if (cellValue == "是")
                                                    {
                                                        r.IsUp = true;
                                                    }
                                                    else
                                                    {
                                                        r.IsUp = false;
                                                    }
                                                }
                                                else
                                                {
                                                    r.Mark = cellValue;
                                                }
                                            }
                                            ResourceService brandService = new ResourceService(Services);
                                            var result = brandService.AddResource(r);
                                        }
                                    }
                                }
                                fileStream.Dispose();
                                workbook.Close();
                            }
                        }
                    }
                }

            }
            catch(Exception e)
            {
                return StatusCode(500);
            }

            return StatusCode(200);
        }
        #endregion
    }
}
