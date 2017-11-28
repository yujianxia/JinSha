/*****************************
 * 作者：xqx
 * 日期：2017年6月22日
 * 操作：创建
 * ****************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SteponTech.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stepon.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Distributed;
using SteponTech.ViewModels;
using SteponTech.Services.BaseServices;
using SteponTech.Services.CommonService;
using SteponTech.Data.CommonModel;
using SteponTech.Data.BaseModels;
using Stepon.Sender.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SteponTech.Services.BaseService;
using SteponTech.Data.BaseModel;

//http://120.25.240.32:8600
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//http://localhost:55241/swagger/ui/index.html


namespace SteponTech.ApiControllers
{
    /// <summary>
    /// 登录API
    /// </summary>
    //谷歌调试有自动记住 如果AuthorizeNothing效 应该是这个问题 换个浏览器看看
    [Produces("application/json")]
    [Route("api/LoginAuthorize")]
    public class LoginAuthorizeController : BaseController<LoginAuthorizeController, SteponContext, ApplicationUser, IdentityRole>
    {
        private readonly IDistributedCache _cache;
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="options">电子邮件选项</param>
        /// <param name="config"></param>
        /// <param name="env"></param>
        public LoginAuthorizeController(IDistributedCache cache, IOptions<EmailOptions> options, IConfiguration config, IHostingEnvironment env)
        {
            _cache = cache;
            _emailOptions = options;
            _configuration = config;
            Environment = env;
        }


        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="loginModel">登录信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<dynamic> Login([FromBody] LoginModel loginModel)
        {
            var code = 5;
            var message = "用户名或密码错误！";
            dynamic data = null;
            try
            {
                if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
                {
                    message = "用户名或密码错误！";
                }
                else
                {
                    var result = await SignInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, true);
                    if (result.Succeeded)
                    {
                        try
                        {
                            var userInfo = await UserManager.FindByNameAsync(loginModel.UserName);
                            if (userInfo.LockoutEnabled)
                            {
                                message = "用户被锁定！";
                                //清空登录实例
                                await SignInManager.SignOutAsync();
                            }
                            else
                            {
                                if (userInfo != null)
                                {
                                    var filepath = Environment.WebRootPath;
                                    filepath = System.IO.Path.Combine(filepath + "\\upload\\HeadPortrait", userInfo.Id + ".jpg");
                                    if (System.IO.File.Exists(filepath))
                                    {

                                        userInfo.HeadPortrait = "/upload/HeadPortrait/" + userInfo.Id + ".jpg";
                                    }
                                    else
                                    {
                                        if (userInfo.Sex == "男")
                                        {
                                            userInfo.HeadPortrait = "/upload/HeadPortrait/Default/avatar＿male.jpg";
                                        }
                                        else
                                        {
                                            userInfo.HeadPortrait = "/upload/HeadPortrait/Default/avatar＿female.jpg";
                                        }
                                    }
                                    data = new { userInfo.Id, userInfo.UserName, userInfo.RealName, userInfo.HeadPortrait };
                                    code = 1;
                                    message = "登录成功！";


                                    var logService = new LoginLogService(Services);
                                    var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                                    if (ip == "::1")
                                    {
                                        ip = "127.0.0.1";
                                    }
                                    var loginLog = new LoginLog
                                    {
                                        UserName = loginModel.UserName,
                                        RealName = userInfo.RealName,
                                        Address = ip,
                                        Browser = Request.Headers["User-Agent"]
                                    };
                                    logService.AddLoginLog(loginLog);

                                }
                                else
                                {
                                    code = 2;
                                    message = "未找到用户！";
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                code = 4;
                message = "登录失败，请稍后重试！";
                data = exception.Message;
            }
            return new { code, message, data };
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<dynamic> Logout()
        {
            int code;
            string message;
            try
            {
                await SignInManager.SignOutAsync();
                code = 1;
                message = "注销成功！";
            }
            catch (Exception exception)
            {
                code = 4;
                message = "注销失败，请稍后重试！";
            }
            return new { code, message };
        }

        /// <summary>
        /// 忘记密码（登录页面）
        /// </summary>
        /// <param name="obj">提交信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<dynamic> FindPassword([FromBody]FindPasswordModel obj)
        {
            var code = 5;
            var message = "重置密码失败！";
            if (!ModelState.IsValid)
            {
                return Json(new { code = 5, message = "参数错误" });
            }
            try
            {
                var contact = obj.Contact;
                var validateCode = obj.ValidateCode;
                var password = obj.Password;
                var emailcode = _cache.GetObject<string>(string.Format("code_{0}", validateCode));
                if (emailcode != null)
                {
                    if (string.Equals(emailcode, validateCode.Trim()))
                    {
                        ApplicationUser userInfo;
                        userInfo = await UserManager.FindByEmailAsync(contact);
                        if (userInfo != null)
                        {
                            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(userInfo);
                            var result = await UserManager.ResetPasswordAsync(userInfo, resetToken, password);
                            if (result.Succeeded)
                            {
                                _cache.Remove(string.Format("code_{0}", validateCode)); //清空验证码
                                code = 3;
                                message = "重置密码成功！";
                            }
                            else
                            {
                                code = 3;
                                message = "新密码格式不符合要求！";
                            }
                        }
                        else
                        {
                            message = "用户不存在！";
                        }
                    }
                    else
                    {
                        message = "验证码不正确！";
                    }
                }
            }
            catch (Exception exception)
            {
                if (code != 3)
                {
                    code = 4;
                    message = "重置密码失败，请稍后重试！";
                }
            }
            return new { code, message };
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="obj">提交信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<dynamic> Send([FromBody]FindPasswordModel obj)
        {
            var code = 5;
            var message = "发送失败！";
            if (!ModelState.IsValid)
            {
                return Json(new { code = 5, message = "参数错误" });
            }
            try
            {
                var contact = obj.Contact;
                if (!string.IsNullOrEmpty(contact))
                {
                    var validateCode = CreateCode(6);
                    _cache.SetObject(string.Format("code_{0}", validateCode), validateCode, TimeSpan.FromMinutes(5));
                    var smsstr = "验证码：" + validateCode + "，请勿泄露给他人。";
                    var userInfo = await UserManager.FindByEmailAsync(contact);
                    if (userInfo != null)
                    {
                        var es = new EmailSender(_emailOptions);
                        es.AddReciever(contact);
                        es.Send("重置密码", smsstr);
                        code = 3;
                        message = "发送成功！";
                    }
                    else
                    {
                        message = "用户不存在！";
                    }
                }
            }
            catch (Exception exception)
            {
                code = 4;
                message = "发送失败，请稍后重试！";
            }
            return new { code, message };
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="codeLength">验证码长度</param>
        /// <returns></returns>
        public static string CreateCode(int codeLength)
        {
            const string so = "1,2,3,4,5,6,7,8,9,0";
            var strArr = so.Split(',');
            var code = "";
            var rand = new Random();
            for (var i = 0; i < codeLength; i++)
            {
                code += strArr[rand.Next(0, strArr.Length)];
            }
            return code;
        }
        [HttpGet("[action]")]
        public ActionResult zxczxcxzczc()
        {
            var filepath = Environment.WebRootPath;  //文件存储路径
            filepath = System.IO.Path.Combine(filepath, "upload\\Information\\0530c65f-b662-4585-ad1e-f16ace73f16d");
            Utils.ImageControl.ImageControl ic = new Utils.ImageControl.ImageControl();
            byte[] bytes = ic.resizeImage("Collection_Gold-Mask.png", filepath, 400, 400);
            FileContentResult fresult = File(bytes, "application/octet-stream", "Collection_Gold-Mask.png");
            return fresult;
        }



        /// <summary>
        /// 搜索三级页面
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult SearchThreeLevelId()
        {
            var dataqqq = Context.InformationEnglishAll.Where(x => x.ColumName == "Announcements" || x.ColumName == "News" || x.ColumName == "Arts" || x.ColumName == "Cultural Creation" || x.ColumName == "Exhibition Hall" || x.ColumName == "Exhibition for Hire" || x.ColumName == "Featured Exhibition" || x.ColumName == "Jinsha Sun Festival" || x.ColumName == "Upcoming lectures" || x.ColumName == "International Programs" || x.ColumName == "Ten-year Jinsha" || x.ColumName == "International Museum Day" || x.ColumName == "National Cultural Heritage Day" || x.ColumName == "Cultural Events" || x.ColumName == "Wallpapers" || x.ColumName == "Ticketing" || x.ColumName == "Interpreter" || x.ColumName == "Food"|| x.ColumName == "Information");
            var newdata = dataqqq.Select(e => new { e.Id, e.Title });

            return Json(newdata);
        }

    }
}
