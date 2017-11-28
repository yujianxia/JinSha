/*****************************
 * 作者：xqx
 * 日期：2017年7月13日
 * 操作：创建
 * ****************************/
using System;
using System.ComponentModel.DataAnnotations;
using Stepon.EntityFrameworkCore;

namespace SteponTech.Data.BaseModel
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLog : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public System.String UserName { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public System.String Address { get; set; }

        /// <summary>
        /// 警告
        /// </summary>
        public System.String Message { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public System.String Browser { get; set; }

        /// <summary>
        /// 浏览器版本
        /// </summary>
        public System.String BVersion { get; set; }
    }
}
