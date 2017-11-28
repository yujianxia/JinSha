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
    /// 栏目
    /// </summary>
    public class ColunmsYoung : BaseEntity
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public System.Guid ModelId { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 上级栏目Id
        /// </summary>
        public System.Guid ColunmsId { get; set; }

        /// <summary>
        /// 板块排序
        /// </summary>
        public System.Int32 Sorting { get; set; }

        /// <summary>
        /// 板块描述
        /// </summary>
        public System.String ColDescribe { get; set; }

        /// <summary>
        /// 板块展示
        /// </summary>
        public System.String ColPhoto { get; set; }

        /// <summary>
        /// 是否属于新增栏目
        /// </summary>
        public System.Boolean IsNew { get; set; }
    }
    /// <summary>
    /// 栏目视图
    /// </summary>
    public class ColunmsYoungView : BaseEntity
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public System.Guid ModelId { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 上级栏目Id
        /// </summary>
        public System.Guid ColunmsId { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        public System.String ModelName { get; set; }

        /// <summary>
        /// 板块描述
        /// </summary>
        public System.String ColDescribe { get; set; }

        /// <summary>
        /// 板块展示
        /// </summary>
        public System.String ColPhoto { get; set; }

        /// <summary>
        /// 是否属于新增栏目
        /// </summary>
        public System.Boolean IsNew { get; set; }
    }
    /// <summary>
    /// 查询栏目条件
    /// </summary>
    public class SearchColunms
    {
        /// <summary>
        /// 板块描述
        /// </summary>
        public System.Guid ModelId { get; set; }

        /// <summary>
        /// 板块展示
        /// </summary>
        public System.String ColName { get; set; }
    }
}
