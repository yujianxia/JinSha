/*********************************
* 作者：xzf
* 日期：2017-06-16 21:28:49
* 操作：创建
* ********************************/
using System;
using System.Collections.Generic;
using SteponTech.Utils.DHSH.Model.Part;
namespace SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart
{
    /// <summary>
    /// 获取token的返回数据的Body的主体部分的附加数据字段实体
    /// 一般用于返回操作结果
    /// </summary>
    public class RDataTableList
    {
        public RDataTableList(){
            list=new List<RDataTableListItem>();
        }
        /// <summary>
        /// 数据表数据行数据
        /// </summary>
        public List<RDataTableListItem> list { get; set; }
    }
}