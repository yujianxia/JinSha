/*****************************
 * 作者：xqx
 * 日期：2017年7月12日
 * 操作：创建
 * ****************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace SteponTech.Data.TrunkSystem
{
    /// <summary>
    /// 会员系统实体
    /// </summary>
    /// <returns></returns>
    public class MemversDetail
    {
        /// <summary>
        /// 会话session
        /// </summary>
        public System.String session { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public System.String member_id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public System.String user_name { get; set; }

        /// <summary>
        /// phone
        /// </summary>
        public System.String phone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        public System.String email { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public System.String card_no { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public System.String level { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public System.String score { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.String create_time { get; set; }

        /// <summary>
        /// 注册来源
        /// </summary>
        public System.String register_from { get; set; }

        /// <summary>
        /// password
        /// </summary>
        public System.String password { get; set; }

        /// <summary>
        /// open_id
        /// </summary>
        public System.String open_id { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public System.String group_id { get; set; }

        /// <summary>
        /// 分组名字
        /// </summary>
        public System.String group_name { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public System.String token { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public System.String nickname { get; set; }
    }
}
