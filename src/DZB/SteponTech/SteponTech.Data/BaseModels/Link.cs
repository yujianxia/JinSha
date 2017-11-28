/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using Stepon.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// 链接
    /// </summary>
    public class Link : BaseEntity
    {
        /// <summary>
        /// 链接名称
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 链接类型
        /// </summary>
        public System.String LinkType { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public System.String Url { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public System.Boolean IsShow { get; set; }
    }
}
