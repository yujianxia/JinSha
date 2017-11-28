using SteponTech.Utils.DHSH;
using SteponTech.Utils.DHSH.Model;
using SteponTech.Utils.DHSH.Model.Config;
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : BaseController<UserController, Data.SteponContext, ApplicationUser, IdentityRole>
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        public UserController(IConfiguration config, IHostingEnvironment env)
        {
            _configuration = config;
            Environment = env;
        }

        /// <summary>
        /// 获取所有User信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAllUsers()
        {
            try
            {
                //var name = User.Identity.Name;
                var data = UserManager.Users.ToList();
                if (data?.Count > 0)
                {
                    var newdata = data.Select(e => new { e.UserName, e.RealName });

                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "查询用户列表成功！"
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
        /// 获取自己的信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetUserByMy()
        {
            try
            {
                UserService userService = new UserService(Services);
                var data = userService.GeyUser_All();
                if (data?.Count > 0)
                {
                    var userinfo = data.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                    var weburl = _configuration.GetSection("Message:WebUrl").Value;
                    var filepath = Environment.WebRootPath;
                    filepath = System.IO.Path.Combine(filepath + "\\upload\\HeadPortrait", userinfo.Id + ".jpg");
                    if (System.IO.File.Exists(filepath))
                    {

                        userinfo.HeadPortrait = weburl + "/upload/HeadPortrait/" + userinfo.Id + ".jpg";
                    }
                    else
                    {
                        if (userinfo.Sex == "男")
                        {
                            userinfo.HeadPortrait = weburl + "/upload/HeadPortrait/Default/avatar＿male.jpg";
                        }
                        else
                        {
                            userinfo.HeadPortrait = weburl + "/upload/HeadPortrait/Default/avatar＿female.jpg";
                        }
                    }
                    return Json(userinfo);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 新增User信息
        /// </summary>
        /// <param name="Users">Users信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<dynamic> AddUserDetail([FromBody]ApplicationUser Users)
        {
            var isresult = false;
            string message = "新增失败";
            try
            {
                var userName = Users.UserName;
                if (await UserManager.FindByNameAsync(userName) == null)
                {
                    Users.CreatTime = Users.LastUpdateTime = DateTime.Now;
                    Users.LockoutEnabled = false;
                    var res = await UserManager.CreateAsync(Users, Users.PasswordHash);
                    if (res.ToString() == "Succeeded")
                    {
                        isresult = true;
                        message = "注册成功！";
                    }
                    else
                    {
                        isresult = false;
                        message = res.ToString();
                    }

                }
                else
                {
                    message = "注册失败，已存在该用户！";
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 删除User信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public async Task<dynamic> DeleteUser(string id)
        {
            var isresult = false;
            string message = "删除失败";
            try
            {
                var userinfo = await UserManager.FindByIdAsync(id);
                if (userinfo != null)
                {
                   
                    var res = await UserManager.DeleteAsync(userinfo);
                    if (res.ToString() == "Succeeded")
                    {
                        isresult = true;
                        message = "删除成功！";
                    }
                    else
                    {
                        isresult = false;
                        message = res.ToString();
                    }

                }
                else
                {
                    message = "删除失败，不存在该用户！";
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return Json(new { result = isresult, message });
        }

        /// <summary>
        /// 修改User信息
        /// </summary>
        /// <param name="Users">Users信息</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<dynamic> UpdateUserDetail([FromBody]ApplicationUser Users)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                var userinfo = await UserManager.FindByIdAsync(Users.Id);
                userinfo.LastUpdateTime = DateTime.Now;
                userinfo.Email = Users.Email;
                userinfo.PhoneNumber = Users.PhoneNumber;
                userinfo.HeadPortrait = Users.HeadPortrait;
                userinfo.RealName = Users.RealName;
                userinfo.Sex = Users.Sex;
                userinfo.LockoutEnabled = Users.LockoutEnabled;
                userinfo.Role = Users.Role;
                var result = await UserManager.UpdateAsync(userinfo);
                if (result.Succeeded)
                {
                    isresult = true;
                    message = "修改成功！";
                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "修改用户资料成功！"
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
        /// 修改User禁用状态
        /// </summary>
        /// <param name="id">Usersid</param>
        /// <param name="islock">islock</param>
        /// <returns></returns>
        [HttpPut("[action]/{id}/{islock}")]
        public async Task<dynamic> UpdateUserLockout(string id, string islock)
        {
            var isresult = false;
            var message = "修改失败！";
            try
            {
                var userinfo = await UserManager.FindByIdAsync(id);
                userinfo.LockoutEnabled = bool.Parse(islock);
                var result = await UserManager.UpdateAsync(userinfo);
                if (result.Succeeded)
                {
                    isresult = true;
                    message = "修改成功！";
                    var loginusername = User.Identity.Name;
                    Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                    SysLog.AddLog(new Data.CommonModel.SystemLog
                    {
                        UserName = loginusername,
                        OpreationMode = "用户：" + loginusername + "修改用户禁用状态成功！"
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
        /// 验证User密码
        /// </summary>
        /// <param name="oldpassword">旧密码</param>
        /// <returns></returns>
        //[HttpPut("{oldpassword}")]
        //public async Task<dynamic> ValidationUserPass(string oldpassword)
        //{
        //    var isresult = false;
        //    var message = "原始密码错误！";
        //    try
        //    {
        //        var username = User.Identity.Name;
        //        if (string.IsNullOrEmpty(oldpassword))
        //        {
        //            message = "密码未填写！";
        //        }
        //        else
        //        {
        //            var userinfo = await UserManager.FindByNameAsync(username);
        //            if (userinfo != null)
        //            {
        //                var checkPassword = await UserManager.CheckPasswordAsync(userinfo, oldpassword);
        //                if (checkPassword)
        //                {
        //                    isresult = true;
        //                    message = "原始密码正确";
        //                }
        //                else
        //                {
        //                    isresult = false;
        //                    message = "原始密码错误";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        isresult = false;
        //        message = "验证失败，请稍后重试！" + exception.Message + "";
        //    }
        //    return Json(new { result = isresult, message });
        //}
        /// <summary>
        /// 修改User密码
        /// </summary>
        /// <param name="oldpassword">旧密码</param>
        /// <param name="newpassword">新密码</param>
        /// <returns></returns>
        [HttpPut("[action]/{oldpassword}/{newpassword}")]
        public async Task<dynamic> UpdateUserPass(string oldpassword,string newpassword)
        {
            //暂停 未作更新密码 没有测试
            var isresult = false;
            var message = "修改失败！";
            try
            {
                var username = User.Identity.Name;
                if (string.IsNullOrEmpty(oldpassword))
                {
                    message = "密码未填写！";
                }
                else
                {
                    var userinfo = await UserManager.FindByNameAsync(username);
                    if (userinfo != null)
                    {
                        var checkPassword = await UserManager.CheckPasswordAsync(userinfo, oldpassword);
                        if (checkPassword)
                        {
                            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(userinfo);
                            var result = await UserManager.ResetPasswordAsync(userinfo, resetToken, newpassword);

                            if (result.Succeeded)
                            {
                                isresult = true;
                                message = "修改成功！";
                                var loginusername = User.Identity.Name;
                                Services.CommonService.SystemLogService SysLog = new Services.CommonService.SystemLogService(Services);
                                SysLog.AddLog(new Data.CommonModel.SystemLog
                                {
                                    UserName = loginusername,
                                    OpreationMode = "用户：" + loginusername + "修改用户资料成功！"
                                });
                            }
                            else
                            {
                                message = "密码强度较低！";
                            }
                        }
                        else
                        {
                            isresult = false;
                            message = "旧密码填写错误";
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
        /// 根据id查询Users详细信息
        /// </summary>
        /// <param name="id">Resource id</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<dynamic> GetUserById(Guid id)
        {
            try
            {
                var userinfo = await UserManager.FindByIdAsync(id.ToString());
                var weburl = _configuration.GetSection("Message:WebUrl").Value;
                var filepath = Environment.WebRootPath;
                filepath = System.IO.Path.Combine(filepath + "\\upload\\HeadPortrait", userinfo.Id + ".jpg");
                if (System.IO.File.Exists(filepath))
                {

                    userinfo.HeadPortrait = weburl + "/upload/HeadPortrait/" + userinfo.Id + ".jpg";
                }
                else
                {
                    if (userinfo.Sex == "男")
                    {
                        userinfo.HeadPortrait = weburl + "/upload/HeadPortrait/Default/avatar＿male.jpg";
                    }
                    else
                    {
                        userinfo.HeadPortrait = weburl + "/upload/HeadPortrait/Default/avatar＿female.jpg";
                    }
                }
                return Json(userinfo);
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
