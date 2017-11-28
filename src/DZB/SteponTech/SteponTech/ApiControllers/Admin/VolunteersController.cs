using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SteponTech.Utils.DHSH;
using SteponTech.Utils.DHSH.Model;
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Model.Config;
using SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart;
using Microsoft.AspNetCore.Http;
using SteponTech.Data.TrunkSystem;
using SteponTech.Utils.DHSH.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// 志愿者API
    /// </summary>
    [Route("api/[controller]")]
    public class VolunteersController : Controller
    {
        /// <summary>
        /// 志愿者活动预约状态查询
        /// </summary>
        /// <param name="activityId">活动ID</param>
        /// <param name="volunteerId">志愿者在会员系统的ID</param>
        /// <returns></returns>
        [HttpGet("[action]/{activityId}/{volunteerId}")]
        public JsonResult VolunteersActivityState(string activityId, string volunteerId)
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
                var config = new DataConfig(ConfigHelper.GetValue("VolunteersActivityState"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="activityId",value=activityId},
                    new Parameter{ name="volunteerId",value=volunteerId}
                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag) return Json(new { code = 3, message = "操作失败", data = error.message });//获取数据失败
                return Json(new { code = 1, message = "数据获取成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败
                //读取数据部分
            }
            catch (Exception e)
            {
                return Json(new { code = 4, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 志愿者相关选项查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult VolunteersOptions()
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
                var config = new DataConfig(ConfigHelper.GetValue("VolunteersOptions"));
                //设置token信息
                config.postData.setToken(token);
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag) return Json(new { code = 3, message = "操作失败", data = error.message });//获取数据失败
                return Json(new { code = 1, message = "数据获取成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败
                //读取数据部分
            }
            catch (Exception e)
            {
                return Json(new { code = 4, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        //有问题
        /// <summary>
        /// 志愿者活动预约状态查询
        /// </summary>
        /// <param name="activity_id">活动ID</param>
        /// <returns></returns>
        [HttpGet("[action]/{activity_id}")]
        public JsonResult ReservationStatusQuery(string activity_id)
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
                    var config = new DataConfig(ConfigHelper.GetValue("ReservationStatusQuery"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="activity_id",value=activity_id},
                        new Parameter{ name="volunteerId",value=volunteerId}

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
                return Json(new { code = 6, message = "查询数据失败！", data = e.Message });//获取数据失败
            }
        }

        //未测试
        /// <summary>
        /// 签退接口
        /// </summary>
        /// <param name="endTime">签退时间</param>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="activityId">活动ID</param>
        /// <returns></returns>
        [HttpPost("[action]/{endTime}/{attendanceDate}/{activityId}")]
        public JsonResult VolunteersSignback(string endTime, string attendanceDate, string activityId)
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
                    var config = new DataConfig(ConfigHelper.GetValue("VolunteersSignback"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="volunteerId",value=volunteerId},
                        new Parameter{ name="endTime",value=endTime},
                        new Parameter{ name="attendanceDate",value=attendanceDate},
                        new Parameter{ name="activityId",value=activityId}

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
                            return Json(new { code = 1, message = "签退成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
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
                return Json(new { code = 6, message = "签退失败！", data = e.Message });//获取数据失败
            }
        }

        //有问题
        /// <summary>
        /// 志愿者参与的活动查询接口
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="records">每页显示几条</param>
        /// <returns></returns>
        [HttpGet("[action]/{page}/{records}")]
        public JsonResult VolunteersJoinActivity(string page, string records)
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
                    var config = new DataConfig(ConfigHelper.GetValue("VolunteersJoinActivity"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="volunteerId",value=volunteerId},
                        new Parameter{ name="page",value=page},
                        new Parameter{ name="records",value=records}

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
                return Json(new { code = 6, message = "查询数据失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 获取所有活动信息
        /// </summary>
        /// <param name="pageIndex">起始页 最低为1</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        [HttpGet("[action]/{pageIndex}/{pageSize}")]
        public JsonResult ReviewActivities(string pageIndex, string pageSize)
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
                var config = new DataConfig(ConfigHelper.GetValue("ReviewActivities"));
                //设置token信息
                config.postData.setToken(token);
                config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="pageIndex",value=pageIndex},
                        new Parameter{ name="pageSize",value=pageSize}
                    });
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

        //未测试
        /// <summary>
        /// 签到接口
        /// </summary>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="startTime">签到时间</param>
        /// <param name="journal">服务日志</param>
        /// <param name="activityId">活动ID</param>
        /// <returns></returns>
        [HttpPost("[action]/{attendanceDate}/{startTime}/{journal}/{activityId}")]
        public JsonResult VolunteersSignin(string attendanceDate, string startTime, string journal, string activityId)
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
                    var config = new DataConfig(ConfigHelper.GetValue("VolunteersSignin"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="attendanceDate",value=attendanceDate},
                        new Parameter{ name="startTime",value=startTime},
                        new Parameter{ name="journal",value=journal},
                        new Parameter{ name="activityId",value=activityId},
                        new Parameter{ name="volunteerId",value=volunteerId}

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

        //未测试
        /// <summary>
        /// 活动取消参与接口
        /// </summary>
        /// <param name="activity_id">活动ID</param>
        /// <returns></returns>
        [HttpPost("[action]/{activity_id}")]
        public JsonResult VolunteersCancelActivity(string activity_id)
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
                    var config = new DataConfig(ConfigHelper.GetValue("VolunteersCancelActivity"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="activity_id",value=activity_id},
                        new Parameter{ name="volunteerId",value=volunteerId}

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
                return Json(new { code = 6, message = "查询数据失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 活动报名接口
        /// </summary>
        /// <param name="activityId">活动ID</param>
        /// <param name="other">其他信息</param>
        /// <returns></returns>
        [HttpPost("[action]/{activityId}/{other}")]
        public JsonResult ActivitySignup(string activityId, string other)
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
                    var config = new DataConfig(ConfigHelper.GetValue("ActivitySignup"));
                    //设置token信息
                    config.postData.setToken(token);
                    //设置参数信息
                    config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="activityId",value=activityId},
                        new Parameter{ name="volunteerId",value=volunteerId},
                        new Parameter{ name="other",value=other}

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
                            return Json(new { code = 1, message = "活动报名成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
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
                return Json(new { code = 6, message = "活动报名失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 志愿者信息查询接口
        /// </summary>
        /// <param name="id">ID唯一标示</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public JsonResult VolunteersSelectById(string id)
        {
            try
            {
                if (id == "-1")
                {
                    //id = "1168";
                    id = HttpContext.Session.GetString("loginid");
                }
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("VolunteersSelectById"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="id",value=id}

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
                        return Json(new { code = 1, message = "数据获取成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 添加志愿者信息
        /// </summary>
        /// <param name="VolunteersDetail">志愿者实体</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult VolunteersAdd([FromBody]VolunteersDetail VolunteersDetail)
        {
            try
            {
                //VolunteersDetail.systemId = HttpContext.Session.GetString("loginid");
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("VolunteersAdd"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                        new Parameter{ name="systemId",value=HttpContext.Session.GetString("loginid")},
                        new Parameter{ name="name",value=VolunteersDetail.name},
                        new Parameter{ name="sex",value=VolunteersDetail.sex},
                        new Parameter{ name="nation",value=VolunteersDetail.nation},
                        new Parameter{ name="birthday",value=VolunteersDetail.birthday},
                        new Parameter{ name="height",value=VolunteersDetail.height},
                        new Parameter{ name="political",value=VolunteersDetail.political},
                        new Parameter{ name="occupation",value=VolunteersDetail.occupation},
                        new Parameter{ name="qualifications",value=VolunteersDetail.qualifications},
                        new Parameter{ name="health",value=VolunteersDetail.health},
                        new Parameter{ name="mobile",value=VolunteersDetail.mobile},
                        new Parameter{ name="email",value=VolunteersDetail.email},
                        new Parameter{ name="idNumber",value=VolunteersDetail.idNumber},
                        new Parameter{ name="unit",value=VolunteersDetail.unit},
                        new Parameter{ name="specialty",value=VolunteersDetail.specialty},
                        new Parameter{ name="rAndE",value=VolunteersDetail.rAndE},
                        new Parameter{ name="serviceTime",value=VolunteersDetail.serviceTime},
                        new Parameter{ name="serviceTerm",value=VolunteersDetail.serviceTerm},
                        new Parameter{ name="servicePost",value=VolunteersDetail.servicePost},
                        new Parameter{ name="type",value=VolunteersDetail.type},
                        new Parameter{ name="guardian",value=VolunteersDetail.guardian},
                        new Parameter{ name="img",value=VolunteersDetail.img}

                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag) return Json(new { code = 3, message = "操作失败", data = error.message });//获取数据失败
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
    }
}
