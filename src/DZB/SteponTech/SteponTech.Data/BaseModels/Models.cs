/*****************************
 * 作者：xqx
 * 日期：2017年6月23日
 * 操作：创建
 * ****************************/
using Stepon.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// 板块
    /// </summary>
    public class Models : BaseEntity
    {
        /// <summary>
        /// 板块名称
        /// </summary>
        public System.String ModelName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public System.Boolean IsShow { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        public System.String ReturnUrl { get; set; }
    }
}
