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
using SteponTech.Data.TrunkSystem;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteponTech.ApiControllers.Admin
{
    /// <summary>
    /// 馆藏系统API
    /// </summary>
    [Route("api/[controller]")]
    public class CollectionController : Controller
    {
        /// <summary>
        /// 获取精品馆藏文物
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
                var config = new DataConfig("4bbe0296");
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
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }

        /// <summary>
        /// 文物主体数据
        /// </summary>
        /// <param name="name">文物名称</param>
        /// <param name="sss">sss描述</param>
        /// <returns></returns>
        [HttpGet("[action]/{name}/{sss}")]
        public JsonResult SubjectCultural(string name, string sss)
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
                var config = new DataConfig("50e59833");
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Parameter> {
                    new Parameter{ name="name",value=name},
                    new Parameter{ name="sss",value=sss}
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
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }
    }
}
