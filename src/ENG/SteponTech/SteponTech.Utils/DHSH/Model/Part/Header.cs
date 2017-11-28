/*********************************
 * 作者：xzf
 * 日期：2017-6-14 00:19:25
 * 操作：创建
 * ********************************/
using SteponTech.Utils.DHSH.Utils;
using System;

namespace SteponTech.Utils.DHSH.Model.Part
{
    /// <summary>
    /// 提交数据或者返回数据的数据头部
    /// </summary>
    public class Header
    {
        /// <summary>
        /// 消息名称
        /// </summary>
        public string messageName { get; set; }

        /// <summary>
        /// 消息产生时间
        /// </summary>
        public string messageMakingDateTime { get; set; }

        /// <summary>
        /// 发送者编号 用户或者系统
        /// </summary>
        public string senderCode { get; set; }

        /// <summary>
        /// 参考数字  大于0为正常数据 
        /// </summary>
        public string messageReferenceNumber { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string messageVersionNumber { get; set; }
    }
}
