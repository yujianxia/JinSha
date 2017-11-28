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
                            case "img":
                                if (postFile.FileName != null)
                                {
                                    filepath = filepath.Substring(0, filepath.Length - 7);
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
