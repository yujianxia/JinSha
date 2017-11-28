/*****************************
 * 作者：xqx
 * 日期：2017年7月20日
 * 操作：创建
 * ****************************/
using Stepon.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// 资讯试图实体
    /// </summary>
    public class InformationYoungView : BaseEntity
    {
        /// <summary>
        /// 栏目Id
        /// </summary>
        public System.Guid ColumnId { get; set; }

        /// <summary>
        /// 资讯标题
        /// </summary>
        public System.String Title { get; set; }

        /// <summary>
        /// 资讯内容
        /// </summary>
        public System.String Content { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public System.String ReleaseMan { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public System.String FileName { get; set; }

        /// <summary>
        /// 板块Id
        /// </summary>
        public System.Guid ModelId { get; set; }

        /// <summary>
        /// 音频
        /// </summary>
        public System.String Music { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public System.String Describe { get; set; }

        /// <summary>
        /// 展示图片地址
        /// </summary>
        public System.String Photo { get; set; }

        /// <summary>
        /// 栏目描述
        /// </summary>
        public System.String ColDescribe { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public System.String ColumName { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        public System.String ModelName { get; set; }

        /// <summary>
        /// 资讯介绍
        /// </summary>
        public System.String Intro { get; set; }

        /// <summary>
        /// Book书
        /// </summary>
        [Column(TypeName = "jsonb")]
        public System.String Book { get; set; }

        /// <summary>
        /// 姓名字段
        /// </summary>
        public System.String PeopleName { get; set; }
    }
}
