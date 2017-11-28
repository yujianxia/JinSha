/*********************************
* 作者：xzf
* 日期：2017-06-16 21:28:58
* 操作：创建
* ********************************/
using System;
using System.Collections.Generic;
using SteponTech.Utils.DHSH.Model.Part;
namespace SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart
{
    /// <summary>
    /// 获取token的返回数据的Body的主体部分的附加数据字段具体内容实体
    /// </summary>
    public class RDataTableListItem
    {
        public RDataTableListItem(){
            column = new List<RDataColumn>();
        }
        /// <summary>
        /// 附加数据表名称
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// 附加数据表
        /// </summary>
        public List<RDataColumn> column { get; set; }
    }
}