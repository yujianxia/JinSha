/*****************************
 * 作者：xqx
 * 日期：2017年7月11日
 * 操作：创建
 * ****************************/
using Stepon.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// 资讯实体
    /// </summary>
    public class Information : BaseEntity
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
        /// 二级标题
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public System.String Describe { get; set; }

        /// <summary>
        /// 展示图片地址
        /// </summary>
        public System.String Photo { get; set; }

        /// <summary>
        /// 资讯时间（这里存的STRING是因为时间有各种格式 前台要获取到时间然后转换成对应需要的格式，售票时间（开馆时间），购票时间（服务信息））
        /// </summary>
        public System.String InformationTime { get; set; }

        /// <summary>
        /// 地址（所有有关地址都从此获取）
        /// </summary>
        public System.String Address { get; set; }

        /// <summary>
        /// 讲座主题（金沙讲坛）
        /// </summary>
        public System.String ActivitySite { get; set; }

        /// <summary>
        /// 演讲人（金沙讲坛,馆长（馆长致辞））
        /// </summary>
        public System.String Speaker { get; set; }

        /// <summary>
        /// 购咨询电话（文创产品,电话（交通信息））
        /// </summary>
        public System.String ConsultingTelephone { get; set; }

        /// <summary>
        /// 联系人（文创产品）
        /// </summary>
        public System.String Contact { get; set; }

        /// <summary>
        /// 各类链接地址
        /// </summary>
        public System.String UrlAddress { get; set; }

        /// <summary>
        /// 资讯介绍
        /// </summary>
        public System.String Intro { get; set; }

        /// <summary>
        /// 文化活动的截止时间
        /// </summary>
        public System.String Deadline { get; set; }

        /// <summary>
        /// 太阳节
        /// </summary>
        public System.String SunId { get; set; }

        /// <summary>
        /// 相关文章
        /// </summary>
        public System.String InformationId { get; set; }

        /// <summary>
        /// 邮箱(交通信息)
        /// </summary>
        public System.String Email { get; set; }

        /// <summary>
        /// 邮编（交通信息）
        /// </summary>
        public System.String ZipCode { get; set; } 

        /// <summary>
        /// 购票价格（服务信息）
        /// </summary>
        public System.String TicketPrice { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public System.Boolean IsTop { get; set; } = false;

    }
    /// <summary>
    /// 资讯实体
    /// </summary>
    public class FileList
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        public System.String FileName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public System.String FileUrl { get; set; }
    }
}
