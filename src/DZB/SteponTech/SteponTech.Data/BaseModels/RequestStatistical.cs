/*****************************
 * 作者：xqx
 * 日期：2017年7月20日
 * 操作：创建
 * ****************************/
using Stepon.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// 请求统计实体
    /// </summary>
    public class RequestStatistical : BaseEntity
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public System.String RequestUrl { get; set; }

        /// <summary>
        /// 请求页面名称
        /// </summary>
        public System.String UrlName { get; set; }

        /// <summary>
        /// 请求者城市
        /// </summary>
        public System.String RequestCountry { get; set; }

        /// <summary>
        /// 请求者IP（IP）
        /// </summary>
        public System.String RequestIp { get; set; }
    }

    /// <summary>
    /// 请求统计访问量实体
    /// </summary>
    public class RequestStatisticalBy
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public System.String request { get; set; }

        /// <summary>
        /// 请求条数
        /// </summary>
        public System.String traffic { get; set; }
    }


    /// <summary>
    /// 请求统计打包实体
    /// </summary>
    public class RequestStatisticalByAll
    {
        /// <summary>
        /// 访问总数
        /// </summary>
        public int RequestTotal { get; set; }

        /// <summary>
        /// 一周内各地区的访问总数
        /// </summary>
        public int OneWeekRequest { get; set; }

        /// <summary>
        /// 请求url
        /// </summary>
        public List<RequestStatisticalBy> RequestStatisticalUrl { get; set; }

        /// <summary>
        /// 请求城市
        /// </summary>
        public List<RequestStatisticalBy> RequestStatisticalCountry { get; set; }

    }

    /// <summary>
    /// 请求获取ip地址实体
    /// </summary>
    public class RequestIpContent
    {
        /// <summary>
        /// code
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// data
        /// </summary>
        public Newtonsoft.Json.Linq.JObject data { get; set; }

    }
}
