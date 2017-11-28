/*********************************
* 作者：xzf
* 日期：2017-06-16 21:00:36
* 操作：创建
* ********************************/
using System;
using System.Collections.Generic;
using SteponTech.Utils.DHSH.Model.Part;
namespace SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart
{
    /// <summary>
    /// 获取数据的Body部分的数据行部分
    /// </summary>
    public class RDataColumn
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public string type { get; set; }
        
        /// <summary>
        /// 数据姓名
        /// </summary>
         public string name { get; set; }

        /// <summary>
        /// 数据值
        /// </summary>
        public string value { get; set; }
        
        /// <summary>
        /// 数据含义
        /// </summary>
        public string remark { get; set; }
    }
}