/*****************************
 * 作者：xqx
 * 日期：2017年6月23日
 * 操作：创建
 * ****************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace SteponTech.Data.CommonModels
{
    /// <summary>
    /// 权限映射表
    /// </summary>
    public class Mapping
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Mark { get; set; }
    }
}
