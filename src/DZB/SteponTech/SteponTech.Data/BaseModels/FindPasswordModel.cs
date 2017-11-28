/*****************************
 * 作者：xqx
 * 日期：2017年7月13日
 * 操作：创建
 * ****************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// 忘记密码
    /// </summary>
    public class FindPasswordModel
    {
        /// <summary>
        /// 联系方式(微信号或邮箱)
        /// </summary>
        [Required]
        public string Contact { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string Password { get; set; }
    }
}
