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
    public class WebConfig: BaseEntity
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public System.String WebName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public System.String CompanyName { get; set; }

        /// <summary>
        /// 博物馆链接
        /// </summary>
        [Column(TypeName = "jsonb")]
        public System.String MuseumLink { get; set; }

        /// <summary>
        /// 政府相关链接
        /// </summary>
        [Column(TypeName = "jsonb")]
        public System.String GovernmentLink { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public System.String PhoneNumber { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public System.String Address { get; set; }

        /// <summary>
        /// 备案号
        /// </summary>
        public System.String RegisterNo { get; set; }

        /// <summary>
        /// 水印
        /// </summary>
        public System.String Watermark { get; set; }

        /// <summary>
        /// 微信公众号
        /// </summary>
        public System.String Wechat { get; set; }

        /// <summary>
        /// 闭馆日
        /// </summary>
        public System.String ClosedDay { get; set; }
    }
}
