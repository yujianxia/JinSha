/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// 用户权限视图
    /// </summary>
    public class UserRolesView
    {
        //
        // 摘要:
        //     主键
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public System.String Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public System.String PhoneNumber { get; set; }
        
        /// <summary>
        /// 用户名
        /// </summary>
        public System.String UserName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public System.String HeadPortrait { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public System.String RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public System.String Sex { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public System.DateTime CreatTime { get; set; }

        /// <summary>
        /// 上次修改日期
        /// </summary>
        public System.DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public System.String roleid { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public System.String Role { get; set; }
        
        /// <summary>
        /// 用户锁定
        /// </summary>
        public System.Boolean LockoutEnabled { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public System.String Mark { get; set; }
    }
}
