using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SteponTech.Utils.DHSH;
using SteponTech.Utils.DHSH.Model;
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Model.Config;
using SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart;
using Microsoft.AspNetCore.Http;
using SteponTech.Utils.DHSH.Utils;
using SteponTech.Data.TrunkSystem;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// 会员API
    /// </summary>
    [Route("api/[controller]")]
    public class MembersController : Controller
    {
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="phone">注册电话</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet("{phone}/{password}")]
        public JsonResult LoginMembers(string phone, string password)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("LoginMembers"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="phone",value=phone},
                    new Parameter{ name="password",value=password}
                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        string session = data.body.dataTable.dataRow[0].column.Where(x => x.name == "session").FirstOrDefault().value;
                        string volunteerId = data.body.dataTable.dataRow[0].column.Where(x => x.name == "member_id").FirstOrDefault().value;
                        //获取session
                        HttpContext.Session.SetString("loginsession", session);
                        HttpContext.Session.SetString("loginid", volunteerId);
                        return Json(new { code = 1, message = "登录成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 判断是否登录
        /// </summary>
        public JsonResult IsLogin()
        {
            if (HttpContext.Session.GetString("loginsession") != null)
            {
                return Json(new { code = 1, message = "数据获取成功！", data = true });//获取数据失败
            }
            else
            {
                return Json(new { code = 1, message = "数据获取成功！", data = false });//获取数据失败
            }
        }

        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="MemversDetail">会员实体</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult AddMembers([FromBody]MemversDetail MemversDetail)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("AddMembers"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="phone",value=MemversDetail.phone},
                    new Parameter{ name="password",value=MemversDetail.password},
                    new Parameter{ name="register_from",value="2"},
                    new Parameter{ name="user_name",value=MemversDetail.user_name},
                    new Parameter{ name="email",value=MemversDetail.email},
                    new Parameter{ name="nickname",value=MemversDetail.nickname}
                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag) return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                var meassgeResult = data.body.dataTable.dataListMap.list[0];
                if (meassgeResult.column[0].value == "FAIL")
                {
                    return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                }
                else
                {
                    return Json(new { code = 1, message = "注册成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败
                }
                //读取数据部分
            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult MembersLoginout()
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("MembersLoginout"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="session",value=session}
                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            //移除session
                            HttpContext.Session.Remove("loginsession");
                            HttpContext.Session.Remove("loginid");


                            return Json(new { code = 1, message = "注销成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }
                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }
        
        /// <summary>
        /// 会员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult MembersSelect()
        {
            //这个接口有问题
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("MembersSelect"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="session",value=session}
                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "数据获取成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }
                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        //有问题
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult UpdateMembers([FromBody]MemversDetail MemversDetail)
        {
            //这个接口有问题
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("UpdateMembers"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="session",value=session},
                    new Parameter{ name="nickname",value=MemversDetail.nickname},
                    new Parameter{ name="email",value=MemversDetail.email},
                    new Parameter{ name="address",value=MemversDetail.address},
                    new Parameter{ name="username",value=MemversDetail.user_name},
                    new Parameter{ name="phone",value=MemversDetail.phone}
                    });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "修改成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }
                }
                else
                {
                    return Json(new { code = 4, message = "尚未登录！" });//获取数据失败
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 4, message = "数据修改失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult ForgotPassword([FromBody]MemversDetail MemversDetail)
        {
            try
            {

                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("ForgotPassword"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="phone",value=MemversDetail.phone},
                    new Parameter{ name="email",value=MemversDetail.email}
                    });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        return Json(new { code = 1, message = "修改成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据修改失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 志愿者升级或注销
        /// </summary>
        /// <param name="volunteer">0:注销志愿者 1:设置为志愿者</param>
        /// <returns></returns>
        [HttpPost("[action]/{volunteer}")]
        public JsonResult VolunteersUpDown(string volunteer)
        {
            try
            {
                string volunteerId = HttpContext.Session.GetString("loginid");
                if (volunteerId != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("VolunteersUpDown"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="member_id",value=volunteerId},
                        new Parameter{ name="volunteer",value=volunteer}

                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "操作成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }

                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "操作失败！", data = e.Message });//获取数据失败
            }
        }
        
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password">新密码</param>
        /// <param name="oldpassword">旧密码</param>
        /// <returns></returns>
        [HttpPost("[action]/{password}/{oldpassword}")]
        public JsonResult UpdatePassword(string password, string oldpassword)
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("UpdatePassword"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="session",value=session},
                        new Parameter{ name="password",value=password},
                        new Parameter{ name="oldPassword",value=oldpassword}
                    });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "登录成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }

                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }
            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 我的兑换的礼物
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult MyExchange()
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("MyExchange"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="session",value=session}
                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }
                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 积分变动情况
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{pageIndex}/{pageSize}")]
        public JsonResult IntegralChange(string pageIndex, string pageSize)
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("IntegralChange"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="session",value=session},
                    new Parameter{ name="pageIndex",value=pageIndex},
                    new Parameter{ name="pageSize",value=pageSize}
                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败", data = error.message });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }
                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 积分商品列表
        /// </summary>
        /// <param name="pageIndex">起始页 最低为1</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        [HttpGet("[action]/{pageIndex}/{pageSize}")]
        public JsonResult GiftList(string pageIndex, string pageSize)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("GiftList"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="pageIndex",value=pageIndex},
                    new Parameter{ name="pageSize",value=pageSize}
                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 积分商品兑换
        /// </summary>
        /// <param name="gift_id">礼品ID</param>
        /// <returns></returns>
        [HttpGet("[action]/{gift_id}")]
        public JsonResult GiftExchange(string gift_id)
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("GiftExchange"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="session",value=session},
                    new Parameter{ name="gift_id",value=gift_id}
                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "兑换成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }

                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "兑换失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 查询已预约的活动
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult MyActivity()
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("MyActivity"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="session",value=session}
                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }
                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }
        
        /// <summary>
        /// 增加积分
        /// </summary>
        /// <param name="score">分数</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        [HttpPost("[action]/{score}/{remark}")]
        public JsonResult IncreaseIntegral(string score, string remark)
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("IncreaseIntegral"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="session",value=session},
                        new Parameter{ name="score",value=score},
                        new Parameter{ name="remark",value=remark}

                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "积分增加成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }

                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "积分增加失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 活动签到
        /// </summary>
        /// <param name="activity_id">活动ID</param>
        /// <returns></returns>
        [HttpPost("[action]/{activity_id}")]
        public JsonResult ActivitiesIn(string activity_id)
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("ActivitiesIn"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="activity_id",value=activity_id},
                        new Parameter{ name="session",value=session}

                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "签到成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }

                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "签到失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 活动预约
        /// </summary>
        /// <param name="type">1 预约 0:取消预约</param>
        /// <param name="activity_id">活动ID</param>
        /// <param name="requried_info">必填信息(仅预约使用)</param>
        /// <returns></returns>
        [HttpPost("[action]/{type}/{activity_id}/{requried_info}")]
        public JsonResult ActivitiesAppointment(string type, string activity_id, string requried_info)
        {
            try
            {
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    //数据获取对象
                    var ob = new Obtainer();
                    Error error;
                    ReturnToken token;
                    //获取token
                    var flag = ob.GetToken(out token, out error);
                    if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                    //创建并初始化获取数据的配置对象
                    var config = new DataConfig(ConfigHelper.GetValue("ActivitiesAppointment"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="type",value=type},
                        new Parameter{ name="activity_id",value=activity_id},
                        new Parameter{ name="session",value=session},
                        new Parameter{ name="requried_info",value=requried_info}

                });
                    ReturnData data;
                    //获取数据
                    flag = ob.GetData(config, out data, out error);
                    if (!flag)
                    {
                        return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                    }
                    else
                    {
                        var meassgeResult = data.body.dataTable.dataListMap.list[0];
                        if (meassgeResult.column[0].value == "FAIL")
                        {
                            return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                        }
                        else
                        {
                            return Json(new { code = 1, message = "操作成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                        }
                    }

                }
                else
                {
                    return Json(new { code = 5, message = "尚未登录！" });//获取数据失败
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "操作失败！", data = e.Message });//获取数据失败
            }
        }


        #region 获取活动信息
        /// <summary>
        /// 获取所有活动信息
        /// </summary>
        /// <param name="pageIndex">起始页 最低为1</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        [HttpGet("[action]/{pageIndex}/{pageSize}")]
        public JsonResult ActivityInformation(string pageIndex, string pageSize)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("ActivityInformation"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="pageIndex",value=pageIndex},
                        new Parameter{ name="pageSize",value=pageSize},
                        new Parameter{ name="session",value=session}
                    });
                }
                else
                {
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="pageIndex",value=pageIndex},
                        new Parameter{ name="pageSize",value=pageSize}
                    });
                }
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "数据获取失败！" });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 根据活动id获取活动信息
        /// </summary>
        /// <param name="pageIndex">起始页 最低为1</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="id">活动ID</param>
        /// <returns></returns>
        [HttpGet("[action]/{pageIndex}/{pageSize}/{id}")]
        public JsonResult ActivityInformationById(string pageIndex, string pageSize,string id)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("ActivityInformationById"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="pageIndex",value=pageIndex},
                        new Parameter{ name="pageSize",value=pageSize},
                        new Parameter{ name="id",value=id},
                        new Parameter{ name="session",value=session}
                    });
                }
                else
                {
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="pageIndex",value=pageIndex},
                        new Parameter{ name="pageSize",value=pageSize},
                        new Parameter{ name="id",value=id}
                    });
                }
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "数据获取失败！" });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 根据活动类别获取活动信息
        /// </summary>
        /// <param name="pageIndex">起始页 最低为1</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="class_id">活动类型(0:普通活动 1:社教活动 10:志愿者活动')</param>
        /// <returns></returns>
        [HttpGet("[action]/{pageIndex}/{pageSize}/{class_id}")]
        public JsonResult ActivityInformationByClassId(string pageIndex, string pageSize, string class_id)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("ActivityInformationByClassId"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="pageIndex",value=pageIndex},
                        new Parameter{ name="pageSize",value=pageSize},
                        new Parameter{ name="class_id",value=class_id},
                        new Parameter{ name="session",value=session}
                    });
                }
                else
                {
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="pageIndex",value=pageIndex},
                        new Parameter{ name="pageSize",value=pageSize},
                        new Parameter{ name="class_id",value=class_id}
                    });
                }
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "数据获取失败！" });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        [HttpGet("[action]/{Index}/{PageSize}/{IsAll}")]
        public JsonResult asdfasdfasdfsadfasdfasdfsadf(string Index, string PageSize, string IsAll)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig("1831a182");
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                string session = HttpContext.Session.GetString("loginsession");
                if (session != null)
                {
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="Index",value=Index},
                        new Parameter{ name="PageSize",value=PageSize},
                        new Parameter{ name="IsAl",value=IsAll}
                    });
                }
                else
                {
                    config.postData.setParameters(new List<Parameter> {
                       new Parameter{ name="Index",value=Index},
                        new Parameter{ name="PageSize",value=PageSize},
                        new Parameter{ name="IsAl",value=IsAll}
                    });
                }
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "数据获取失败！" });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        #endregion

        /// <summary>
        /// 查询会员信息
        /// </summary>
        /// <param name="phone">注册电话</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet("[action]/{phone}/{password}")]
        public JsonResult MembersInformation(string phone, string password)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("MembersInformation"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="phone",value=phone},
                    new Parameter{ name="password",value=password}
                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "操作失败",data = error.message  });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        return Json(new { code = 1, message = "数据获取成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }
    }
}
