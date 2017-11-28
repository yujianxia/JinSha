/*****************************
 * 作者：xqx
 * 日期：2017年6月23日
 * 操作：创建
 * ****************************/
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stepon.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SteponTech.Data.CommonModels
{
    /// <summary>
    /// 页脚
    /// </summary>
    public class WebConfig : BaseEntity
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public System.String WebName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public System.String Phone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public System.String Address { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public System.String WeChart { get; set; }

        /// <summary>
        /// 微博
        /// </summary>
        public System.String WeBo { get; set; }

        /// <summary>
        /// QQ群号
        /// </summary>
        public System.String QQGroup { get; set; }

        /// <summary>
        /// QQ群号
        /// </summary>
        public System.String QQGroup2 { get; set; }

        /// <summary>
        /// 备案号
        /// </summary>
        public System.String RegisterNo { get; set; }
    }
}
