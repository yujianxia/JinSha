/*****************************
 * 作者：xzf
 * 日期：2017年7月21日 16:06:08
 * 操作：创建
 * ****************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SteponTech.Data;
using Stepon.Mvc.Controllers;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;
using SteponTech.Services.BaseServices;
using SteponTech.Utils.DHSH.Utils;
using SteponTech.Utils.DHSH.Model.Config;
using SteponTech.Utils.DHSH;
using SteponTech.Utils.DHSH.Model;
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart;
using SteponTech.Data.BaseModels;
using System.Text;
using SteponTech.Services.CommonService;
using Newtonsoft.Json;

namespace SteponTech.ApiControllers
{
    /// <summary>
    /// 总线服务API
    /// </summary>
    public class DataHubAccessController : BaseController<DataHubAccessController, SteponContext>
    {
        #region 无效登录
        /// <summary>
        /// 无效登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void Login()
        {
            return;
        }
        #endregion

        #region 获取咨询列表
        /// <summary>
        /// 获取咨询列表
        /// </summary>
        [HttpPost]
        public void GetInformation()
        {
            try
            {
                var keyid = Request.Form["keyid"].ToString();
                var data = Request.Form["data"].ToString();
                var aesc = new AESConfig(keyid);
                var aesh = new AesHelper();
                data = aesh.Decrypt(data, aesc);
                var postData = (DHPostData)new DataFactory().CreateData(DataType.DH_Post_Data, data);
                var paras = postData.body.accessParameters.parameters.parameter;
                var index = int.Parse(paras.AsQueryable().FirstOrDefault(e => e.name == "Index").value);
                var pageSize = int.Parse(paras.AsQueryable().FirstOrDefault(e => e.name == "PageSize").value);
                var isAll = bool.Parse(paras.AsQueryable().FirstOrDefault(e => e.name == "IsAll").value);
                var service = new DataHubAccessService(Services);
                var infos = service.GetInformationAllByCondition(index, pageSize, isAll);
                var datam = GetDataModel(infos);
                var extdata = GetResultModel(infos.Count != 0, infos.Count != 0 ? "获取成功！" : "没有数据！");
                var result = new ReturnData("");
                result.body.dataTable.name = "Information";
                result.body.dataTable.remark = "资讯";
                result.SetData(datam);
                result.SetExtData(new List<RDataTableListItem> { extdata });
                var temp = aesh.Encrypt(result.ToString(), aesc);
                var bytes = Encoding.UTF8.GetBytes(new WebHelper().ToUrlString(temp));
                Response.Body.Write(bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.ToString() + "\r\n");
                var temp = Error();
                Response.Body.Write(temp, 0, temp.Length);
            }
        }
        #endregion

        #region 统计信息

        /// <summary>
        /// 获取地区统计信息
        /// </summary>
        [HttpPost]
        public void GetCountryStatistics()
        {
            try
            {
                var keyid = Request.Form["keyid"].ToString();
                var data = Request.Form["data"].ToString();
                var aesc = new AESConfig(keyid);
                var aesh = new AesHelper();
                data = aesh.Decrypt(data, aesc);
                var postData = (DHPostData)new DataFactory().CreateData(DataType.DH_Post_Data, data);
                var paras = postData.body.accessParameters.parameters.parameter;
                var start = DateTime.Parse(paras.AsQueryable().FirstOrDefault(e => e.name == "Start").value);
                var end = DateTime.Parse(paras.AsQueryable().FirstOrDefault(e => e.name == "End").value);
                if (start < end)
                {
                    RequestStatisticalService brandService = new RequestStatisticalService(Services);
                    var RequestStatisticalList = brandService.SelectRequestStatisticalBy(start, end);
                    var resultdata = new List<RDataRow>();
                    foreach (var ip in RequestStatisticalList.RequestStatisticalCountry)
                    {
                        var countries = new List<RDataColumn>();
                        countries.Add(new RDataColumn
                        {
                            name = "Country",
                            type = "字符串",
                            value = ip.request,
                            remark = "地区"
                        });
                        countries.Add(new RDataColumn
                        {
                            name = "Sum",
                            type = "整数",
                            value = ip.traffic,
                            remark = "访问次数"
                        });
                        resultdata.Add(new RDataRow { column = countries });
                    }
                    var extdata = GetResultModel(true, "获取成功！");
                    var result = new ReturnData("");
                    result.body.dataTable.name = "Statistics";
                    result.body.dataTable.remark = "统计信息";
                    result.SetData(resultdata);
                    result.SetExtData(new List<RDataTableListItem> { extdata });
                    var temp = aesh.Encrypt(result.ToString(), aesc);
                    var bytes = Encoding.UTF8.GetBytes(new WebHelper().ToUrlString(temp));
                    Response.Body.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    var result = new ReturnData("");
                    result.SetExtData(new List<RDataTableListItem> { GetResultModel(false, "日期填写错误！") });
                    var temp = Encoding.UTF8.GetBytes(new WebHelper().ToUrlString(result.ToString()));
                    Response.Body.Write(temp, 0, temp.Length);
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.ToString() + "\r\n");
                var temp = Error();
                Response.Body.Write(temp, 0, temp.Length);
            }
        }

        /// <summary>
        /// 获取url统计信息
        /// </summary>
        [HttpPost]
        public void GetUrlStatistics()
        {
            try
            {
                var keyid = Request.Form["keyid"].ToString();
                var data = Request.Form["data"].ToString();
                var aesc = new AESConfig(keyid);
                var aesh = new AesHelper();
                data = aesh.Decrypt(data, aesc);
                var postData = (PostData)new DataFactory().CreateData(DataType.Post_Data, data);
                var paras = postData.body.accessParameters.parameters.parameter;
                var start = DateTime.Parse(paras.AsQueryable().FirstOrDefault(e => e.name == "Start").value);
                var end = DateTime.Parse(paras.AsQueryable().FirstOrDefault(e => e.name == "End").value);
                if (start < end)
                {
                    RequestStatisticalService brandService = new RequestStatisticalService(Services);
                    var RequestStatisticalList = brandService.SelectRequestStatisticalBy(start, end);
                    var resultdata = new List<RDataRow>();
                    foreach (var url in RequestStatisticalList.RequestStatisticalUrl)
                    {
                        var urls = new List<RDataColumn>();
                        urls.Add(new RDataColumn
                        {
                            name = "URL",
                            type = "字符串",
                            value = url.request,
                            remark = "访问地址"
                        });
                        urls.Add(new RDataColumn
                        {
                            name = "Sum",
                            type = "整数",
                            value = url.traffic,
                            remark = "访问次数"
                        });
                        resultdata.Add(new RDataRow { column = urls });
                    }
                    var extdata = GetResultModel(true, "获取成功！");
                    var result = new ReturnData("");
                    result.body.dataTable.name = "Statistics";
                    result.body.dataTable.remark = "统计信息";
                    result.SetData(resultdata);
                    result.SetExtData(new List<RDataTableListItem> { extdata });
                    var temp = aesh.Encrypt(result.ToString(), aesc);
                    var bytes = Encoding.UTF8.GetBytes(new WebHelper().ToUrlString(temp));
                    Response.Body.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    var result = new ReturnData("");
                    result.SetExtData(new List<RDataTableListItem> { GetResultModel(false, "日期填写错误！") });
                    var temp = Encoding.UTF8.GetBytes(new WebHelper().ToUrlString(result.ToString()));
                    Response.Body.Write(temp, 0, temp.Length);
                }
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.ToString() + "\r\n");
                var temp = Error();
                Response.Body.Write(temp, 0, temp.Length);
            }
        }
        #endregion

        #region 辅助方法
        private byte[] Error()
        {
            var result = new ReturnData("");
            result.SetExtData(new List<RDataTableListItem> { GetResultModel(false, "参数错误！") });
            return Encoding.UTF8.GetBytes(new WebHelper().ToUrlString(result.ToString()));
        }

        private List<RDataRow> GetDataModel(List<InformationAll> data)
        {
            var result = new List<RDataRow>();
            //这种最好提出来 不然比较拖效率
            var ip = ConfigHelper.GetValue("ServerIp");
            var imgaddress = ConfigHelper.GetValue("ServerImgAddress");
            if (string.IsNullOrEmpty(ip))
            {
                return result;
            }
            foreach (var d in data)
                result.Add(new RDataRow { column = InformationAllToRDataColumns(d, ip, imgaddress) });
            return result;
        }

        #region  咨询列表转总线表数据
        private List<RDataColumn> InformationAllToRDataColumns(InformationAll data, string ip, string imgadrress)
        {
            var result = new List<RDataColumn>();
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "Id",
                value = data.Id + "",
                remark = "咨询Id"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "CreationDate",
                value = data.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                remark = "创建时间"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "LastUpdate",
                value = data.LastUpdate.ToString("yyyy-MM-dd HH:mm:ss"),
                remark = "最后修改时间"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "Title",
                value = data.Title,
                remark = "资讯标题"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "Content",
                value = data.Content,
                remark = "资讯内容"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "ModelName",
                value = data.ModelName,
                remark = "模块名称"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "ColumName",
                value = data.ColumName,
                remark = "栏目名称"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "Photo",
                value = ip + imgadrress + data.Id + "/" + data.Photo,
                remark = "展示图片地址"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "Describe",
                value = data.Describe,
                remark = "描述"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "SecondTitle",
                value = data.Name,
                remark = "二级标题"
            });
            result.Add(new RDataColumn
            {
                type = "字符串",
                name = "IntroUrl",
                value = ip + "/NEWS/News?id=" + data.Id,
                remark = "资讯详情链接"
            });
            return result;
        }
        #endregion

        private RDataTableListItem GetResultModel(bool result, string mesage)
        {
            var res = new RDataTableListItem
            {
                name = "operate result",
                column = new List<RDataColumn>()
            };
            res.column.Add(new RDataColumn
            {
                type = "布尔型",
                name = "OperateResult",
                value = result + "",
                remark = "操作结果"
            });
            res.column.Add(new RDataColumn
            {
                type = "字符串",
                name = "Message",
                value = mesage,
                remark = "详细信息"
            });
            res.column.Add(new RDataColumn
            {
                type = "字符串",
                name = "ListUrl",
                value = ConfigHelper.GetValue("ServerIp") + "/News/Index",
                remark = "详细信息"
            });
            return res;
        }
        #endregion
    }
}
