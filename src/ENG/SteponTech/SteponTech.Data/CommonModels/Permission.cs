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
    /// 权限表
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 权限名称（用于显示到树形菜单）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限的标识
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 权限的父级标识，如果为顶级，则可以为空
        /// </summary>
        public string ParentMark { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public int Type { get; set; }

        
    }
}
