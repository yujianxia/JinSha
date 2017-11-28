/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using Stepon.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// Banner
    /// </summary>
    public class Banner : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        public System.String ReturnUrl { get; set; }

        /// <summary>
        /// 缩略图地址
        /// </summary>
        public System.String FileName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public System.Boolean IsShow { get; set; }

        /// <summary>
        /// 栏目Id
        /// </summary>
        public System.Guid ModelsId { get; set; }

        /// <summary>
        /// 是否跳转
        /// </summary>
        public System.Boolean IsUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public System.String Content { get; set; }
    }
}
