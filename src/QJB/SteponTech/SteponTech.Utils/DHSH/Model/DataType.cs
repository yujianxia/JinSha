/*********************************
* 作者：xzf
* 日期：2017-06-17 11:21:13
* 操作：创建
* ********************************/
using System;
using System.Collections.Generic;
namespace SteponTech.Utils.DHSH.Model
{
    /// <summary>
    /// 数据类型枚举
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 获取token的提交数据
        /// </summary>
        Post_Token,

        /// <summary>
        /// 获取数据的提交数据
        /// </summary>
        Post_Data,
        
        /// <summary>
        /// 获取token的返回数据
        /// </summary>
        Return_Token,

        /// <summary>
        /// 获取数据的返回数据
        /// </summary>
        Return_Data,

        /// <summary>
        /// 总线转发的获取数据提交实体
        /// </summary>
        DH_Post_Data
    }
}