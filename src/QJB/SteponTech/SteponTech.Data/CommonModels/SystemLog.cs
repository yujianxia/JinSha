/*****************************
 * 作者：xqx
 * 日期：2017年6月23日
 * 操作：创建
 * ****************************/
using Stepon.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SteponTech.Data.CommonModel
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class SystemLog : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        /// <summary>
        /// 用户真实名称
        /// </summary>
        [MaxLength(50)]
        public string RealName { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        [MaxLength(400)]
        public string OpreationMode { get; set; }
    }
}
